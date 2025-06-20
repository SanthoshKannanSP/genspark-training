using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AutoMapper;

namespace AttendanceApi.Services;

public class TeacherService : ITeacherService
{
    private readonly IRepository<int, Teacher> _teacherRepository;
    private readonly IRepository<string, User> _userRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly IMapper _mapper;
    public TeacherService(IRepository<int,Teacher> teacherRepository, IRepository<string,User> userRepository, IEncryptionService encryptionService, IMapper mapper)
    {
        _teacherRepository = teacherRepository;
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _mapper = mapper;
    }
    public async Task<Teacher> AddTeacher(AddTeacherRequestDTO addTeacherRequestDTO)
    {
        try
        {
            var user = _mapper.Map<User>(addTeacherRequestDTO);
            user.Role = "Teacher";
            var encryptedData = _encryptionService.EncryptData(new EncryptModel()
            {
                Data = addTeacherRequestDTO.Password
            });
            user.Password = encryptedData.EncryptedData;
            user.HashKey = encryptedData.HashKey;
            user = await _userRepository.Add(user);
            var teacher = _mapper.Map<Teacher>(addTeacherRequestDTO);
            teacher.Status = "Active";
            teacher = await _teacherRepository.Add(teacher);
            return teacher;
        }
        catch (Exception ex)
        {
            throw new Exception("Unable to create Teacher");
        }    
    }

    public async Task<Teacher> DeactivateTeacher(int teacherId)
    {
        var teacher = await _teacherRepository.Get(teacherId);
        if (teacher == null)
            throw new Exception("Teacher with given Id not found");
        if (teacher.Status == "Deactivated")
            throw new Exception("Teacher is already deactivated");
        teacher.Status = "Deactivated";
        teacher = await _teacherRepository.Update(teacher.TeacherId,teacher);
        return teacher;
    }

    public async Task<List<Teacher>> GetAllActiveTeachers(int? page, int? pageSize)
    {
        page = page > 0 ? page : 1;
        pageSize = pageSize > 0 ? pageSize : 10;
        var teachers = await _teacherRepository.GetAll();
        if (teachers == null)
            throw new Exception("No teachers found");
        teachers = teachers.Where(t => t.Status == "Active");
        teachers = teachers.Skip((page!.Value - 1) * pageSize!.Value).Take(pageSize!.Value);

        return teachers.ToList();
    }

    public async Task<Teacher> GetTeacher(int teacherId)
    {
        var teacher = await _teacherRepository.Get(teacherId);
        if (teacher == null)
            throw new Exception("No teacher found");
        return teacher;
    }
}