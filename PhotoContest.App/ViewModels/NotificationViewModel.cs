
using System.Linq;
using AutoMapper;

namespace PhotoContest.App.ViewModels
{
    using PhotoContest.Common.Mappings;
    using PhotoContest.Models;

    public class NotificationViewModel : IMapFrom<Notification>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public int ContestId { get; set; }

        public string ContestTitle { get; set; }

        public string SenderId { get; set; }

        public string Sender { get; set; }

        public string ReceiverId { get; set; }

        public string Receiver { get; set; }

        public bool IsRead { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Notification, NotificationViewModel>()
               .ForMember(x => x.Sender, cnf => cnf.MapFrom(m => m.Sender.UserName))
               .ForMember(x => x.Receiver, cnf => cnf.MapFrom(m => m.Receiver.UserName));
        }
    }
}