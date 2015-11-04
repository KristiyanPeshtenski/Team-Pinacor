

namespace PhotoContest.App.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using System.Linq;
    using ViewModels;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class UserController : BaseController
    {

        public UserController(IPhotoContestData data) : base(data)
        {
        }

        // GET: User
        public ActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public ActionResult MyContests()
        {
            var currentUserId = this.User.Identity.GetUserId();

            var myContests = this.Data.Contests
               .All()
               .Where(x => x.Participants.Any(p => p.Id == currentUserId))
               .OrderByDescending(x => x.DateEnd)
               .Project()
               .To<ContestViewModel>();

            return View(myContests);
        }

        [Authorize]
        public ActionResult Photos()
        {
            var currentUserId = this.User.Identity.GetUserId();

            var usersPhotos = this.Data.Photos
                .All()
                .Where(x => x.AuthorId == currentUserId)
                .Project()
               .To<PhotoViewModel>();

            return View(usersPhotos);
        }
    }
}

