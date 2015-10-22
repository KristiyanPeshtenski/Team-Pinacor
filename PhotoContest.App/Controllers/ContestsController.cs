namespace PhotoContest.App.Controllers
{
    using System;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using ViewModels;
    using AutoMapper;
    using BindingModels;
    using PhotoContest.Models;

    [Authorize]
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

        public ActionResult AddContest()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContest(AddContestBindingModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var contest = Mapper.Map<Contest>(model);
                contest.DateCreated = DateTime.Now;
                contest.Creator = this.UserProfile;
                this.Data.Contests.Add(contest);
                this.Data.SaveChanges();

                return this.RedirectToAction("Details", new {id = contest.Id});
            }

            return this.View(model);
        }
    }
}
