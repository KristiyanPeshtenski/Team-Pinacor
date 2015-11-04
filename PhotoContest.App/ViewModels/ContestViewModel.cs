namespace PhotoContest.App.ViewModels
{
    using System;
    using PhotoContest.Models;
    using Common.Mappings;
    using PhotoContest.Models.Enums;
    using System.Collections.Generic;
    using AutoMapper;

    public class ContestViewModel : IMapFrom<Contest>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CreatorId { get; set; }

        public ContestStatus Status { get; set; }

        public ParticipationStrategy ParticipationStrategy { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateEnd { get; set; }

        public int? MaximumParticipants { get; set; }

        public DeadLineStrategy DeadLineStrategy { get; set; }

        public bool IsExpired { get; set; }

        public bool IsFull { get; set; }

        public IEnumerable<MinifiedUserViewModel> Participants { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Contest, ContestViewModel>()
                .ForMember(x => x.IsExpired, cnf => cnf.MapFrom(c => c.DateEnd == null ? false :  c.DateEnd >= DateTime.Now))
                .ForMember(x => x.IsFull, cnf => cnf.MapFrom(c => c.MaximumParticipants == null ? false : c.Participants.Count >= c.MaximumParticipants));
        }
    }
}