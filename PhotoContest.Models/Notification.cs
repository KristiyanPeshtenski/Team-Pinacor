using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        public int ContestId { get; set; }

        public Contest Contest { get; set; }

        public string SenderId { get; set; }

        public User Sender { get; set; }

        public string ReceiverId { get; set; }

        public User Receiver { get; set; }
    }
}
