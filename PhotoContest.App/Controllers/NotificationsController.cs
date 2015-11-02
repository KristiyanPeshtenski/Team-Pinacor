namespace PhotoContest.App.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.SignalR;
    using Hub;
    using System.Collections.Generic;
    using System.Linq;
    using Data.UnitOfWork;
    using AutoMapper.QueryableExtensions;
    using ViewModels;
    using Newtonsoft.Json;

    public class NotificationsController : BaseController
    {

        public NotificationsController(IPhotoContestData data) : base(data)
        {
        }

        public ActionResult SendNotification(string notification)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();

            hubContext.Clients.All.receiveNotification(notification);

            return new EmptyResult();
            //return this.Content("Notification sent.<br />");
        }

        public ActionResult SearchUser(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                var usersResult = this.Data.Users
                   .All()
                   .Where(x => x.UserName.StartsWith(query))
                   .OrderBy(x => x.UserName)
                   .Project()
                   .To<MinifiedUserViewModel>();

                //var jsonResult = JsonConvert.SerializeObject(usersResult).ToString();

                return this.Json(usersResult, JsonRequestBehavior.AllowGet);
            }

            return new EmptyResult();
        }
    }
}