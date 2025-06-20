using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Services;
using Moq;

public class OwnerServiceTests
{
    private Mock<IRepository<int, Student>> _studentRepositoryMock;
    private Mock<IRepository<int, Teacher>> _teacherRepositoryMock;
    private Mock<IRepository<int, Session>> _sessionRepositoryMock;
    [SetUp]
    public void Setup()
    {
        _studentRepositoryMock = new();
        _teacherRepositoryMock = new();
        _sessionRepositoryMock = new();
    }

    [Test]
    public async Task IsOwnerOfResource_StudentResourceAndValidOwner_ReturnsTrue()
    {
        var student = new Student { StudentId = 1, Email = "johndoe@gmail.com" };
        _studentRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(student);
        var ownerService = new OwnerService(_teacherRepositoryMock.Object, _studentRepositoryMock.Object, _sessionRepositoryMock.Object);

        var result = await ownerService.IsOwnerOfResource("johndoe@gmail.com", "Student", 1);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task IsOwnerOfResource_StudentResourceAndInValidOwner_ReturnsFalse()
    {
        var student = new Student { StudentId = 1, Email = "johndoe@gmail.com" };
        _studentRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(student);
        var ownerService = new OwnerService(_teacherRepositoryMock.Object, _studentRepositoryMock.Object, _sessionRepositoryMock.Object);

        var result = await ownerService.IsOwnerOfResource("janedoe@gmail.com", "Student", 1);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsOwnerOfResource_TeacherResourceAndValidOwner_ReturnsTrue()
    {
        var teacher = new Teacher { TeacherId = 1, Email = "johndoe@gmail.com" };
        _teacherRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(teacher);
        var ownerService = new OwnerService(_teacherRepositoryMock.Object, _studentRepositoryMock.Object, _sessionRepositoryMock.Object);

        var result = await ownerService.IsOwnerOfResource("johndoe@gmail.com", "Teacher", 1);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task IsOwnerOfResource_TeacherResourceAndInValidOwner_ReturnsFalse()
    {
        var teacher = new Teacher { TeacherId = 1, Email = "johndoe@gmail.com" };
        _teacherRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(teacher);
        var ownerService = new OwnerService(_teacherRepositoryMock.Object, _studentRepositoryMock.Object, _sessionRepositoryMock.Object);

        var result = await ownerService.IsOwnerOfResource("janedoe@gmail.com", "Teacher", 1);

        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task IsOwnerOfResource_UnknownResourceType_ReturnsFalse()
    {
        var ownerService = new OwnerService(_teacherRepositoryMock.Object, _studentRepositoryMock.Object, _sessionRepositoryMock.Object);

        var result = await ownerService.IsOwnerOfResource("johndoe@gmail.com", "Unknown", 1);

        Assert.That(result, Is.False);
    }
}