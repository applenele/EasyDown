using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace EasyDown.WebApp.SignalR
{
    public class ResourceHub : Hub
    {
        public void NewToNotify(string data)
        {
           // Clients.All.New(data);
            Clients.All.hello(data);
        }
    }
}