using System.Security.Claims;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AutoMapper;

namespace AttendanceApi.Services;

public class StudentService : IStudentService
{
    private readonly IRepository<int, Student> _studentRepository;
    private readonly IRepository<string, User> _userRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public StudentService(IRepository<int, Student> studentRepository, IRepository<string, User> userRepository, IEncryptionService encryptionService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }
    public async Task<Student> AddStudent(AddStudentRequestDTO addStudentRequestDTO)
    {
            var students = await _studentRepository.GetAll();
            var existingStudent = students.FirstOrDefault(t => t.Email == addStudentRequestDTO.Email);
            if (existingStudent != null )
                throw new Exception("An account with the given email already exists");
            var user = _mapper.Map<User>(addStudentRequestDTO);
            user.Role = "Student";
            var encryptedData = _encryptionService.EncryptData(new EncryptModel()
            {
                Data = addStudentRequestDTO.Password
            });
            user.Password = encryptedData.EncryptedData;
            user.HashKey = encryptedData.HashKey;
            user = await _userRepository.Add(user);

            var student = _mapper.Map<Student>(addStudentRequestDTO);
            student.Status = "Active";
            student = await _studentRepository.Add(student);

            return student;
    }

    public async Task<Student> DeactivateStudent()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrWhiteSpace(username))
            throw new Exception("Username not found");

        var students = await _studentRepository.GetAll();
        var student = students.FirstOrDefault(s => s.Email == username);

        if (student == null)
            throw new Exception("Student not found");

        if (student.Status == "Deactivated")
            throw new Exception("Student already deactivated");
        student.Status = "Deactivated";
        student = await _studentRepository.Update(student.StudentId, student);
        return student;
    }

    // public async Task<List<Student>> GetAllActiveStudents(int page, int pageSize)
    // {
    //     page = page > 0 ? page : 1;
    //     pageSize = pageSize > 0 ? pageSize : 10;
    //     var students = await _studentRepository.GetAll();
    //     if (students == null)
    //         throw new Exception("No students found");
    //     students = students.Where(s => s.Status == "Active");
    //     students = students.Skip((page - 1) * pageSize).Take(pageSize);
    //     return students.ToList();
    // }
    public async Task<List<StudentResponseDto>> GetAllActiveStudents(int page, int pageSize)
    {
        page = page > 0 ? page : 1;
        pageSize = pageSize > 0 ? pageSize : 10;

        var students = await _studentRepository.GetAll();

        var filtered = students
            .Where(s => s.Status == "Active")
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new StudentResponseDto
            {
                StudentId = s.StudentId,
                Name = s.Name,
                Email = s.Email,
                Status = s.Status,
                Batch = s.Batch == null ? null : new StudentResponseDto.BatchDto
                {
                    BatchId = s.Batch.BatchId,
                    BatchName = s.Batch.BatchName
                }
            });

        return filtered.ToList();

    }


    public async Task<StudentDetailsDTO> GetMyDetails()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrWhiteSpace(username))
            throw new Exception("Username not found");

        var students = await _studentRepository.GetAll();
        var student = students.FirstOrDefault(s => s.Email == username);

        if (student == null)
            throw new Exception("Student not found");

        var resposne = new StudentDetailsDTO()
        {
            Name = student.Name,
            DateOfBirth = student.DateOfBirth,
            Gender = student.Gender
        };
        return resposne;
    }

    public async Task<Student> GetStudent(int studentId)
    {
        var student = await _studentRepository.Get(studentId);
        if (student == null)
            throw new Exception("Student not found");
        return student;
    }
    
    public async Task<StudentDetailsDTO> UpdateDetails(StudentDetailsDTO studentDetailsDTO)
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrWhiteSpace(username))
            throw new Exception("Username not found");
        var students = await _studentRepository.GetAll();
        var student = students.FirstOrDefault(s => s.Email == username);

        if (student == null)
            throw new Exception("student not found");

        student.Name = studentDetailsDTO.Name;
        student.DateOfBirth = studentDetailsDTO.DateOfBirth;
        student.Gender = studentDetailsDTO.Gender;
        student = await _studentRepository.Update(student.StudentId, student);

        return new StudentDetailsDTO()
        {
            Name = student.Name,
            DateOfBirth = student.DateOfBirth,
            Gender = student.Gender
        };
    }
}