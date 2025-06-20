using AttendanceApi.Contexts;
using AttendanceApi.Models;
using AttendanceApi.Repositories;
using Microsoft.EntityFrameworkCore;

public class SessionRepositoryTests
{
    private AttendanceContext _context;
    private SessionRepository _repository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AttendanceContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AttendanceContext(options);
        _repository = new SessionRepository(_context);
    }

    [Test]
    public async Task Add_AddsSessionSuccessfully()
    {
        var session = new Session {  Date = DateOnly.FromDateTime(DateTime.Parse("2025-06-10")), TeacherId = 1, SessionName = "C# basics", Status="Scheduled"};

        var result = await _repository.Add(session);

        Assert.That(result,Is.Not.Null);
        Assert.That(result.SessionId, Is.EqualTo(1));
        Assert.That(result.SessionName, Is.EqualTo("C# basics"));
        Assert.That(result.Date, Is.EqualTo(DateOnly.FromDateTime(DateTime.Parse("2025-06-10"))));
        Assert.That(result.TeacherId, Is.EqualTo(1));
        Assert.That(_context.Sessions.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task Get_ReturnsCorrectSession()
    {
        var session = new Session {  Date = DateOnly.FromDateTime(DateTime.Parse("2025-06-10")), TeacherId = 1, SessionName = "C# basics", Status="Scheduled", MadeBy = new Teacher() {TeacherId=1}};
        _context.Sessions.Add(session);
        _context.SaveChanges();

        var s = _context.Sessions.Where(s => true);
        foreach (var w in s.ToList())
        {
            System.Console.WriteLine(w.SessionId);
        }

        var result = await _repository.Get(1);

        Assert.That(result,Is.Not.Null);
        Assert.That(result.SessionId, Is.EqualTo(1));
        Assert.That(result.SessionName, Is.EqualTo("C# basics"));
        Assert.That(result.Date, Is.EqualTo(DateOnly.FromDateTime(DateTime.Parse("2025-06-10"))));
        Assert.That(result.TeacherId, Is.EqualTo(1));
    }

    [Test]
    public async Task GetAll_ReturnsAllSessions()
    {
        _context.Sessions.AddRange(
            new Session { Date = DateOnly.FromDateTime(DateTime.Parse("2025-06-10")), TeacherId = 1, SessionName = "C# basics", Status="Scheduled"},
            new Session { Date = DateOnly.FromDateTime(DateTime.Parse("2025-06-22")), TeacherId = 2, SessionName = "Python basics", Status="Completed"}
        );
        _context.SaveChanges();

        var result = (await _repository.GetAll()).ToList();

        Assert.That(result,Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Update_UpdatesSessionSuccessfully()
    {
        var session = new Session { SessionId = 1, Date = DateOnly.FromDateTime(DateTime.Parse("2025-06-10")), TeacherId = 1, SessionName = "C# basics", Status="Scheduled", MadeBy = new Teacher() {TeacherId=1}};
        _context.Sessions.Add(session);
        _context.SaveChanges();

        var updatedSession = new Session {SessionId=1, Date = DateOnly.FromDateTime(DateTime.Parse("2025-06-22")), TeacherId = 1, SessionName = "Python basics", Status="Scheduled", MadeBy = new Teacher() {TeacherId=1}};
        var result = await _repository.Update(updatedSession.SessionId, updatedSession);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.SessionId, Is.EqualTo(1));
        Assert.That(result.SessionName, Is.EqualTo("Python basics"));
        Assert.That(result.Date, Is.EqualTo(DateOnly.FromDateTime(DateTime.Parse("2025-06-22"))));
        Assert.That(result.TeacherId, Is.EqualTo(1));
    }

    [Test]
    public async Task Delete_RemovesSessionSuccessfully()
    {
        var session = new Session { Date = DateOnly.FromDateTime(DateTime.Parse("2025-06-10")), TeacherId = 1, SessionName = "C# basics", Status="Scheduled", MadeBy = new Teacher() {TeacherId=1}};
        _context.Sessions.Add(session);
        _context.SaveChanges();

        var result = await _repository.Delete(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.SessionId, Is.EqualTo(1));
        Assert.That(result.SessionName, Is.EqualTo("C# basics"));
        Assert.That(result.Date, Is.EqualTo(DateOnly.FromDateTime(DateTime.Parse("2025-06-10"))));
        Assert.That(result.TeacherId, Is.EqualTo(1));
        Assert.That(_context.Sessions.Count(), Is.EqualTo(0));
    }

    [Test]
    public void Delete_NonExistentSession_ThrowsException()
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