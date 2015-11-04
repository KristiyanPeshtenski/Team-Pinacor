using PhotoContest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoContest.App.ViewModels
{
    public class ContestWinnersViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public Photo Photo { get; set; }

        public int Place { get; set; }

    }
}