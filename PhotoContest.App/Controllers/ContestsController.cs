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
    using System.Data.Entity;
    using PhotoContest.Models.Enums;

    public class ContestsController : BaseController
    {
        public ContestsController(IPhotoContestData data) : base(data)
        {
        }

        [AllowAnonymous]
        public ActionResult Active()
        {
            var activeContests = this.Data.Contests
                .All()
                .Where(x => x.DateEnd > DateTime.Now || x.DateEnd == null)
                .OrderByDescending(x => x.DateCreated)
                .Project()
                .To<ContestViewModel>();

            return this.View(activeContests);
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

            if (this.User.Identity.IsAuthenticated)
            {
                ViewBag.HasUserPartisipate = this.HasParticipateInContest(id, this.UserProfile);
            }

            var contestViewModel = Mapper.Map<ContestDetailsViewModel>(contestContent);

            return View(contestViewModel);
        }

        public ActionResult AddContest()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContest(AddContestBindingModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var contest = Mapper.Map<Contest>(model);
                contest.DateCreated = DateTime.Now;
                contest.Creator = this.UserProfile;
                contest.NumberOfPrices = model.NumberOfPrices ?? 1;
                this.Data.Contests.Add(contest);
                this.Data.SaveChanges();

                return this.RedirectToAction("Details", new { id = contest.Id });
            }

            return this.View(model);
        }

        [Authorize]
        public ActionResult Created()
        {
            var userId = this.UserProfile.Id;
            var ownContests = this.Data.Contests
                .All()
                .Where(x => x.CreatorId == userId && (x.DateCreated < x.DateEnd || x.DateEnd == null))
                .Project()
                .To<ContestViewModel>();

            return this.View(ownContests);
        }

        [Authorize]
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

            if (this.HasParticipateInContest(contestId, this.UserProfile))
            {
                throw new InvalidOperationException("You already participate in this contest.");
            }

            contest.Participants.Add(this.UserProfile);
            this.Data.SaveChanges();

            return this.RedirectToAction("Details", new { id = contestId });
        }

        private bool HasParticipateInContest(int contestId, User user)
        {
            return user.ContestsParticipateIn.Any(x => x.Id == contestId);
        }

    }
}
