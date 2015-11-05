


namespace PhotoContest.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using Data.UnitOfWork;
    using AutoMapper.QueryableExtensions;
    using ViewModels;
    using Models;

    [Authorize(Roles = "Administrator")]
    public class HomeAdminController : BaseAdminController
    {
        public HomeAdminController(IPhotoContestData data) : base(data)
        {
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            var allUsers = this.Data.Users
                            .All()
                            .OrderBy(x => x.UserName)
                            .ProjectTo<MinifiedUserViewModel>()
                            .ToList();

            var allContests = this.Data.Contests
                           .All()
                           .OrderByDescending(x => x.DateEnd)
                           .ProjectTo<ContestViewModel>()
                           .ToList();

            var model = new HomeAdminViewModel { Users = allUsers, Contests = allContests };

            return this.View(model);
        }

    }
}