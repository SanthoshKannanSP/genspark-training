using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AttendanceApi.Models;
using AttendanceApi.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

public class TokenServiceTests
{
    private Mock<IConfiguration> _configurationMock;
    [SetUp]
    public void Setup()
    {
        _configurationMock = new();
        _configurationMock.Setup(config => config["Keys:JwtTokenKey"]).Returns("secret_key_used_for_jwy_signing_abcd");
    }

    [Test]
    public void GenerateToken_TokenContainsCorrectClaims()
    {
        var user = new User { Username = "johndoe@gmail.com", Role = "Student" };
        var tokenService = new TokenService(_configurationMock.Object);

        var token = tokenService.GenerateToken(user);

        Assert.That(token, Is.Not.Null);
        Assert.That(token.Length, Is.GreaterThan(0));

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
    
        Assert.That(jwtToken.Claims.First(claim => claim.Type == "unique_name").Value, Is.EqualTo("johndoe@gmail.com"));
        Assert.That(jwtToken.Claims.First(claim => claim.Type == "role").Value, Is.EqualTo("Student"));
    }
}