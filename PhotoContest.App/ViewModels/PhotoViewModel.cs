namespace PhotoContest.App.ViewModels
{
    using Common.Mappings;
    using PhotoContest.Models;
    using System.Linq;
    using AutoMapper;

    public class PhotoViewModel : IMapFrom<Photo>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string DateAdded { get; set; }

        public int ContestId { get; set; }

        public int Votes { get; set; }

        public bool UserHasVoted { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Photo, PhotoViewModel>()
               .ForMember(x => x.Votes, cnf => cnf.MapFrom(m => m.Votes.Any() ? m.Votes.Sum(v => v.Value) : 0))
               .ForMember(x => x.Author, cnf => cnf.MapFrom(m => m.Author.UserName));
        }
    }
}