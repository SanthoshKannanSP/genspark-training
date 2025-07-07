using System.Security.Claims;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using AttendanceApi.Models.DTOs;
using AttendanceApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;

public class SessionServiceTests
{
    private Mock<IRepository<int,Session>> _sessionRepositoryMock;
    private Mock<IRepository<int,Student>> _studentRepositoryMock;
    private Mock<IRepository<int, SessionAttendance>> _sessionAttendanceRepositoryMock;
    private Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private Mock<IMapper> _mapperMock;

    [SetUp]
    public void Setup()
    {
        _sessionAttendanceRepositoryMock = new();
        _sessionRepositoryMock = new();
        _studentRepositoryMock = new();
        _httpContextAccessorMock = new();
        _mapperMock = new();
    }

    [Test]
    public async Task AddStudentsToSession_ShouldAddStudentsToSession()
    {
        var username = "student@example.com";
        var role = "Student";
        var sessionCode = "ABC123";

        var student = new Student { StudentId = 1, Email = username};
        var session = new Session { SessionId = 1, SessionCode = sessionCode };
        var expectedAttendance = new SessionAttendance
        {
            StudentId = student.StudentId,
            SessionId = session.SessionId,
            Status = "NotAttended"
        };

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };
        var identity = new ClaimsIdentity(claims, "mock");
        var principal = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = principal };

        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(context);
        _sessionRepositoryMock.Setup(repo => repo.GetAll())
            .ReturnsAsync(() => new List<Session> { session }.AsQueryable());
        _studentRepositoryMock.Setup(repo => repo.GetAll())
            .ReturnsAsync(() => new List<Student> { student }.AsQueryable());
        _sessionAttendanceRepositoryMock.Setup(repo => repo.Add(It.IsAny<SessionAttendance>()))
            .ReturnsAsync(expectedAttendance);

        var addStudentsToSessionRequestDTO = new AddStudentToSessionRequestDTO
        {
            SessionCode = sessionCode
        };

        var sessionAttendance = new SessionAttendance { StudentId = 1, SessionId = 1, Status = "NotAttended" };
        _sessionAttendanceRepositoryMock.Setup(sar => sar.Add(It.IsAny<SessionAttendance>())).ReturnsAsync(sessionAttendance);

        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _studentRepositoryMock.Object, _httpContextAccessorMock.Object, _mapperMock.Object);

        var result = await sessionService.AddStudentToSession(addStudentsToSessionRequestDTO);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].StudentId, Is.EqualTo(student.StudentId));
        Assert.That(result[0].SessionId, Is.EqualTo(session.SessionId));
        Assert.That(result[0].Status, Is.EqualTo("NotAttended"));
    }

    [Test]
    public async Task CancelSession_ValidSessionId_SessionCancelled()
    {
        var session = new Session { SessionId = 1, Status = "Scheduled" };
        var updatedSession = new Session { SessionId = 1, Status = "Cancelled" };
        _sessionRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(session);
        _sessionRepositoryMock.Setup(repo => repo.Update(1, It.IsAny<Session>())).ReturnsAsync(updatedSession);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _studentRepositoryMock.Object, _httpContextAccessorMock.Object, _mapperMock.Object);

        var result = await sessionService.CancelSession(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.SessionId, Is.EqualTo(1));
        Assert.That(result.Status, Is.EqualTo("Cancelled"));
    }

    [Test]
    public void CancelSession_InvalidSessionId_ThrowsException()
    {
        _sessionRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync((Session)null);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _studentRepositoryMock.Object, _httpContextAccessorMock.Object, _mapperMock.Object);

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
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _studentRepositoryMock.Object, _httpContextAccessorMock.Object, _mapperMock.Object);

        var result = await sessionService.CompleteSession(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.SessionId, Is.EqualTo(1));
        Assert.That(result.Status, Is.EqualTo("Completed"));
    }

    [Test]
    public void CompleteSession_InvalidSessionId_ThrowsException()
    {
        _sessionRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync((Session)null);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _studentRepositoryMock.Object, _httpContextAccessorMock.Object, _mapperMock.Object);

        var ex = Assert.ThrowsAsync<Exception>(async () => await sessionService.CompleteSession(1));
        Assert.That(ex.Message, Is.EqualTo("Session not found"));
    }

    [Test]
    public async Task GetAllSession_ReturnsAllSessions()
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

        var sessions = new List<Session>
        {
            new Session { SessionId = 1, Status = "Scheduled", TeacherEmail="johndoe@gmail.com" },
            new Session { SessionId = 2, Status = "Cancelled", TeacherEmail="johndoe@gmail.com" },
            new Session { SessionId = 3, Status = "Completed", TeacherEmail="janedoe@gmail.com" }
        };

        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(context);
        _sessionRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(sessions.AsQueryable());
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _studentRepositoryMock.Object, _httpContextAccessorMock.Object, _mapperMock.Object);

        var result = await sessionService.GetAllSession(1,10);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Data.Count, Is.EqualTo(2));
        Assert.That(result.Data[0].Status, Is.EqualTo("Scheduled"));
        Assert.That(result.Data[1].Status, Is.EqualTo("Cancelled"));
    }

    [Test]
    public async Task GetSession_ValidSessionId_ReturnsSession()
    {
        var expectedSession = new Session { SessionId = 1, Status = "Scheduled" };
        _sessionRepositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(expectedSession);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _studentRepositoryMock.Object, _httpContextAccessorMock.Object, _mapperMock.Object);

        var result = await sessionService.GetSession(1);

        Assert.That(result.SessionId, Is.EqualTo(1));
        Assert.That(result.Status, Is.EqualTo("Scheduled"));
    }

    [Test]
    public async Task GetSessionByTeacher_ValidTeacherId_ReturnsFilteredSessions()
    {
        var sessions = new List<Session>
        {
            new Session { SessionId = 1, TeacherEmail = "johndoe@gmail.com", Status = "Scheduled", MadeBy = new Teacher() {TeacherId=1, Email = "johndoe@gmail.com"} },
            new Session { SessionId = 2, TeacherEmail = "johndoe@gmail.com", Status = "Cancelled", MadeBy = new Teacher() {TeacherId=1, Email = "johndoe@gmail.com"} },
            new Session { SessionId = 3, TeacherEmail = "janedoe@gmail.com", Status = "Completed", MadeBy = new Teacher() {TeacherId=2, Email = "janedoe@gmail.com"} }
        };

        _sessionRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(sessions.AsQueryable());
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _studentRepositoryMock.Object, _httpContextAccessorMock.Object, _mapperMock.Object);

        var result = await sessionService.GetSessionByTeacher(1);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result[0].TeacherEmail, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result[1].TeacherEmail, Is.EqualTo("johndoe@gmail.com"));
    }

    [Test]
    public async Task ScheduleSession_ValidTeacherId_ReturnsScheduledSession()
    {
        var username = "johndoe@gmail.com";
         var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
        };
        var identity = new ClaimsIdentity(claims, "mock");
        var principal = new ClaimsPrincipal(identity);
        var context = new DefaultHttpContext { User = principal };

        var addSessionRequestDTO = new AddSessionRequestDTO
        {
            SessionName = "C# basics",
            Date = DateOnly.Parse("2025-08-23"),
            StartTime = TimeOnly.Parse("10:00"),
            EndTime = TimeOnly.Parse("11:00"),
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
            TeacherEmail = "johndoe@gmail.com",
            SessionName = "C# basics",
            Date = DateOnly.Parse("2025-08-23"),
            StartTime = TimeOnly.Parse("10:00"),
            EndTime = TimeOnly.Parse("11:00")
        };
        
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(context);
        _mapperMock.Setup(m => m.Map<Session>(addSessionRequestDTO)).Returns(session);
        _sessionRepositoryMock.Setup(repo => repo.Add(It.IsAny<Session>())).ReturnsAsync(addSession);
        var sessionService = new SessionService(_sessionRepositoryMock.Object, _sessionAttendanceRepositoryMock.Object, _studentRepositoryMock.Object, _httpContextAccessorMock.Object, _mapperMock.Object);


        var result = await sessionService.ScheduleSession(addSessionRequestDTO);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.SessionId, Is.EqualTo(1));
        Assert.That(result.Status, Is.EqualTo("Scheduled"));
        Assert.That(result.TeacherEmail, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(result.SessionName, Is.EqualTo("C# basics"));
        Assert.That(result.Date, Is.EqualTo(DateOnly.Parse("2025-08-23")));
        Assert.That(result.StartTime, Is.EqualTo(TimeOnly.Parse("10:00")));
        Assert.That(result.EndTime, Is.EqualTo(TimeOnly.Parse("11:00")));
    }
}