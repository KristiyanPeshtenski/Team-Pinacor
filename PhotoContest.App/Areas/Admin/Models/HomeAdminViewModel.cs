
namespace PhotoContest.App.Areas.Admin.Models
{
    using System.Collections.Generic;
    using PhotoContest.App.ViewModels;

    public class HomeAdminViewModel
    {
        public IEnumerable<MinifiedUserViewModel> Users { get; set; }

        public IEnumerable<ContestViewModel> Contests { get; set; }
    }
}