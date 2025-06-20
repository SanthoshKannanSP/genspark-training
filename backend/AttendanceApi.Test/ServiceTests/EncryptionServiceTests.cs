using System.Text;
using AttendanceApi.Models;
using AttendanceApi.Services;
using Moq;

public class EncryptionServiceTests
{
    private EncryptionService _encryptionService;
    [SetUp]
    public void Setup()
    {
        _encryptionService = new();
    }

    [Test]
    public void EncryptData_WithProvidedHashKey_ReturnsEncryptedDataAndKey()
    {
        // Arrange
        var data = new EncryptModel
        {
            Data = "Test data to encrypt",
            HashKey = Encoding.UTF8.GetBytes("ThisIsASecretKeyForHMAC")
        };

        // Act
        var result = _encryptionService.EncryptData(data);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.EncryptedData, Is.Not.Null);
        Assert.That(result.EncryptedData.Length, Is.GreaterThan(0));
        Assert.That(result.HashKey, Is.EqualTo(data.HashKey));
        Assert.That(result.Data, Is.EqualTo(data.Data));
    }

    [Test]
    public void EncryptData_WithoutProvidedHashKey_ReturnsEncryptedDataAndGeneratedKey()
    {
        // Arrange
        var data = new EncryptModel
        {
            Data = "Test data to encrypt",
            HashKey = null
        };

        // Act
        var result = _encryptionService.EncryptData(data);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Data, Is.EqualTo(data.Data));
        Assert.That(result.EncryptedData, Is.Not.Null);
        Assert.That(result.EncryptedData.Length, Is.GreaterThan(0));
        Assert.That(result.HashKey, Is.Not.Null);
        Assert.That(result.HashKey.Length, Is.GreaterThan(0));
    }
}