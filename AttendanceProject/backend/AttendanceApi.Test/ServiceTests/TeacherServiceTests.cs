using System.Security.Claims;
using System.Threading.Tasks;
using AttendanceApi.Contexts;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AttendanceApi.Repositories;
using AttendanceApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    private Mock<IHttpContextAccessor> _httpContextAccessor;

    [SetUp]
    public async Task Setup()
    {
        _teacherRepositoryMock = new();
        _userRepositoryMock = new();
        _encryptionServiceMock = new();
        _httpContextAccessor = new();
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
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object, _httpContextAccessor.Object);

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
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object, _httpContextAccessor.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () =>
                await teacherService.AddTeacher(addTeacherRequestDTO));

        Assert.That(ex.Message, Is.EqualTo("Unable to add User"));
    }

    [Test]
    public async Task DeactivateTeacher_ShouldSetStatusToDeactivated()
    {
        var username = "johndoe@gmail.com";
        var role = "Teacher";

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };
        var identity = new ClaimsIdentity(claims, "mock");
        var principal = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = principal };


        var createdTeacher = new List<Teacher>() { new Teacher(){
            Email = "johndoe@gmail.com",
            Name = "John Doe",
            Status = "Active",
            TeacherId = 1
        } };
        var updatedTeacher = new Teacher()
        {
            Email = "johndoe@gmail.com",
            Name = "John Doe",
            Status = "Deactivated",
            TeacherId = 1
        };

        _httpContextAccessor.Setup(x => x.HttpContext).Returns(context);
        _teacherRepositoryMock.Setup(t => t.GetAll()).ReturnsAsync(createdTeacher.AsQueryable());
        _teacherRepositoryMock.Setup(t => t.Update(1, It.IsAny<Teacher>())).ReturnsAsync(updatedTeacher);
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object, _httpContextAccessor.Object);


        var teacher = await teacherService.DeactivateTeacher();

        Assert.That(teacher.TeacherId, Is.EqualTo(1));
        Assert.That(teacher.Name, Is.EqualTo("John Doe"));
        Assert.That(teacher.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(teacher.Status, Is.EqualTo("Deactivated"));
    }

    [Test]
    public async Task DeactivateTeacher_ShouldThrow_WhenTeacherIsNull()
    {
        var username = "johndoe@gmail.com";
        var role = "Teacher";

         var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };
        var identity = new ClaimsIdentity(claims, "mock");
        var principal = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = principal };

        _httpContextAccessor.Setup(x => x.HttpContext).Returns(context);

        _teacherRepositoryMock.Setup(t => t.Get(It.IsAny<int>())).ReturnsAsync((Teacher?)null);
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object, _httpContextAccessor.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () =>
                await teacherService.DeactivateTeacher());

        Assert.That(ex.Message, Is.EqualTo("Teacher not found"));
    }

    [Test]
    public async Task DeactivateTeacher_ShouldThrow_WhenTeacherAlreadyDeactivated()
    {
        var username = "johndoe@gmail.com";
        var role = "Teacher";

         var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };
        var identity = new ClaimsIdentity(claims, "mock");
        var principal = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = principal };

        Teacher teacher = new()
        {
            TeacherId = 1,
            Name = "John Doe",
            Email = "johndoe@gmail.com",
            Status = "Deactivated"
        };

        _httpContextAccessor.Setup(x => x.HttpContext).Returns(context);
        _teacherRepositoryMock.Setup(t => t.Get(It.IsAny<int>())).ReturnsAsync(teacher);
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object, _httpContextAccessor.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () => await teacherService.DeactivateTeacher());

        Assert.That(ex.Message, Is.EqualTo("Teacher not found"));
    }

    [Test]
    public async Task GetAllActiveTeachers_ShouldReturnAllTeachers()
    {
        List<Teacher> teachers = new()
        {
            new Teacher() {TeacherId=1, Name="John Doe",Email="johndoe@gmail.com",Status="Active"},
            new Teacher() {TeacherId=2, Name="Jane Doe",Email="janedoe@gmail.com",Status="Deactivated"}
        };
        _teacherRepositoryMock.Setup(t => t.GetAll()).ReturnsAsync(teachers.AsQueryable());
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object, _httpContextAccessor.Object);

        var result = await teacherService.GetAllActiveTeachers(1,10);

        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result[0].TeacherId, Is.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("John Doe"));
        Assert.That(result[0].Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result[0].Status, Is.EqualTo("Active"));
    }

    [Test]
    public async Task GetAllActiveTeachers_ShouldThrow_WhenTeacherListIsNull()
    {
        _teacherRepositoryMock.Setup(t => t.GetAll()).ReturnsAsync(() => null);
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object, _httpContextAccessor.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () => await teacherService.GetAllActiveTeachers(1,10));

        Assert.That(ex.Message, Is.EqualTo("No teachers found"));
    }

    [Test]
    public async Task GetTeacher_ShouldReturnTeacherWithSpecifiedId()
    {
        Teacher teacher = new()
        {
            TeacherId = 1,
            Name = "John Doe",
            Email = "johndoe@gmail.com",
            Status = "Active"
        };
        _teacherRepositoryMock.Setup(t => t.Get(1)).ReturnsAsync(teacher);
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object, _httpContextAccessor.Object);

        var result = await teacherService.GetTeacher(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.TeacherId, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("John Doe"));
        Assert.That(result.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Status, Is.EqualTo("Active"));
    }

    [Test]
    public async Task GetTeacher_ShouldThrow_WhenTeacherWithSpecifiedIdIsNull()
    {
        _teacherRepositoryMock.Setup(t => t.Get(1)).ReturnsAsync((Teacher)null);
        var teacherService = new TeacherService(_teacherRepositoryMock.Object, _userRepositoryMock.Object, _encryptionServiceMock.Object, _mapperMock.Object, _httpContextAccessor.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () => await teacherService.GetTeacher(1));

        Assert.That(ex.Message, Is.EqualTo("No teacher found"));
    }
}