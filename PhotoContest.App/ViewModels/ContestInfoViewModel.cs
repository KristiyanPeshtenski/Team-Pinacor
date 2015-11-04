//namespace PhotoContest.App.ViewModels
//{
//    using Common.Mappings;
//    using PhotoContest.Models;
//    using PhotoContest.Models.Enums;
//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel.DataAnnotations;
//    using AutoMapper;

//    public class ContestInfoViewModel : IMapFrom<Contest>, IHaveCustomMappings
//    {
//        public string Title { get; set; }

//        public string Description { get; set; }

//        public ContestStatus Status { get; set; }

//        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/d/yyyy}")]
//        public DateTime DateCreated { get; set; }

//        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/d/yyyy}")]
//        public DateTime? DateEnd { get; set; }

//        public string Creator { get; set; }

//        public int? MaximumParticipants { get; set; }

//        public IEnumerable<MinifiedUserViewModel> Participants { get; set; }

//        public DeadLineStrategy DeadLineStrategy { get; set; }

//        public ParticipationStrategy ParticipationStrategy { get; set; }

//        public void CreateMappings(IConfiguration configuration)
//        {
//            configuration.CreateMap<Contest, ContestDetailsViewModel>()
//                .ForMember(x => x.Creator, cnf => cnf.MapFrom(m => m.Creator.UserName))
//                .ForMember(x => x.IsExpired,
//                    cnf => cnf.MapFrom(c => c.DateEnd == null ? false : c.DateEnd >= DateTime.Now))
//                .ForMember(x => x.IsFull,
//                    cnf => cnf.MapFrom(c => c.MaximumParticipants == null ? false : c.Participants.Count >= c.MaximumParticipants));
//        }
//    }
//}