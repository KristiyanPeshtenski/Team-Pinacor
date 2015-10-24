namespace PhotoContest.App.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;

    public class HomeController : BaseController
    {
        public HomeController(IPhotoContestData data) : base(data)
        {
        }

        public ActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "User");
            }

            return View();
        }
    }
}