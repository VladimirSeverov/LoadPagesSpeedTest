using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace LoadPagesSpeedTest.Hubs
{
    public class ViewHub : Hub
    {
        public override Task OnConnected()
        {
            Clients.Caller.registerId(Context.ConnectionId);
            return base.OnConnected();
        }
    }
}