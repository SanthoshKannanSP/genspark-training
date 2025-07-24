using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AttendanceApi.Interfaces;
using AttendanceApi.Misc;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using Microsoft.AspNetCore.SignalR;
using QuestPDF.Fluent;

namespace AttendanceApi.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IRepository<int, SessionAttendance> _sessionAttendanceRepository;
    private readonly IRepository<int, Session> _sessionRepository;
    private readonly IRepository<int, AttendanceEditRequest> _attendanceEditRequestRepository;

    private readonly IHubContext<NotificationHub> _hubContext;
    public AttendanceService(IRepository<int, SessionAttendance> sessionAttendanceRepository, IRepository<int, Session> sessionRepository, IRepository<int, AttendanceEditRequest> attendanceEditRequestRepository, IHubContext<NotificationHub> hubContext)
    {
        _sessionAttendanceRepository = sessionAttendanceRepository;
        _sessionRepository = sessionRepository;
        _attendanceEditRequestRepository = attendanceEditRequestRepository;
        _hubContext = hubContext;
    }

    // R E Q U E S T    B Y    T E A C H E R
    public async Task<AttendanceEditRequest> RequestAttendanceEditAsync(AttendanceEditRequestDTO dto)
    {
        var sessionAttendance = await _sessionAttendanceRepository.Get(dto.SessionAttendanceId);
        if (sessionAttendance == null)
            throw new Exception("Invalid SessionAttendanceId");

        if (sessionAttendance.Status == dto.RequestedStatus)
            throw new Exception("Requested status is same as current status");


        var existingRequests = await _attendanceEditRequestRepository.GetAll();
        var pending = existingRequests.FirstOrDefault(r =>
            r.SessionAttendanceId == dto.SessionAttendanceId && r.Status == "Pending");
        if (pending != null)
            throw new Exception("A pending request already exists for this record");

        var request = new AttendanceEditRequest
        {
            SessionAttendanceId = dto.SessionAttendanceId,
            RequestedStatus = dto.RequestedStatus
        };

        return await _attendanceEditRequestRepository.Add(request);
    }

    // A D M I N
    public async Task<SessionAttendance> AddAttendanceToStudent(AttendanceUpdateDTO attendanceUpdateDTO)
    {
        var attendances = await _sessionAttendanceRepository.GetAll();
        var attendance = attendances.FirstOrDefault(s => s.StudentId == attendanceUpdateDTO.StudentId && s.SessionId == attendanceUpdateDTO.SessionId);
        if (attendance == null)
        {
            throw new Exception("Student not scheduled to attend session");
        }
        if (attendance.Status == "Attended")
            throw new Exception("Attendance already marked");
        if (attendance.Session.Status == "Cancelled")
            throw new Exception("Cannot add attendance to a cancelled session");

        attendance.Status = "Attended";
        attendance = await _sessionAttendanceRepository.Update(attendance.SessionAttendanceId, attendance);

        var connections = NotificationHub.GetConnections(attendance.Student.Email);
        if (connections != null)
        {
            foreach (var connection in connections)
            {
                await _hubContext.Clients.Client(connection).SendAsync("AttendanceMarked", attendance.Session.SessionName);
            }
        }

        return attendance;
    }

    public async Task<PaginatedResponseDTO<SessionAttendanceResponseDTO>> GetAttendanceOfSession(int sessionId, int page, int pageSize, string? studentName = null, bool? attended = null)
    {
        page = page > 0 ? page : 1;
        pageSize = pageSize > 0 ? pageSize : 10;

        var attendances = await _sessionAttendanceRepository.GetAll();
        attendances = attendances.Where(s => s.SessionId == sessionId);

        var response = attendances.Select(a => new SessionAttendanceDTO() { StudentId = a.StudentId, StudentName = a.Student.Name, Attended = a.Status == "Attended", SessionId = a.SessionId, SessionAttendanceId = a.SessionAttendanceId });

        if (!string.IsNullOrWhiteSpace(studentName))
            response = response.Where(a => a.StudentName.ToLower().Contains(studentName.ToLower()));

        if (attended.HasValue)
            response = response.Where(a => a.Attended == attended);

        var totalRecords = response.Count();
        var paginatedAttendance = response.OrderBy(s => s.StudentName).Skip((page - 1) * pageSize).Take(pageSize).ToList();

        var paginatedResponse = new PaginatedResponseDTO<SessionAttendanceResponseDTO>
        {
            Data = new SessionAttendanceResponseDTO
            {
                RegisteredCount = response.Count(),
                AttendedCount = response.Where(a => a.Attended).Count(),
                SessionAttendance = paginatedAttendance
            },
            Pagination = new PaginationModel
            {
                TotalRecords = totalRecords,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
            }
        };

        return paginatedResponse;
    }

    public async Task<List<SessionAttendance>> GetAttendanceOfStudent(int studentId)
    {
        var attendances = await _sessionAttendanceRepository.GetAll();
        attendances = attendances.Where(s => s.StudentId == studentId && s.Session.Status != "Cancelled");

        return attendances.ToList();
    }

    public async Task<SessionAttendance> RemoveAttendanceFromStudent(AttendanceUpdateDTO attendanceUpdateDTO)
    {
        var attendances = await _sessionAttendanceRepository.GetAll();
        var attendance = attendances.FirstOrDefault(s => s.StudentId == attendanceUpdateDTO.StudentId && s.SessionId == attendanceUpdateDTO.SessionId);
        if (attendance == null)
        {
            throw new Exception("Student not scheduled to attend session");
        }
        if (attendance.Status == "NotAttended")
            throw new Exception("Attendance already unmarked");

        attendance.Status = "NotAttended";
        attendance = await _sessionAttendanceRepository.Update(attendance.SessionAttendanceId, attendance);

        var connections = NotificationHub.GetConnections(attendance.Student.Email);
        if (connections != null)
        {
            foreach (var connection in connections)
            {
                await _hubContext.Clients.Client(connection).SendAsync("AttendanceUnmarked", attendance.Session.SessionName);
            }
        }

        return attendance;
    }

    public async Task<byte[]> GenerateSessionReport(int sessionId)
    {
        var attendanceReportDto = new AttendanceReportDTO();

        var session = await _sessionRepository.Get(sessionId);
        if (session == null)
            throw new Exception("Session not found");
        attendanceReportDto.SessionName = session.SessionName;
        attendanceReportDto.Date = session.Date;
        attendanceReportDto.StartTime = session.StartTime;
        attendanceReportDto.EndTime = session.EndTime;

        var attendances = await _sessionAttendanceRepository.GetAll();
        attendances = attendances.Where(s => s.SessionId == sessionId);
        var attendanceDetails = attendances.Select(a => new SessionAttendanceDTO() { StudentId = a.StudentId, StudentName = a.Student.Name, Attended = a.Status == "Attended", SessionId = a.SessionId, Email = a.Student.Email });
        attendanceReportDto.RegisteredCount = attendanceDetails.Count();
        attendanceReportDto.AttendedCount = attendanceDetails.Count(s => s.Attended == true);
        attendanceReportDto.SessionAttendance = attendanceDetails.OrderBy(s => s.StudentName).ToList();

        var document = new AttendanceReport(attendanceReportDto);
        using var stream = new MemoryStream();
        document.GeneratePdf(stream);
        return stream.ToArray();
    }

    //GET ALL ATTENDANCE EDIT REQUESTS
    public async Task<List<AttendanceEditRequestDTOResponse>> GetAllAttendanceEditRequests()
    {
        var query = await _attendanceEditRequestRepository.GetAll(); // IQueryable<AttendanceEditRequest>

        var result = await query
            .Select(r => new AttendanceEditRequestDTOResponse
            {
                Id = r.Id,
                SessionAttendanceId = r.SessionAttendanceId,
                RequestedStatus = r.RequestedStatus,
                Status = r.Status,
                RequestedAt = r.RequestedAt,
                StudentName = r.SessionAttendance.Student.Name,
                SessionName =r.SessionAttendance.Session.SessionName,
                CurrentStatus = r.SessionAttendance.Status,
                StudentId = r.SessionAttendance.StudentId,
                SessionId = r.SessionAttendance.SessionId
            })
            .ToListAsync();

        return result;
    }

    //APPROVE EDIT REQUEST
    public async Task<bool> ApproveEditRequestAsync(int requestId)
    {
        var request = await _attendanceEditRequestRepository.Get(requestId);
        Console.WriteLine(request);
        if (request == null)
            return false;

        request.Status = "Approved";

        // await _attendanceEditRequestRepository.UpdateAsync(request);
        await _attendanceEditRequestRepository.Update(request.Id, request);
        return true;
    }

}