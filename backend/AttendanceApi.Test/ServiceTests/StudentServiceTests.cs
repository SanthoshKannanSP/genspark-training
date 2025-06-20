using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AttendanceApi.Services;
using AutoMapper;
using Moq;

public class StudentServiceTests
{
    private Mock<IRepository<int, Student>> _studentRepositoryMock;
    private Mock<IRepository<string, User>> _userRepositoryMock;
    private Mock<IEncryptionService> _encryptionServiceMock;
    private Mock<IMapper> _mapperMock;

    [SetUp]
    public async Task Setup()
    {
        _studentRepositoryMock = new();
        _userRepositoryMock = new();
        _encryptionServiceMock = new();
        _mapperMock = new();
    }

    [Test]
    public async Task AddStudent_ShouldAddStudentAndUser()
    {
        var addStudentRequestDTO = new AddStudentRequestDTO
        {
            Name = "John Doe",
            Email = "johndoe@gmail.com",
            Age = 20,
            Gender = "Male",
            Password = "johndoe123"
        };
        var mappedUser = new User()
        {
            Username = "johndoe@gmail.com"
        };
        var createdUser = new User()
        {
            Username = "johndoe@gmail.com",
            Role = "Student",
            Password = System.Text.Encoding.UTF8.GetBytes("encryptedPassword"),
            HashKey = System.Text.Encoding.UTF8.GetBytes("hashKey")
        };
        var createdStudent = new Student()
        {
            StudentId = 1,
            Email = "johndoe@gmail.com",
            Name = "John Doe",
            Age = 20,
            Gender = "Male",
            Status = "Active"
        };
        var mappedStudent = new Student()
        {
            Name = "John Doe",
            Email = "johndoe@gmail.com",
            Age = 20,
            Gender = "Male"
        };
        var encryptedData = new EncryptModel()
        {
            Data = "johndoe123",
            EncryptedData = System.Text.Encoding.UTF8.GetBytes("encryptedPassword"),
            HashKey = System.Text.Encoding.UTF8.GetBytes("hashKey")
        };
        _userRepositoryMock.Setup(u => u.Add(It.IsAny<User>())).ReturnsAsync(createdUser);
        _studentRepositoryMock.Setup(u => u.Add(It.IsAny<Student>())).ReturnsAsync(createdStudent);
        _mapperMock.Setup(m => m.Map<User>(addStudentRequestDTO)).Returns(mappedUser);
        _mapperMock.Setup(m => m.Map<Student>(addStudentRequestDTO)).Returns(mappedStudent);
        _encryptionServiceMock.Setup(e => e.EncryptData(It.IsAny<EncryptModel>())).Returns(encryptedData);
        var studentService = new StudentService(_studentRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);

        var student = await studentService.AddStudent(addStudentRequestDTO);

        Assert.That(student, Is.Not.Null);
        Assert.That(student.StudentId, Is.EqualTo(1));
        Assert.That(student.Name, Is.EqualTo("John Doe"));
        Assert.That(student.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(student.Status, Is.EqualTo("Active"));
        Assert.That(student.OwnerName, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(student.Age, Is.EqualTo(20));
        Assert.That(student.Gender, Is.EqualTo("Male"));

    }

    [Test]
    public async Task AddStudent_ShouldThrow_WhenUserCreationFails()
    {
        var addStudentRequestDTO = new AddStudentRequestDTO
        {
            Name = "John Doe",
            Email = "johndoe@gmail.com",
            Age = 20,
            Gender = "Male",
            Password = "johndoe123"
        };
        var mappedUser = new User()
        {
            Username = "johndoe@gmail.com"
        };
        var mappedStudent = new Student()
        {
            Name = "John Doe",
            Email = "johndoe@gmail.com",
            Age = 20,
            Gender = "Male"
        };
        var encryptedData = new EncryptModel()
        {
            Data = "johndoe123",
            EncryptedData = System.Text.Encoding.UTF8.GetBytes("encryptedPassword"),
            HashKey = System.Text.Encoding.UTF8.GetBytes("hashKey")
        };
        _mapperMock.Setup(m => m.Map<User>(addStudentRequestDTO)).Returns(mappedUser);
        _mapperMock.Setup(m => m.Map<Student>(addStudentRequestDTO)).Returns(mappedStudent);
        _encryptionServiceMock.Setup(e => e.EncryptData(It.IsAny<EncryptModel>())).Returns(encryptedData);
        _userRepositoryMock.Setup(u => u.Add(It.IsAny<User>())).ThrowsAsync(new Exception("Unable to add User"));
        var studentService = new StudentService(_studentRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () =>
               await studentService.AddStudent(addStudentRequestDTO));

        Assert.That(ex.Message, Is.EqualTo("Unable to create Student"));
    }

    [Test]
    public async Task DeactivateStudent_ShouldSetStatusToDeactivated()
    {
        var createdStudent = new Student()
        {
            Email = "johndoe@gmail.com",
            Name = "John Doe",
            Age = 20,
            Gender = "Male",
            Status = "Active",
            StudentId = 1
        };
        var updatedStudent = new Student()
        {
            Email = "johndoe@gmail.com",
            Name = "John Doe",
            Age = 20,
            Gender = "Male",
            Status = "Deactivated",
            StudentId = 1
        };
        _studentRepositoryMock.Setup(t => t.Get(1)).ReturnsAsync(createdStudent);
        _studentRepositoryMock.Setup(t => t.Update(1, It.IsAny<Student>())).ReturnsAsync(updatedStudent);
        var studentService = new StudentService(_studentRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);

        var student = await studentService.DeactivateStudent(1);

        Assert.That(student.StudentId, Is.EqualTo(1));
        Assert.That(student.Name, Is.EqualTo("John Doe"));
        Assert.That(student.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(student.Age, Is.EqualTo(20));
        Assert.That(student.Gender, Is.EqualTo("Male"));
        Assert.That(student.Status, Is.EqualTo("Deactivated"));
    }

    [Test]
    public async Task DeactivateStudent_ShouldThrow_WhenStudentIsNull()
    {
        _studentRepositoryMock.Setup(t => t.Get(It.IsAny<int>())).ReturnsAsync((Student?)null);
        var studentService = new StudentService(_studentRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () =>
                await studentService.DeactivateStudent(1));

        Assert.That(ex.Message, Is.EqualTo("Student not found"));
    }

    [Test]
    public async Task DeactivateStudent_ShouldThrow_WhenStudentAlreadyDeactivated()
    {
        Student student = new()
        {
            Email = "johndoe@gmail.com",
            Name = "John Doe",
            Age = 20,
            Gender = "Male",
            Status = "Deactivated",
            StudentId = 1
        };
        _studentRepositoryMock.Setup(t => t.Get(It.IsAny<int>())).ReturnsAsync(student);
        var studentService = new StudentService(_studentRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () => await studentService.DeactivateStudent(1));

        Assert.That(ex.Message, Is.EqualTo("Student already deactivated"));
    }

    [Test]
    public async Task GetAllActiveStudents_ShouldReturnAllStudents()
    {
        List<Student> students = new()
        {
            new Student() {StudentId=1, Name="John Doe",Email="johndoe@gmail.com",Age=20,Gender="Male",Status="Active"},
            new Student() {StudentId=2, Name="Jane Doe",Email="janedoe@gmail.com",Age=22,Gender="Female",Status="Deactivated"}
        };
        _studentRepositoryMock.Setup(t => t.GetAll()).ReturnsAsync(students);
        var studentService = new StudentService(_studentRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);

        var result = await studentService.GetAllActiveStudents(1,10);

        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result[0].StudentId, Is.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("John Doe"));
        Assert.That(result[0].Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result[0].Age, Is.EqualTo(20));
        Assert.That(result[0].Gender, Is.EqualTo("Male"));
        Assert.That(result[0].Status, Is.EqualTo("Active"));
    }

    [Test]
    public async Task GetAllActiveStudents_ShouldThrow_WhenStudentListIsNull()
    {
        _studentRepositoryMock.Setup(t => t.GetAll()).ReturnsAsync((List<Student>)null);
        var studentService = new StudentService(_studentRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () => await studentService.GetAllActiveStudents(1,10));

        Assert.That(ex.Message, Is.EqualTo("No students found"));
    }

    [Test]
    public async Task GetStudent_ShouldReturnStudentWithSpecifiedId()
    {
        Student student = new()
        {
            StudentId = 1,
            Name = "John Doe",
            Email = "johndoe@gmail.com",
            Age = 20,
            Gender = "Male",
            Status = "Active"
        };
        _studentRepositoryMock.Setup(t => t.Get(1)).ReturnsAsync(student);
        var studentService = new StudentService(_studentRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);

        var result = await studentService.GetStudent(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.StudentId, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("John Doe"));
        Assert.That(result.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Age, Is.EqualTo(20));
        Assert.That(result.Gender, Is.EqualTo("Male"));
        Assert.That(result.Status, Is.EqualTo("Active"));
    }

    [Test]
    public async Task GetStudent_ShouldThrow_WhenStudentWithSpecifiedIdIsNull()
    {
        _studentRepositoryMock.Setup(t => t.Get(1)).ReturnsAsync((Student)null);
        var studentService = new StudentService(_studentRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () => await studentService.GetStudent(1));

        Assert.That(ex.Message, Is.EqualTo("Student not found"));
    }
}