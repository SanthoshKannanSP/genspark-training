using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace AttendanceApi.Misc;

public class NotificationHub : Hub
{
    private static readonly ConcurrentDictionary<string, HashSet<string>> userConnections = new();

    public override Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var username = httpContext?.Request.Query["username"].ToString();
        Console.WriteLine(username);
        if (!string.IsNullOrEmpty(username))
        {
            userConnections.AddOrUpdate(username,
                _ => new HashSet<string> { Context.ConnectionId },
                (_, connections) =>
                {
                    connections.Add(Context.ConnectionId);
                    return connections;
                });
        }

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        foreach (var kvp in userConnections)
        {
            if (kvp.Value.Remove(Context.ConnectionId) && kvp.Value.Count == 0)
            {
                userConnections.TryRemove(kvp.Key, out _);
            }
        }

        return base.OnDisconnectedAsync(exception);
    }

    public static HashSet<string>? GetConnections(string userId)
    {
        userConnections.TryGetValue(userId, out var connections);
        return connections;
    }

    public async Task NotifyMarkAttendance(string student, string session)
    {
        var connections = GetConnections(student);
        if (connections != null)
        {
            foreach (var connection in connections)
            {
                await Clients.Client(connection).SendAsync("AttendanceMarked", session);
            }
        }
    }
}