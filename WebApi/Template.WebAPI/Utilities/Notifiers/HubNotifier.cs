using Microsoft.AspNetCore.SignalR;
using Template.Application.Abstraction;
using Template.WebAPI.Hubs;


namespace Test.WebAPI.Notifiers
{
    public class HubNotifier : IHubNotifier
    {
        private readonly IHubContext<MyHub> _hubContext;

        public HubNotifier(IHubContext<MyHub> hubContext) { _hubContext = hubContext; }

        public async Task NotifyUser(string message, string connectionId) =>  await _hubContext.Clients.Client(connectionId).SendAsync("UpdateUserInfo", message);
        
    }
   
}
