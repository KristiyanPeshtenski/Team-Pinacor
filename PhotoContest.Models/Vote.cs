namespace PhotoContest.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Vote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public int PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

    }
}