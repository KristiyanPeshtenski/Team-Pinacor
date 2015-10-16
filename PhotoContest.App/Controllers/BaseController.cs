namespace PhotoContest.App.Controllers
{
    using System.Web.Mvc;
    using Data.UnitOfWork;

    public abstract class BaseController : Controller
    {
        private IPhotoContestData data;

        protected BaseController(IPhotoContestData data)
        {
            this.data = data;
        }

        protected IPhotoContestData Data { get; private set; }
    }
}