namespace PhotoContest.App.Controllers
{
    using System;
    using System.Web;
    using Data.Repository;
    using Data.UnitOfWork;
    using PhotoContest.Models;
    using System.Web.Mvc;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using ViewModels;

    [Authorize]
    public class ImageController : BaseController
    {
        public ImageController(IPhotoContestData data) : base(data)
        {
        }

        // GET: Image
        public ActionResult UploadImage()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(int contestId, HttpPostedFileBase image)
        {
            var authorName = this.UserProfile.UserName;
            var path = DropBoxRepository.Upload(image.FileName, authorName, image.InputStream);
            var photo = new Photo
            {
                Name = image.FileName,
                Path = path,
                AuthorId = this.UserProfile.Id,
                ContestId = contestId,
                DateAdded = DateTime.Now,
            };
            this.Data.Photos.Add(photo);
            this.Data.SaveChanges();

            return this.View();
        }

        [AllowAnonymous]
        public ActionResult GetImage(int imageId)
        {
            var image = this.Data.Photos
                .All()
                .Where(x => x.Id == imageId)
                .Project()
                .To<PhotoViewModel>()
                .FirstOrDefault();

            if (image == null)
            {
                return HttpNotFound();
            }

            var url = DropBoxRepository.Download(image.Path);
            image.Url = url;

            return this.PartialView(image);
        }
    }
}