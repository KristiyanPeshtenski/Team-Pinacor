namespace PhotoContest.App.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Common.Mappings;
    using PhotoContest.Models;
    using PhotoContest.Models.Enums;

    public class FinishedContestViewModel : IMapFrom<Contest>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ContestStatus Status { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/d/yyyy}")]
        public DateTime DateCreated { get; set; }

        public string CreatorId { get; set; }

        public string Creator { get; set; }

        public IEnumerable<MinifiedUserViewModel> Participants { get; set; }

        public IEnumerable<PhotoViewModel> Photos { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Contest, ContestDetailsViewModel>()
                .ForMember(x => x.Creator, cnf => cnf.MapFrom(m => m.Creator.UserName));
        }
    }
}