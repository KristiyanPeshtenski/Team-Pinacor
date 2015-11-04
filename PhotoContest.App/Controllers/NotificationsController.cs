
using System;
using System.Web.UI.WebControls;

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
    using PhotoContest.Models;

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

        public ActionResult Invite(string senderName, string receiverName, int contestId)
        {
            var contest = this.Data.Contests.GetById(contestId);

            if (contest == null)
            {
                return this.HttpNotFound();
            }

            if (contest.Participants.Any(x => x.UserName == receiverName))
            {
                throw new InvalidOperationException("User already participates in this contest.");
            }


            var invitedUser = this.Data.Users.All().FirstOrDefault(x => x.UserName == receiverName);
            if (invitedUser == null)
            {
                return this.HttpNotFound();
            }


            contest.Participants.Add(invitedUser);

            var invitation = new Notification
            {
                ContestId = contestId,
                IsRead = false,
                Sender = this.Data.Users.All().FirstOrDefault(x => x.UserName == senderName),
                Receiver = this.Data.Users.All().FirstOrDefault(x => x.UserName == receiverName),
                Message = "You have been invited to participate in contest"
            };

            invitedUser.ReceivedNotifications.Add(invitation);
            //this.Data.Notifications.Add(invitation);
            this.Data.SaveChanges();

            return new EmptyResult();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ReadNotification(int notificationId)
        {
            var notification = this.Data.Notifications.GetById(notificationId);

            if (notification == null)
            {
                return this.HttpNotFound();
            }

            notification.IsRead = true;
            this.Data.SaveChanges();

            return this.Content("");
        }
    }
}