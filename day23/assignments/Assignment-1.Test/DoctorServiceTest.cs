using assignment_1.Contexts;
using assignment_1.Models;
using assignment_1.Models.DTOs;
using assignment_1.Repositories;
using assignment_1.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Assignment_1.Test;

public class DoctorServiceTest
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

    [TestCase("Varun")]
    public async Task TestGetDoctorByName(string name)
    {
        Mock<DoctorRepository> doctorRepositoryMock = new(_context);
        Mock<UserRepository> userRepositoryMock = new(_context);
        Mock<EncryptionService> encryptionServiceMock = new();
        Mock<IMapper> mapperMock = new();

        doctorRepositoryMock.Setup(drm => drm.GetAll())
                                    .ReturnsAsync(() => new List<Doctor>{
                                   new Doctor
                                        {
                                            Id = 1,
                                            Name = "Varun",
                                            YearsOfExperience = 10,
                                            Status = "Active"
                                        },
                                    new Doctor
                                        {
                                            Id = 2,
                                            Name = "Ram",
                                            YearsOfExperience = 2,
                                            Status = "Inactive"
                                        }
                            });

        DoctorService doctorService = new(doctorRepositoryMock.Object, userRepositoryMock.Object, encryptionServiceMock.Object, mapperMock.Object);

        var result = await doctorService.GetDoctorByName(name);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Varun"));
        Assert.That(result.YearsOfExperience, Is.EqualTo(10));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}