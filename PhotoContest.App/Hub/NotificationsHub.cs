
namespace PhotoContest.App.Hub
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    public class NotificationsHub : Hub
    {
        public void SendNotification(string notification)
        {
            this.Clients.Others.receiveNotification(notification);
        }
    }
}