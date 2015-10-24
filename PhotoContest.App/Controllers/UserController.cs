namespace PhotoContest.App.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;

    [Authorize]
    public class UserController : BaseController
    {

        public UserController(IPhotoContestData data) : base(data)
        {
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}