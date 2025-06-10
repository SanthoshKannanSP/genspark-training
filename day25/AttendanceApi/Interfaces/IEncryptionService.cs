using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IEncryptionService
{
    public EncryptModel EncryptData(EncryptModel data);
}