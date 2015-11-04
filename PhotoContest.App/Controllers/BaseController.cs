using System.Data.Entity;

namespace PhotoContest.App.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Routing;
    using PhotoContest.Models;

    using System.Web.Mvc;
    using Data.UnitOfWork;

    public abstract class BaseController : Controller
    {
        private IPhotoContestData data;
        private User userProfile;

        protected BaseController(IPhotoContestData data)
        {
            this.Data = data;
        }

        protected BaseController(IPhotoContestData data, User user)
            :this(data)
        {
            this.UserProfile = user;
        }

        protected User UserProfile { get; private set; }
        protected IPhotoContestData Data { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(x => x.UserName == username);

                this.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}