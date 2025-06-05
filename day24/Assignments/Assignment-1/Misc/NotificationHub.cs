using Microsoft.AspNetCore.SignalR;

namespace assignment_1.Misc;

public class NotificationHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage",user, message);
    }
}