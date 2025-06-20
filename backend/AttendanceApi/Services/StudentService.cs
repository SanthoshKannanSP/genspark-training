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
    public StudentService(IRepository<int, Student> studentRepository, IRepository<string, User> userRepository, IEncryptionService encryptionService, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _mapper = mapper;
    }
    public async Task<Student> AddStudent(AddStudentRequestDTO addStudentRequestDTO)
    {
        try
        {
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
        catch (Exception ex)
        {
            throw new Exception("Unable to create Student");
        }
        
    }

    public async Task<Student> DeactivateStudent(int StudentId)
    {
        var student = await _studentRepository.Get(StudentId);
        if (student == null)
            throw new Exception("Student not found");
        if (student.Status == "Deactivated")
            throw new Exception("Student already deactivated");
        student.Status = "Deactivated";
        student = await _studentRepository.Update(student.StudentId,student);
        return student;
    }

    public async Task<List<Student>> GetAllActiveStudents(int page, int pageSize)
    {
        page = page > 0 ? page : 1;
        pageSize = pageSize > 0 ? pageSize : 10;
        var students = await _studentRepository.GetAll();
        if (students == null)
            throw new Exception("No students found");
        students = students.Where(s => s.Status == "Active");
        students = students.Skip((page - 1) * pageSize).Take(pageSize);
        return students.ToList();
    }

    public async Task<Student> GetStudent(int studentId)
    {
        var student = await _studentRepository.Get(studentId);
        if (student == null)
            throw new Exception("Student not found");
        return student;
    }
}