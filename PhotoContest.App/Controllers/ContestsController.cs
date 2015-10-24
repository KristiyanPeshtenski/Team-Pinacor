using System.Data.Entity;
using AutoMapper;
using PhotoContest.Models.Enums;

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
        public ActionResult ActiveContests()
        {
            var activeContests = this.Data.Contests
                .All()
                .Where(x => x.DateEnd > DateTime.Now || x.DateEnd == null)
                .OrderByDescending(x => x.DateCreated)
                .Project()
                .To<ContestViewModel>();

            return PartialView("_ActiveContestsPartial", activeContests);
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

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var contestContent = this.Data.Contests
                .All()
                .Include(x => x.Photos)
                .FirstOrDefault(x => x.Id == id);

            var bookmarkViewModel = Mapper.Map<ContestDetailsViewModel>(contestContent);
            return View(bookmarkViewModel);
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

                return this.RedirectToAction("Details", new { id = contest.Id });
            }

            return this.View(model);
        }

        public ActionResult OwnContests()
        {
            var userId = this.UserProfile.Id;
            var ownContests = this.Data.Contests
                .All()
                .Where(x => x.CreatorId == userId && (x.DateCreated < x.DateEnd || x.DateEnd == null))
                .Project()
                .To<ContestViewModel>();

            return this.View(ownContests);
        }

        public ActionResult Participate(int contestId)
        {
            var contest = this.Data.Contests.GetById(contestId);

            if (contest == null)
            {
                return this.HttpNotFound();
            }

            bool isInvited = contest.InvitedUsers.Contains(this.UserProfile);
            if (contest.ParticipationStrategy == ParticipationStrategy.Closed && !isInvited)
            {
                throw new InvalidOperationException("cannot participated in close contest.");
            }

            contest.Participants.Add(this.UserProfile);
            this.Data.SaveChanges();

            return this.RedirectToAction("Details", new {id = contestId});
        }
    }
}
