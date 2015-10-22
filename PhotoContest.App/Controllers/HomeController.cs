namespace PhotoContest.App.Controllers
{
    using System;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using AutoMapper;
    using ViewModels;

    public class HomeController : BaseController
    {
        public HomeController(IPhotoContestData data) : base(data)
        {
        }

        public ActionResult Index()
        {
            var activeContests = this.Data.Contests
                .All()
                .Where(x => x.DateEnd > DateTime.Now || x.DateEnd == null)
                .OrderByDescending(x => x.DateCreated)
                .Project()
                .To<ContestViewModel>();

            return View(activeContests);
        }
    }
}