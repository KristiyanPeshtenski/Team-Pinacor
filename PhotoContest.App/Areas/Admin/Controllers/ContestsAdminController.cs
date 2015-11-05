
using System.Linq;
using AutoMapper.QueryableExtensions;
using PhotoContest.App.BindingModels;

namespace PhotoContest.App.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;

    [Authorize(Roles = "Administrator")]
    public class ContestsAdminController : BaseAdminController
    {
        public ContestsAdminController(IPhotoContestData data) : base(data)
        {
        }

        // GET: Admin/ContestsAdmin
        [HttpGet]
        public ActionResult EditContest(int id)
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
            
            return this.View(contest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContest(int id, ContestBindingModel model)
        {
            var contest = this.Data.Contests.GetById(id);
            if (contest == null)
            {
                return this.HttpNotFound();
            }
            
            contest.Title = model.Title;
            contest.Description = model.Description;
            contest.DateEnd = model.DateEnd;
            contest.MaximumParticipants = model.MaximumParticipants;
            this.Data.SaveChanges();

            return this.RedirectToAction("Index", "HomeAdmin");
        }

    }
}