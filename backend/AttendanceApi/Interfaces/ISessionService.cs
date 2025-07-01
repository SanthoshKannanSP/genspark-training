using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Interfaces;

public interface ISessionService
{
    public Task<Session> ScheduleSession(AddSessionRequestDTO addSessionRequestDTO);  // DOne
    public Task<Session> CancelSession(int sessionId); // Done
    public Task<Session> CompleteSession(int sessionId); // Done
    public Task<Session> UpdateSession(UpdateSessionRequestDTO updateSessionRequestDTO, int sessionId); // Done
    public Task<PaginatedResponseDTO<List<AllSessionRequestDTO>>> GetAllSession(int page, int pageSize, string? sessionName = null, DateOnly? startDate = null, DateOnly? endDate = null, TimeOnly? startTime = null, TimeOnly? endTime = null, string? status = null); // Done
    public Task<Session> GetSession(int sessionId); // Done
    public Task<List<Session>> GetSessionByTeacher(int teacherId); // Done
    public Task<List<SessionAttendance>> AddStudentToSession(AddStudentToSessionRequestDTO addStudentToSessionRequestDTO); // DOne
    public Task RemoveStudentsFromSession(List<int> studentIds, int sessionId); // DOne

    public Task<List<UpcomingSessionsResponseDTO>> GetUpcomingSessions();

    public Task<List<PastSessionResponseDTO>> GetPastSessions();
    public Task<PaginatedResponseDTO<List<AttendanceDetailsResponseDTO>>> GetAttendanceDetails(int page, int pageSize, string? sessionName = null, DateOnly? startDate = null, DateOnly? endDate = null, TimeOnly? startTime = null, TimeOnly? endTime = null);
}