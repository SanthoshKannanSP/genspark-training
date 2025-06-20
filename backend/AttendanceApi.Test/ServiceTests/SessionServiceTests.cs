using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AttendanceApi.Services;
using AutoMapper;
using Moq;

public class SessionServiceTests
{
    private Mock<IRepository<int, Session>> _sessionRepositoryMock;
    private Mock<IRepository<int, SessionAttendance>> _sessionAttendanceRepositoryMock;
    private Mock<IMapper> _mapperMock;

    [SetUp]
    public void Setup()
    {
        _sessionAttendanceRepositoryMock = new();
        _sessionRepositoryMock = new();
        _mapperMock = new();
    }

    [Test]
    public async Task AddStudentsToSession_ShouldAddStudentsToSession()
    {
        AddStudentsToSessionRequestDTO addStudentRequestDTO = new()
        {
            StudentIds = [1, 2]
        };

        var sessionAttendance = new SessionAttendance { StudentId = 1, SessionId = 1, Status = "NotAttended" };
        _sessionAttendanceRepositoryMock.Setup(sar => sar.Add(It.IsAny<SessionAttendance>())).ReturnsAsync(sessionAttendance);

        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _mapperMock.Object);

        var result = await sessionService.AddStudentsToSession(addStudentRequestDTO,1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task CancelSession_ValidSessionId_SessionCancelled()
    {
        var session = new Session { SessionId = 1, Status = "Scheduled" };
        var updatedSession = new Session { SessionId = 1, Status = "Cancelled" };
        _sessionRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(session);
        _sessionRepositoryMock.Setup(repo => repo.Update(1, It.IsAny<Session>())).ReturnsAsync(updatedSession);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _mapperMock.Object);


        var result = await sessionService.CancelSession(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.SessionId, Is.EqualTo(1));
        Assert.That(result.Status, Is.EqualTo("Cancelled"));
    }

    [Test]
    public void CancelSession_InvalidSessionId_ThrowsException()
    {
        _sessionRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync((Session)null);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _mapperMock.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () => await sessionService.CancelSession(1));
        Assert.That(ex.Message, Is.EqualTo("Session not found"));
    }

    [Test]
    public async Task CompleteSession_ValidSessionId_SessionCompleted()
    {
        var session = new Session { SessionId = 1, Status = "Scheduled" };
        var updatedSession = new Session { SessionId = 1, Status = "Completed" };
        _sessionRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(session);
        _sessionRepositoryMock.Setup(repo => repo.Update(1, It.IsAny<Session>())).ReturnsAsync(updatedSession);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _mapperMock.Object);


        var result = await sessionService.CompleteSession(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.SessionId, Is.EqualTo(1));
        Assert.That(result.Status, Is.EqualTo("Completed"));
    }

    [Test]
    public void CompleteSession_InvalidSessionId_ThrowsException()
    {
        _sessionRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync((Session)null);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _mapperMock.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () => await sessionService.CompleteSession(1));
        Assert.That(ex.Message, Is.EqualTo("Session not found"));
    }

    [Test]
    public async Task GetAllSession_ReturnsAllSessions()
    {
        var sessions = new List<Session>
        {
            new Session { SessionId = 1, Status = "Scheduled" },
            new Session { SessionId = 2, Status = "Cancelled" },
            new Session { SessionId = 3, Status = "Completed" }
        };

        _sessionRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(sessions);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _mapperMock.Object);

        var result = await sessionService.GetAllSession(1,10);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0].Status, Is.EqualTo("Scheduled"));
        Assert.That(result[1].Status, Is.EqualTo("Cancelled"));
        Assert.That(result[2].Status, Is.EqualTo("Completed"));
    }

    [Test]
    public async Task GetSession_ValidSessionId_ReturnsSession()
    {
        var expectedSession = new Session { SessionId = 1, Status = "Scheduled" };
        _sessionRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(expectedSession);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _mapperMock.Object);

        var result = await sessionService.GetSession(1);

        Assert.That(result.SessionId, Is.EqualTo(1));
        Assert.That(result.Status, Is.EqualTo("Scheduled"));
    }

    [Test]
    public async Task GetSessionByTeacher_ValidTeacherId_ReturnsFilteredSessions()
    {
        var sessions = new List<Session>
        {
            new Session { SessionId = 1, TeacherId = 1, Status = "Scheduled" },
            new Session { SessionId = 2, TeacherId = 2, Status = "Cancelled" },
            new Session { SessionId = 3, TeacherId = 1, Status = "Completed" }
        };

        _sessionRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(sessions);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _mapperMock.Object);

        var result = await sessionService.GetSessionByTeacher(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result[0].TeacherId, Is.EqualTo(1));
        Assert.That(result[1].TeacherId, Is.EqualTo(1));
    }

    [Test]
    public async Task ScheduleSession_ValidTeacherId_ReturnsScheduledSession()
    {
        var addSessionRequestDTO = new AddSessionRequestDTO
        {
            SessionName = "C# basics",
            Date = DateOnly.Parse("2025-08-23"),
            StartTime = TimeOnly.Parse("10:00"),
            EndTime = TimeOnly.Parse("11:00"),
            teacherId = 1
        };

        var session = new Session
        {
            SessionName = "C# basics",
            Date = DateOnly.Parse("2025-08-23"),
            StartTime = TimeOnly.Parse("10:00"),
            EndTime = TimeOnly.Parse("11:00")
        };

        var addSession = new Session
        {
            SessionId = 1,
            Status = "Scheduled",
            TeacherId = 1,
            SessionName = "C# basics",
            Date = DateOnly.Parse("2025-08-23"),
            StartTime = TimeOnly.Parse("10:00"),
            EndTime = TimeOnly.Parse("11:00")
        };

        _mapperMock.Setup(m => m.Map<Session>(addSessionRequestDTO)).Returns(session);
        _sessionRepositoryMock.Setup(repo => repo.Add(It.IsAny<Session>())).ReturnsAsync(addSession);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _mapperMock.Object);

        var result = await sessionService.ScheduleSession(addSessionRequestDTO);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.SessionId, Is.EqualTo(1));
        Assert.That(result.Status, Is.EqualTo("Scheduled"));
        Assert.That(result.TeacherId, Is.EqualTo(1));
        Assert.That(result.SessionName, Is.EqualTo("C# basics"));
        Assert.That(result.Date, Is.EqualTo(DateOnly.Parse("2025-08-23")));
        Assert.That(result.StartTime, Is.EqualTo(TimeOnly.Parse("10:00")));
        Assert.That(result.EndTime, Is.EqualTo(TimeOnly.Parse("11:00")));
    }
}