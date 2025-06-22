using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;

namespace AttendanceApi.Interfaces;

public interface ISessionService
{
    public Task<Session> ScheduleSession(AddSessionRequestDTO addSessionRequestDTO);  // DOne
    public Task<Session> CancelSession(int sessionId); // Done
    public Task<Session> CompleteSession(int sessionId); // Done
    public Task<Session> UpdateSession(UpdateSessionRequestDTO updateSessionRequestDTO, int sessionId); // Done
    public Task<List<Session>> GetAllSession(int page, int pageSize); // Done
    public Task<Session> GetSession(int sessionId); // Done
    public Task<List<Session>> GetSessionByTeacher(int teacherId); // Done
    public Task<List<SessionAttendance>> AddStudentsToSession(AddStudentsToSessionRequestDTO addStudentsToSessionRequestDTO, int sessionId); // DOne
    public Task RemoveStudentsFromSession(List<int> studentIds, int sessionId); // DOne

    public Task<List<Session>> GetUpcomingSessions();
}