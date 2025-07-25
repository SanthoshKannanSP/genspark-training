using System.Security.Claims;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TeacherService(IRepository<int, Teacher> teacherRepository, IRepository<string, User> userRepository, IEncryptionService encryptionService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _teacherRepository = teacherRepository;
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }
    public async Task<Teacher> AddTeacher(AddTeacherRequestDTO addTeacherRequestDTO)
    {
            var teachers = await _teacherRepository.GetAll();
            var existingTeacher = teachers.FirstOrDefault(t => t.Email == addTeacherRequestDTO.Email);
            if (existingTeacher != null)
                throw new Exception("An account with the given email already exists");
            var user = _mapper.Map<User>(addTeacherRequestDTO);
            
            // Enabling Admin/Teacher roles
            // user.Role = "Teacher";
            user.Role = string.IsNullOrWhiteSpace(addTeacherRequestDTO.Role)
                ? "Teacher"
                : addTeacherRequestDTO.Role.Trim();

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

    public async Task<Teacher> DeactivateTeacher()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrWhiteSpace(username))
            throw new Exception("Username not found");

        var teachers = await _teacherRepository.GetAll();
        var teacher = teachers.FirstOrDefault(s => s.Email == username);
        if (teacher == null)
            throw new Exception("Teacher not found");

        if (teacher.Status == "Deactivated")
            throw new Exception("Teacher is already deactivated");
        teacher.Status = "Deactivated";
        teacher = await _teacherRepository.Update(teacher.TeacherId, teacher);
        return teacher;
    }

    // ADMIN DELETE
    public async Task<bool> DeleteTeacherByIdAsync(int teacherId)
    {
        var teacher = await _teacherRepository.Get(teacherId);
        if (teacher == null)
            throw new Exception("Teacher not found");

        if (teacher.Status == "Deactivated")
            throw new Exception("Teacher is already deactivated");

        teacher.Status = "Deactivated";
        await _teacherRepository.Update(teacherId, teacher);
        return true;
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

    public async Task<TeacherDetailsDTO> GetMyDetails()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrWhiteSpace(username))
            throw new Exception("Username not found");

        var teachers = await _teacherRepository.GetAll();
        var teacher = teachers.FirstOrDefault(s => s.Email == username);

        if (teacher == null)
            throw new Exception("Teacher not found");

        var resposne = new TeacherDetailsDTO()
        {
            Name = teacher.Name,
            Organization = teacher.Organization
        };
        return resposne;
    }

    public async Task<Teacher> GetTeacher(int teacherId)
    {
        var teacher = await _teacherRepository.Get(teacherId);
        if (teacher == null)
            throw new Exception("No teacher found");
        return teacher;
    }
    public async Task<TeacherDetailsDTO> UpdateDetails(TeacherDetailsDTO teacherDetailsDTO)
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrWhiteSpace(username))
            throw new Exception("Username not found");
        var teachers = await _teacherRepository.GetAll();
        var teacher = teachers.FirstOrDefault(s => s.Email == username);

        if (teacher == null)
            throw new Exception("Teacher not found");

        teacher.Name = teacherDetailsDTO.Name;
        teacher.Organization = teacherDetailsDTO.Organization;
        teacher = await _teacherRepository.Update(teacher.TeacherId, teacher);

        return new TeacherDetailsDTO()
        {
            Name = teacher.Name,
            Organization = teacher.Organization
        };
    }

}