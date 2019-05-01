using System;
using System.Collections.Generic;

namespace TalentVN.ApplicationCore.Entities
{
    public class Notify
    {
        public Notify()
        {
            this.NotifyGroups = new HashSet<NotifyGroup>();
        }

        public string NotifyID { get; set; }

        public NotifyType Type { get; set; }

        public string Message { get; set; }

        public string CreatedDate { get; } = DateTime.Today.ToString();

        public string TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        // public string From { get; set; }    // TeacherName

        // public string To { get; set; }  // GroupName

        public virtual ICollection<NotifyGroup> NotifyGroups { get; set; }

    }

    public enum NotifyType
    {
        None,
        All,
        TeacherNotify,
    }
}
