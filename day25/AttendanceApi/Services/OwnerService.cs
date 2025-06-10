using AttendanceApi.Interfaces;
using AttendanceApi.Models;

namespace AttendanceApi.Services;

public class OwnerService : IOwnerService
{
    private readonly IRepository<int, Student> _studentRepository;
    private readonly IRepository<int, Teacher> _teacherRepository;
    public OwnerService(IRepository<int, Teacher> teacherRepository, IRepository<int, Student> studentRepository)
    {
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
    }
    public async Task<bool> IsOwnerOfResource(string username, string resourceType, int resourceId)
    {
        IOwnableResource? resource = resourceType switch
        {
            "Student" => await _studentRepository.Get(resourceId),
            "Teacher" => await _teacherRepository.Get(resourceId),
            _ => null
        };

        return resource != null && resource.OwnerName == username;
    }
}