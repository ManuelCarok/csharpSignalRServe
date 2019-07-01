using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SignalRTest.Hubs
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public static List<Client> ConnectedUsers { get; set; } = new List<Client>();

        public void Connect(string username) {
            Client client = new Client() {
                UserName = username,
                IdUser = Context.ConnectionId
            };
            ConnectedUsers.Add(client);
            Clients.All.updateUsers(ConnectedUsers.Count(), ConnectedUsers.Select(u => u.UserName));
        }

        public void Send(string message)
        {
            var sender = ConnectedUsers.First(u => u.IdUser.Equals(Context.ConnectionId));
            Clients.All.broadcastMessage(sender.UserName, message);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var disconnectedUser = ConnectedUsers.FirstOrDefault(c => c.IdUser.Equals(Context.ConnectionId));
            ConnectedUsers.Remove(disconnectedUser);
            Clients.All.updateUsers(ConnectedUsers.Count(), ConnectedUsers.Select(u => u.UserName));
            return base.OnDisconnected(stopCalled);
        }
    }

    public class Client
    {
        public string UserName { get; set; }
        public string IdUser { get; set; }
    }
}