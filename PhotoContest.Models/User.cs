namespace PhotoContest.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        private ICollection<Contest> ownContests;
        private ICollection<Contest> contestsParticipateIn;
        private ICollection<Contest> winContests; 
        private ICollection<Photo> photos;
        private ICollection<Notification> sentNotifications;
        private ICollection<Notification> receivedNotifications;

        //private ICollection<Vote> votes;
        //Add comments to photos 

        public User()
        {
            this.ownContests = new HashSet<Contest>();
            this.contestsParticipateIn = new HashSet<Contest>();
            this.photos = new HashSet<Photo>();
            this.winContests = new HashSet<Contest>();
            this.sentNotifications = new HashSet<Notification>();
            this.receivedNotifications = new HashSet<Notification>();
        }

        public virtual ICollection<Contest> OwnContests
        {
            get { return this.ownContests; }
            set { this.ownContests = value; }
        }

        public virtual ICollection<Contest> ContestsParticipateIn
        {
            get { return this.contestsParticipateIn; }
            set { this.contestsParticipateIn = value; }
        }

        public virtual ICollection<Contest> WinContests
        {
            get { return this.winContests; }
            set { this.winContests = value; }
        }

        public virtual ICollection<Photo> Photos
        {
            get { return this.photos; }
            set { this.photos = value; }
        }

        public virtual ICollection<Notification> SentNotifications
        {
            get { return this.sentNotifications; }
            set { this.sentNotifications = value; }
        }

        public virtual ICollection<Notification> ReceivedNotifications
        {
            get { return this.receivedNotifications; }
            set { this.receivedNotifications = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
