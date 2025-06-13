using AttendanceApi.Contexts;
using AttendanceApi.Models;
using AttendanceApi.Repositories;
using Microsoft.EntityFrameworkCore;

public class StudentRepositoryTests
{
    private AttendanceContext _context;
    private StudentRepository _repository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AttendanceContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AttendanceContext(options);
        _repository = new StudentRepository(_context);
    }

    [Test]
    public async Task Add_AddsStudentSuccessfully()
    {
        var student = new Student { Name = "John Doe", Email="johndoe@gmail.com",Status="Active",StudentId=1, Age=20, Gender="Male" };

        var result = await _repository.Add(student);

        Assert.That(result,Is.Not.Null);
        Assert.That(result.StudentId, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("John Doe"));
        Assert.That(result.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Status, Is.EqualTo("Active"));
        Assert.That(result.Age, Is.EqualTo(20));
        Assert.That(result.Gender, Is.EqualTo("Male"));
        Assert.That(_context.Students.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task Get_ReturnsCorrectStudent()
    {
        var student = new Student { Name = "John Doe", Email = "johndoe@gmail.com", Status = "Active", StudentId = 1, Age = 20, Gender = "Male" };
        _context.Students.Add(student);
        _context.SaveChanges();

        var result = await _repository.Get(student.StudentId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.StudentId, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("John Doe"));
        Assert.That(result.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Status, Is.EqualTo("Active"));
        Assert.That(result.Age, Is.EqualTo(20));
        Assert.That(result.Gender, Is.EqualTo("Male"));
    }

    [Test]
    public async Task GetAll_ReturnsAllStudents()
    {
        _context.Students.AddRange(
            new Student { Name = "John Doe", Email="johndoe@gmail.com",Status="Active",StudentId=1, Age = 20, Gender = "Male" },
            new Student { Name = "Jane Doe", Email="janedoe@gmail.com",Status="Deactivated",StudentId=2, Age = 22, Gender = "Female" }
        );
        _context.SaveChanges();

        var result = (await _repository.GetAll()).ToList();

        Assert.That(result,Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Update_UpdatesStudentSuccessfully()
    {
        var student = new Student { Name = "John Doe", Email = "johndoe@gmail.com", Status = "Active", StudentId = 1, Age = 20, Gender = "Male" };
        _context.Students.Add(student);
        _context.SaveChanges();

        var updatedStudent = new Student { Name = "Jane Doe", Email = "janedoe@gmail.com", Status = "Deactivated", StudentId = 1, Age = 22, Gender = "Female" };
        var result = await _repository.Update(updatedStudent.StudentId, updatedStudent);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.StudentId, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Jane Doe"));
        Assert.That(result.Email, Is.EqualTo("janedoe@gmail.com"));
        Assert.That(result.Status, Is.EqualTo("Deactivated"));
        Assert.That(result.Age, Is.EqualTo(22));
        Assert.That(result.Gender, Is.EqualTo("Female"));
    }

    [Test]
    public async Task Delete_RemovesStudentSuccessfully()
    {
        var student = new Student { Name = "John Doe", Email = "johndoe@gmail.com", Status = "Active", StudentId = 1, Age = 20, Gender = "Male" };
        _context.Students.Add(student);
        _context.SaveChanges();

        var result = await _repository.Delete(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.StudentId, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("John Doe"));
        Assert.That(result.Email, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Status, Is.EqualTo("Active"));
        Assert.That(result.Age, Is.EqualTo(20));
        Assert.That(result.Gender, Is.EqualTo("Male"));
        Assert.That(_context.Students.Count(), Is.EqualTo(0));
    }

    [Test]
    public void Delete_NonExistentStudent_ThrowsException()
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