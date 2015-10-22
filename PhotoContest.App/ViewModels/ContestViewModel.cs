

namespace PhotoContest.App.ViewModels
{
    using AutoMapper;
    using System;
    using PhotoContest.Models;
    using Common.Mappings;
    using PhotoContest.Models.Enums;

    public class ContestViewModel : IMapFrom<Contest>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ParticipationStrategy ParticipationStrategy { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateEnd { get; set; }

    }
}