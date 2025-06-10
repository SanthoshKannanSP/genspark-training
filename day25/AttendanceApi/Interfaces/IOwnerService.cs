using AttendanceApi.Models;

namespace AttendanceApi.Interfaces;

public interface IOwnerService
{
    public Task<bool> IsOwnerOfResource(string username, string resourceType, int resourceId);
}