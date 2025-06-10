using System.Threading.Tasks;
using AttendanceApi.Contexts;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AttendanceApi.Repositories;
using AttendanceApi.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

public class TeacherServiceTests
{
    private AttendanceContext _context;
    private Mock<IRepository<int, Teacher>> _teacherRepositoryMock;
    private Mock<IRepository<string, User>> _userRepositoryMock;
    private Mock<IEncryptionService> _encryptionServiceMock;
    private Mock<IMapper> _mapperMock;

    [SetUp]
    public async Task Setup()
    {
        _teacherRepositoryMock = new();
        _userRepositoryMock = new();
        _encryptionServiceMock = new();
        _mapperMock = new();
    }

    [Test]
    public async Task AddTeacher_ShouldAddUserAndTeacher()
    {
        var addTeacherRequestDTO = new AddTeacherRequestDTO
        {
            Name = "John Doe",
            Email = "johndoe@gmail.com",
            Password = "johndoe123"
        };
        var mappedUser = new User()
        {
            Username = "johndoe@gmail.com"
        };
        var createdUser = new User()
        {
            Username = "johndoe@gmail.com",
            Role = "Teacher",
            Password = System.Text.Encoding.UTF8.GetBytes("encryptedPassword"),
            HashKey = System.Text.Encoding.UTF8.GetBytes("hashKey")
        };
        var createdTeacher = new Teacher()
        {
            Email = "johndoe@gmail.com",
            Name = "John Doe",
            Status = "Active",
            TeacherId = 1
        };
        var mappedTeacher = new Teacher()
        {
            Name = "John Doe",
            Email = "johndoe@gmail.com"
        };
        var encryptedData = new EncryptModel()
        {
            Data = "johndoe123",
            EncryptedData = System.Text.Encoding.UTF8.GetBytes("encryptedPassword"),
            HashKey = System.Text.Encoding.UTF8.GetBytes("hashKey")
        };
        _userRepositoryMock.Setup(u => u.Add(It.IsAny<User>())).ReturnsAsync(createdUser);
        _teacherRepositoryMock.Setup(u => u.Add(It.IsAny<Teacher>())).ReturnsAsync(createdTeacher);
        _mapperMock.Setup(m => m.Map<User>(addTeacherRequestDTO)).Returns(mappedUser);
        _mapperMock.Setup(m => m.Map<Teacher>(addTeacherRequestDTO)).Returns(mappedTeacher);
        _encryptionServiceMock.Setup(e => e.EncryptData(It.IsAny<EncryptModel>())).Returns(encryptedData);
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);

        var teacher = await teacherService.AddTeacher(addTeacherRequestDTO);

        Assert.That(teacher, Is.Not.Null);
        Assert.That(teacher.TeacherId, Is.EqualTo(1));
        Assert.That(teacher.Name, Is.EqualTo("John Doe"));
        Assert.That(teacher.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(teacher.Status, Is.EqualTo("Active"));
        Assert.That(teacher.OwnerName, Is.EqualTo("johndoe@gmail.com"));
    }

    [Test]
    public async Task AddTeacher_ShouldThrow_WhenUserCreationFails()
    {
        var addTeacherRequestDTO = new AddTeacherRequestDTO
        {
            Name = "John Doe",
            Email = "johndoe@gmail.com",
            Password = "johndoe123"
        };
        var mappedUser = new User()
        {
            Username = "johndoe@gmail.com"
        };
        var mappedTeacher = new Teacher()
        {
            Name = "John Doe",
            Email = "johndoe@gmail.com"
        };
        var encryptedData = new EncryptModel()
        {
            Data = "johndoe123",
            EncryptedData = System.Text.Encoding.UTF8.GetBytes("encryptedPassword"),
            HashKey = System.Text.Encoding.UTF8.GetBytes("hashKey")
        };
        _mapperMock.Setup(m => m.Map<User>(addTeacherRequestDTO)).Returns(mappedUser);
        _mapperMock.Setup(m => m.Map<Teacher>(addTeacherRequestDTO)).Returns(mappedTeacher);
        _encryptionServiceMock.Setup(e => e.EncryptData(It.IsAny<EncryptModel>())).Returns(encryptedData);
        _userRepositoryMock.Setup(r => r.Add(It.IsAny<User>()))
                .ThrowsAsync(new Exception("Unable to add User"));
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () =>
                await teacherService.AddTeacher(addTeacherRequestDTO));

        Assert.That(ex.Message, Is.EqualTo("Unable to create Teacher"));
    }

    [Test]
    public async Task DeactivateTeacher_ShouldSetStatusToDeactivated()
    {
        var createdTeacher = new Teacher()
        {
            Email = "johndoe@gmail.com",
            Name = "John Doe",
            Status = "Active",
            TeacherId = 1
        };
        var updatedTeacher = new Teacher()
        {
            Email = "johndoe@gmail.com",
            Name = "John Doe",
            Status = "Deactivated",
            TeacherId = 1
        };
        _teacherRepositoryMock.Setup(t => t.Get(1)).ReturnsAsync(createdTeacher);
        _teacherRepositoryMock.Setup(t => t.Update(1, It.IsAny<Teacher>())).ReturnsAsync(updatedTeacher);
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);


        var teacher = await teacherService.DeactivateTeacher(1);

        Assert.That(teacher.TeacherId, Is.EqualTo(1));
        Assert.That(teacher.Name, Is.EqualTo("John Doe"));
        Assert.That(teacher.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(teacher.Status, Is.EqualTo("Deactivated"));
    }

    [Test]
    public async Task DeactivateTeacher_ShouldThrow_WhenTeacherIsNull()
    {
        _teacherRepositoryMock.Setup(t => t.Get(It.IsAny<int>())).ReturnsAsync((Teacher?)null);
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () =>
                await teacherService.DeactivateTeacher(1));
        
        Assert.That(ex.Message, Is.EqualTo("Teacher with given Id not found"));
    }
}