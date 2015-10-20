namespace PhotoContest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Photo
    {
        private ICollection<Vote> votes;

        public Photo()
        {
            this.votes = new HashSet<Vote>();
        }

        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        } 
    }
}