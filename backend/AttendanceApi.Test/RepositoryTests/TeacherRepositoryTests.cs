using AttendanceApi.Contexts;
using AttendanceApi.Models;
using AttendanceApi.Repositories;
using Microsoft.EntityFrameworkCore;

public class TeacherRepositoryTests
{
    private AttendanceContext _context;
    private TeacherRepository _repository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AttendanceContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AttendanceContext(options);
        _repository = new TeacherRepository(_context);
    }

    [Test]
    public async Task Add_AddsTeacherSuccessfully()
    {
        var teacher = new Teacher { Name = "John Doe", Email="johndoe@gmail.com",Status="Active",TeacherId=1 };

        var result = await _repository.Add(teacher);

        Assert.That(result,Is.Not.Null);
        Assert.That(result.TeacherId, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("John Doe"));
        Assert.That(result.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Status, Is.EqualTo("Active"));
        Assert.That(_context.Teachers.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task Get_ReturnsCorrectTeacher()
    {
        var teacher = new Teacher { Name = "John Doe", Email="johndoe@gmail.com",Status="Active",TeacherId=1 };
        _context.Teachers.Add(teacher);
        _context.SaveChanges();

        var result = await _repository.Get(teacher.TeacherId);

        Assert.That(result,Is.Not.Null);
        Assert.That(result.TeacherId, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("John Doe"));
        Assert.That(result.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Status, Is.EqualTo("Active"));
    }

    [Test]
    public async Task GetAll_ReturnsAllTeachers()
    {
        _context.Teachers.AddRange(
            new Teacher { Name = "John Doe", Email="johndoe@gmail.com",Status="Active",TeacherId=1 },
            new Teacher { Name = "Jane Doe", Email="janedoe@gmail.com",Status="Deactivated",TeacherId=2 }
        );
        _context.SaveChanges();

        var result = (await _repository.GetAll()).ToList();

        Assert.That(result,Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Update_UpdatesTeacherSuccessfully()
    {
        var teacher = new Teacher { Name = "John Doe", Email="johndoe@gmail.com",Status="Active",TeacherId=1 };
        _context.Teachers.Add(teacher);
        _context.SaveChanges();

        var updatedTeacher = new Teacher { Name = "John Doe Smith", Email = "johndoe@gmail.com", Status = "Deactivated", TeacherId = 1 };
        var result = await _repository.Update(updatedTeacher.TeacherId, updatedTeacher);

        Assert.That(result,Is.Not.Null);
        Assert.That(result.TeacherId, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("John Doe Smith"));
        Assert.That(result.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Status, Is.EqualTo("Deactivated"));
    }

    [Test]
    public async Task Delete_RemovesTeacherSuccessfully()
    {
        var teacher = new Teacher { Name = "John Doe", Email = "johndoe@gmail.com", Status = "Active", TeacherId = 1 };
        _context.Teachers.Add(teacher);
        _context.SaveChanges();

        var result = await _repository.Delete(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.TeacherId, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("John Doe"));
        Assert.That(result.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Status, Is.EqualTo("Active"));
        Assert.That(_context.Teachers.Count(), Is.EqualTo(0));
    }

    [Test]
    public void Delete_NonExistentTeacher_ThrowsException()
    {
        var ex = Assert.ThrowsAsync<Exception>(async () => await _repository.Delete(10));

        Assert.That(ex.Message, Is.EqualTo("No such item found for deleting"));
    }

    [TearDown]
    public void Dispose()
    {
        _context.Dispose();
    }
}