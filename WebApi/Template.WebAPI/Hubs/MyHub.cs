using Microsoft.AspNetCore.SignalR;

namespace Template.WebAPI.Hubs
{
    public class MyHub : Hub
    {

        public MyHub()
        {
        }

        public async Task SendData(string data)
        {

            await Clients.All.SendAsync("ReceiveData", data);
        }
    }
}
