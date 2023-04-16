using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Services;

namespace API.Hubs {
    public class AlgoHub : Hub {
        private readonly HubsService _service;

        public AlgoHub(HubsService service) {
            _service = service;
        }

        public async void SendMessage(string to, string message) {
            Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
            /*if (Context.User == null || Context.User.Identity == null || Context.User.Identity.Name == null)
            {
                 await Clients.Caller.SendAsync("Error", "Your Id is unknown");
            }
            else
            {
                OutMessage msg = new OutMessage() { Content = message, Created = DateTime.Now };
                List<Connection> connections = await _service.getConnections(to, Context.User.Identity.Name);
                foreach (Connection connection in connections)
                {
                    await Clients.Client(connection.ConnectionID).SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
                }
            }*/
        }


        /* public override Task OnConnectedAsync()
         {
             if (Context.User == null || Context.User.Identity == null || Context.User.Identity.Name == null)
             {
                 Clients.Caller.SendAsync("Error", "Your Id is unknown");
                 return Task.CompletedTask;
             }
             _service.OnConnected(Context.User.Identity.Name, Context.ConnectionId);
             return base.OnConnectedAsync();
         }
         public override Task OnDisconnectedAsync(Exception? exception)
         {
             _service.OnDisconnected(Context.ConnectionId);
             return base.OnDisconnectedAsync(exception);
         }*/
    }
}
