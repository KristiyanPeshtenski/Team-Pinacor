

namespace PhotoContest.App.Areas.Admin.Controllers
{
    using PhotoContest.Data.UnitOfWork;
    using System.Web.Mvc;

    [Authorize(Roles = "Administrator")]
    public abstract class BaseAdminController : Controller
    {
        public BaseAdminController(IPhotoContestData data)
        {
            this.Data = data;
        }

        public IPhotoContestData Data { get; set; }
    }
}