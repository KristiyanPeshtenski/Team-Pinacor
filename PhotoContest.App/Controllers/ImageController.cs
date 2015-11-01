namespace PhotoContest.App.Controllers
{
    using AutoMapper;
    using Data.Repository;
    using Data.UnitOfWork;
    using PhotoContest.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ViewModels;

    [Authorize]
    public class ImageController : BaseController
    {
        public ImageController(IPhotoContestData data) : base(data)
        {
        }

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

            return this.RedirectToAction("Details", "Contests", new { id = contestId });
        }

        [AllowAnonymous]
        public ActionResult GetImage(int photoId)
        {
            var image = this.Data.Photos
                .All()
                .Include(x => x.Votes)
                .FirstOrDefault(x => x.Id == photoId);

            if (image == null)
            {
                return HttpNotFound();
            }

            var imageViewModel = Mapper.Map<PhotoViewModel>(image);

            if (this.User.Identity.IsAuthenticated)
            {
                imageViewModel.UserHasVoted = image.Votes.Any(x => x.UserId == this.UserProfile.Id);
            }

            imageViewModel.Url = DropBoxRepository.Download(imageViewModel.Path);
            return this.PartialView("_GetImagePartial", imageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(int photoId, int contestId)
        {
            var photo = this.Data.Photos
                .All()
                .FirstOrDefault(x => x.Id == photoId);

            if (photo != null)
            {
                var userHasVoted = photo.Votes.Any(x => x.UserId == this.UserProfile.Id);
                if (!userHasVoted)
                {
                    this.Data.Votes.Add(new Vote
                    {
                        PhotoId = photoId,
                        UserId = this.UserProfile.Id,
                        ContestId = contestId,
                        Value = 1
                    });

                    this.Data.SaveChanges();
                }

                var votesCout = photo.Votes.Sum(x => x.Value);
                return this.Content(votesCout.ToString());
            }

            return new EmptyResult();
        }
    }
}
