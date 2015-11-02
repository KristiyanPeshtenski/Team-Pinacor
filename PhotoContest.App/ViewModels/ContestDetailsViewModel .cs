namespace PhotoContest.App.ViewModels
{
    using System;
    using Common.Mappings;
    using PhotoContest.Models;
    using AutoMapper;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using PhotoContest.Models.Enums;

    public class ContestDetailsViewModel : IMapFrom<Contest>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ContestStatus Status { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/d/yyyy}")]
        public DateTime DateCreated { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/d/yyyy}")]
        public DateTime? DateEnd { get; set; }

        public string Creator { get; set; }

        public bool IsExpired { get; set; }

        public bool IsFull { get; set; }

        public IEnumerable<PhotoViewModel> Photos { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Contest, ContestDetailsViewModel>()
                .ForMember(x => x.Creator, cnf => cnf.MapFrom(m => m.Creator.UserName))
                .ForMember(x => x.IsExpired,
                    cnf => cnf.MapFrom(c => c.DateEnd == null ? false : c.DateEnd >= DateTime.Now))
                .ForMember(x => x.IsFull,
                    cnf => cnf.MapFrom(c => c.MaximumParticipants == null ? false : c.Participants.Count >= c.MaximumParticipants));
        }
    }
}