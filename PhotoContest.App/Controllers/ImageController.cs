using System.Data.Entity;
using AutoMapper;
using Microsoft.AspNet.Identity;

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

            return this.RedirectToAction("Details", "Contests", new { id = contestId });
            // return this.View();
        }

        [AllowAnonymous]
        public ActionResult GetImage(int imageId)
        {
            var image = this.Data.Photos
                .All()
                .Include(x => x.Votes)
                .FirstOrDefault(x => x.Id == imageId);

            if (image == null)
            {
                return HttpNotFound();
            }

            var imageViewModel = Mapper.Map<PhotoViewModel>(image);

            if (this.User.Identity.IsAuthenticated)
            {
                imageViewModel.UserHasVoted = image.Votes.Any(x => x.UserId == this.UserProfile.Id);
            }

            var url = DropBoxRepository.Download(imageViewModel.Path);
            imageViewModel.Url = url;

            return this.PartialView(imageViewModel);
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


//public ActionResult GetImage(int imageId)
//{
//    var image = this.Data.Photos
//        .All()
//        .Where(x => x.Id == imageId)
//        .Project()
//        .To<PhotoViewModel>()
//        .FirstOrDefault();

//    if (image == null)
//    {
//        return HttpNotFound();
//    }

//    var url = DropBoxRepository.Download(image.Path);
//    image.Url = url;

//    return this.PartialView(image);
//}