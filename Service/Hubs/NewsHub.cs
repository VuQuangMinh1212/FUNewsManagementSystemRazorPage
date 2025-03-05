using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Hubs
{
    public class NewsHub : Hub
    {
        public async Task SendNewsUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveNewsUpdate", message);
        }

        public async Task SendNewsEdit(string message)
        {
            await Clients.All.SendAsync("ReceiveNewsEdit", message);
        }

        public async Task SendNewsDelete(string message)
        {
            await Clients.All.SendAsync("ReceiveNewsDelete", message);
        }
    }
}
