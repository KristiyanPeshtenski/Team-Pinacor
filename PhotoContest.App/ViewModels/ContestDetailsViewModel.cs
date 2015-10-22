
namespace PhotoContest.App.ViewModels
{
    using System;
    using Common.Mappings;
    using PhotoContest.Models;
    using AutoMapper;
    using System.Collections.Generic;

    public class ContestDetailsViewModel : IMapFrom<Contest>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateEnd { get; set; }

        public string Creator { get; set; }

        public IEnumerable<PhotoViewModel> Photos { get; set; }

        // Strategies ?

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Contest, ContestDetailsViewModel>()
                .ForMember(x => x.Creator, cnf => cnf.MapFrom(m => m.Creator.UserName));
        }
    }
}