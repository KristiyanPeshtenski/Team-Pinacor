namespace PhotoContest.App.Controllers
{
    using System;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using ViewModels;

    public class ContestsController : BaseController
    {
        public ContestsController(IPhotoContestData data) : base(data)
        {
        }


        [AllowAnonymous]
        public ActionResult Past()
        {
            var pastContests = this.Data.Contests
                .All()
                .Where(x => x.DateEnd < DateTime.Now)
                .OrderByDescending(x => x.DateEnd)
                .Project()
                .To<ContestViewModel>();

            return View(pastContests);
        }
    }


}
