namespace PhotoContest.App.ViewModels
{
    using Common.Mappings;
    using PhotoContest.Models;

    public class MinifiedUserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}