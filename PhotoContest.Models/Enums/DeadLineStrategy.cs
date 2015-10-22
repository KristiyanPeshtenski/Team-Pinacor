using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Models.Enums
{
    public enum DeadLineStrategy
    {
        [Display(Name = "By time")]
        ByTime,
        [Display(Name = "By number of participants")]
        ByNumberOfParticipants
    }
}