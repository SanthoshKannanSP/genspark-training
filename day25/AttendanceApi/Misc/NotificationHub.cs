using Microsoft.AspNetCore.SignalR;

namespace AttendanceApi.Misc;

public class NotificationHub : Hub
{
    public async Task SendMessage(string student, string session)
    {
        await Clients.All.SendAsync("ReceiveMessage", student, session);
    }
}