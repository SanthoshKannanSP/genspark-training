using AttendanceApi.Interfaces;
using AttendanceApi.Models;

namespace AttendanceApi.Services;

public class OwnerService : IOwnerService
{
    private readonly IRepository<int, Student> _studentRepository;
    private readonly IRepository<int, Teacher> _teacherRepository;
    private readonly IRepository<int, Session> _sessionRepository;

    public OwnerService(IRepository<int, Teacher> teacherRepository, IRepository<int, Student> studentRepository, IRepository<int, Session> sessionRepository)
    {
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
        _sessionRepository = sessionRepository;
    }
    public async Task<bool> IsOwnerOfResource(string username, string resourceType, int resourceId)
    {
        IOwnableResource? resource = resourceType switch
        {
            "Student" => await _studentRepository.Get(resourceId),
            "Teacher" => await _teacherRepository.Get(resourceId),
            "Session" => await _sessionRepository.Get(resourceId),
            _ => null
        };

        System.Console.WriteLine(resource.OwnerName);

        return resource != null && resource.OwnerName == username;
    }
}