using AttendanceApi.Contexts;
using AttendanceApi.Models;
using AttendanceApi.Repositories;
using Microsoft.EntityFrameworkCore;

public class UserRepositoryTests
{
    private AttendanceContext _context;
    private UserRepository _repository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AttendanceContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AttendanceContext(options);
        _repository = new UserRepository(_context);
    }

    [Test]
    public async Task Add_AddsUserSuccessfully()
    {
        var user = new User { Username="johndoe@gmail.com", Password = System.Text.Encoding.UTF8.GetBytes("password"), HashKey = System.Text.Encoding.UTF8.GetBytes("hashkey"), Role="Student" };

        var result = await _repository.Add(user);

        Assert.That(result,Is.Not.Null);
        Assert.That(result.Username, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Password, Is.EqualTo(System.Text.Encoding.UTF8.GetBytes("password")));
        Assert.That(result.HashKey, Is.EqualTo(System.Text.Encoding.UTF8.GetBytes("hashkey")));
        Assert.That(result.Role, Is.EqualTo("Student"));
        Assert.That(_context.Users.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task Get_ReturnsCorrectUser()
    {
        var user = new User { Username = "johndoe@gmail.com", Password = System.Text.Encoding.UTF8.GetBytes("password"), HashKey = System.Text.Encoding.UTF8.GetBytes("hashkey"), Role = "Student" };
        _context.Users.Add(user);
        _context.SaveChanges();

        var result = await _repository.Get("johndoe@gmail.com");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Username, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Password, Is.EqualTo(System.Text.Encoding.UTF8.GetBytes("password")));
        Assert.That(result.HashKey, Is.EqualTo(System.Text.Encoding.UTF8.GetBytes("hashkey")));
        Assert.That(result.Role, Is.EqualTo("Student"));
    }

    [Test]
    public async Task GetAll_ReturnsAllUsers()
    {
        _context.Users.AddRange(
            new User { Username = "johndoe@gmail.com", Password = System.Text.Encoding.UTF8.GetBytes("password"), HashKey = System.Text.Encoding.UTF8.GetBytes("hashkey"), Role = "Student" },
            new User { Username = "janedoe@gmail.com", Password = System.Text.Encoding.UTF8.GetBytes("password"), HashKey = System.Text.Encoding.UTF8.GetBytes("hashkey"), Role = "Teacher" }
        );
        _context.SaveChanges();

        var result = (await _repository.GetAll()).ToList();

        Assert.That(result,Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Update_UpdatesUserSuccessfully()
    {
        var user = new User { Username = "johndoe@gmail.com", Password = System.Text.Encoding.UTF8.GetBytes("password"), HashKey = System.Text.Encoding.UTF8.GetBytes("hashkey"), Role = "Student" };
        _context.Users.Add(user);
        _context.SaveChanges();

        var updatedUser = new User { Username = "johndoe@gmail.com", Password = System.Text.Encoding.UTF8.GetBytes("newpassword"), HashKey = System.Text.Encoding.UTF8.GetBytes("hashkey"), Role = "Teacher" };
        var result = await _repository.Update("johndoe@gmail.com", updatedUser);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Username, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Password, Is.EqualTo(System.Text.Encoding.UTF8.GetBytes("newpassword")));
        Assert.That(result.HashKey, Is.EqualTo(System.Text.Encoding.UTF8.GetBytes("hashkey")));
        Assert.That(result.Role, Is.EqualTo("Teacher"));
    }

    [Test]
    public async Task Delete_RemovesUserSuccessfully()
    {
        var user = new User { Username = "johndoe@gmail.com", Password = System.Text.Encoding.UTF8.GetBytes("password"), HashKey = System.Text.Encoding.UTF8.GetBytes("hashkey"), Role = "Student" };
        _context.Users.Add(user);
        _context.SaveChanges();

        var result = await _repository.Delete("johndoe@gmail.com");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Username, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.Password, Is.EqualTo(System.Text.Encoding.UTF8.GetBytes("password")));
        Assert.That(result.HashKey, Is.EqualTo(System.Text.Encoding.UTF8.GetBytes("hashkey")));
        Assert.That(result.Role, Is.EqualTo("Student"));
        Assert.That(_context.Users.Count(), Is.EqualTo(0));
    }

    [Test]
    public void Delete_NonExistentUser_ThrowsException()
    {
        var ex = Assert.ThrowsAsync<Exception>(async () => await _repository.Delete("adsadas"));

        Assert.That(ex.Message, Is.EqualTo("No such item found for deleting"));
    }

    [TearDown]
    public void Dispose()
    {
        _context.Dispose();
    }
}