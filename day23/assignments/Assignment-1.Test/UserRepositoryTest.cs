using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using assignment_1.Contexts;
using assignment_1.Models;
using assignment_1.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Assignment_1.Test;

public class UserRepositoryTest
{
    public ClinicContext _context;
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ClinicContext>()
                            .UseInMemoryDatabase("TestDb")
                            .Options;
        _context = new ClinicContext(options);
    }

    [Test]
    public async Task AddDoctor()
    {
        HMACSHA256 hMACSHA256;
        hMACSHA256 = new HMACSHA256();
        var password = hMACSHA256.ComputeHash(Encoding.UTF8.GetBytes("varun123"));
        var key = hMACSHA256.Key;
        var user = new User()
        {
            Username = "Varun",
            Role = "Admin",
            Password = password,
            HashKey = key
        };
        IRepository<string, User> repository = new UserRepository(_context);
        var result = await repository.Add(user);
        Assert.That(result, Is.Not.Null, "Doctor not added");
        Assert.That(result.Username, Is.EqualTo("Varun"));
        Assert.That(result.Role, Is.EqualTo("Admin"));
        Assert.That(result.Password, Is.EqualTo(password));
        Assert.That(result.HashKey, Is.EqualTo(key));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}
