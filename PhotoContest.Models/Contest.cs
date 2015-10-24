namespace PhotoContest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Contest
    {
        private ICollection<User> participants;
        private ICollection<User> winners;
        private ICollection<User> invitedUsers; 
        private ICollection<Photo> photos;

        public Contest()
        {
            this.participants = new HashSet<User>();
            this.invitedUsers = new HashSet<User>();
            this.photos = new HashSet<Photo>();
            this.winners = new HashSet<User>();
        } 

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateEnd { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public VotingStrategy VotingStrategy { get; set; }

        public DeadLineStrategy DeadLineStrategy { get; set; }

        public ParticipationStrategy ParticipationStrategy { get; set; }

        public RewardStrategy RewardStrategy { get; set; }

        public ICollection<User> Participants
        {
            get { return this.participants; }
            set { this.participants = value; }
        }

        public ICollection<User> InvitedUsers
        {
            get { return this.invitedUsers; }
            set { this.invitedUsers = value; }
        }

        public ICollection<Photo> Photos
        {
            get { return this.photos; }
            set { this.photos = value; }
        }

        public ICollection<User> Winners
        {
            get { return this.winners; }
            set { this.winners = value; }
        }
    }
}