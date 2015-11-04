using System;
using System.Collections.Generic;


namespace PhotoContest.App.ViewModels
{
    public class ActiveViewModel
    {
        public IEnumerable<ContestViewModel> ActiveContests { get; set; }

        public IEnumerable<NotificationViewModel> Notifications { get; set; }
        
    }
}