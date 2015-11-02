namespace PhotoContest.App.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.SignalR;
    using Hub;

    public class NotificationsController : Controller
    {

        public ActionResult SendNotification(string notification)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();

            hubContext.Clients.All.receiveNotification(notification);

            return new EmptyResult();
            //return this.Content("Notification sent.<br />");
        }
    }
}