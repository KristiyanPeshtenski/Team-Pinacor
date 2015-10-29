namespace PhotoContest.App.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Mappings;
    using PhotoContest.Models;
    using PhotoContest.Models.Enums;

    public class AddContestBindingModel : IMapTo<Contest>
    {
        [Required(ErrorMessage = "The {0} is Required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The {0} should be between {2} and {1}.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DateEnd { get; set; }

        public int? NumberOfPrices { get; set; }

        public int? MaximumParticipants { get; set; }

        [Required(ErrorMessage = "The {0} is Required")]
        public VotingStrategy VotingStrategy { get; set; }

        [Required(ErrorMessage = "The {0} is Required")]
        public DeadLineStrategy DeadLineStrategy { get; set; }

        [Required(ErrorMessage = "The {0} is Required")]
        public ParticipationStrategy ParticipationStrategy { get; set; }

        [Required(ErrorMessage = "The {0} is Required")]
        public RewardStrategy RewardStrategy { get; set; }
    }
}