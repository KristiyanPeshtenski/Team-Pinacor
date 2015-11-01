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
        public ContestsController(IPhotoContestData data)
            : base(data)
        {
        }

        [AllowAnonymous]
        public ActionResult Active()
        {
            var activeContests = this.Data.Contests
                .All()
                .Where(x => x.Status == ContestStatus.Active);

            foreach (var contest in activeContests)
            {
                contest.Status = this.UpdateContestStatus(contest);
            }

            var updatedContests = activeContests
                .Where(x => x.Status == ContestStatus.Active)
                .Project()
                .To<ContestViewModel>();

            return this.View(updatedContests);
        }

        [AllowAnonymous]
        public ActionResult Past()
        {
            var pastContests = this.Data.Contests
                .All()
                .Where(x => x.Status == ContestStatus.Finished ||
                   x.Status == ContestStatus.Dismissed)
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

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContestBindingModel model)
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

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var contest = this.Data.Contests
                .All()
                .Where(x => x.Id == id)
                .Project()
                .To<ContestBindingModel>()
                .FirstOrDefault();

            if (contest == null)
            {
                return this.HttpNotFound();
            }
            if (contest.CreatorId != this.UserProfile.Id)
            {
                return new HttpUnauthorizedResult("Have to be contest owner to edit it.");
            }

            return this.View(contest);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ContestBindingModel model)
        {
            var contest = this.Data.Contests.GetById(id);
            if (contest == null)
            {
                return this.HttpNotFound();
            }
            if (contest.CreatorId != this.UserProfile.Id)
            {
                return new HttpUnauthorizedResult("Have to be contest owner to edit it.");
            }
            contest.Title = model.Title;
            contest.Description = model.Description;
            contest.DateEnd = model.DateEnd;
            contest.MaximumParticipants = model.MaximumParticipants;
            this.Data.SaveChanges();

            return this.RedirectToAction("Details", new { id = contest.Id });
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
                return new HttpUnauthorizedResult();
            }

            if (this.HasParticipateInContest(contestId, this.UserProfile))
            {
                throw new InvalidOperationException("You already participate in this contest.");
            }

            contest.Participants.Add(this.UserProfile);
            this.Data.SaveChanges();

            return this.RedirectToAction("Details", new { id = contestId });
        }

        public ActionResult Dismiss(int id)
        {
            var contest = this.Data.Contests.GetById(id);

            if (contest == null)
            {
                return this.HttpNotFound();
            }
            if (contest.CreatorId != this.UserProfile.Id)
            {
                return new HttpUnauthorizedResult();
            }

            contest.Status = ContestStatus.Dismissed;
            this.Data.SaveChanges();

            return this.RedirectToAction("Details", new { id = contest.Id });
        }

        //public ActionResult Finalize(int id)
        //{
        //    var contest = this.Data.Contests.GetById(id);

        //    if (contest == null)
        //    {
        //        return this.HttpNotFound();
        //    }
        //    if (contest.CreatorId != this.UserProfile.Id)
        //    {
        //        return new HttpUnauthorizedResult();
        //    }

        //    contest.Status = ContestStatus.Finished;
        //    this.Data.SaveChanges();

        //    this.GetWinners(Contest contest);
        //}


        private bool HasParticipateInContest(int contestId, User user)
        {
            return user.ContestsParticipateIn.Any(x => x.Id == contestId);
        }

        private ContestStatus UpdateContestStatus(Contest contest)
        {
            if(contest.Status != ContestStatus.Dismissed && contest.Status != ContestStatus.Finished)
            {
                if (contest.DeadLineStrategy == DeadLineStrategy.ByTime && contest.DateEnd <= DateTime.Now)
                {
                    return ContestStatus.UploadClosed;
                }
                if (contest.DeadLineStrategy == DeadLineStrategy.ByNumberOfParticipants && contest.Participants.Count >= contest.MaximumParticipants)
                {
                    return ContestStatus.ParticipationClosed;
                }

                return ContestStatus.Active;
            }

            return contest.Status;
        }
    }
}
