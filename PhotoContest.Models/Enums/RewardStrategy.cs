using System.ComponentModel.DataAnnotations;

namespace PhotoContest.Models.Enums
{
    public enum RewardStrategy
    {
        [Display(Name = "Single Winner")]
        SingleWinner,

        [Display(Name = "Top N Prizes")]
        TopNPrizes
    }
}