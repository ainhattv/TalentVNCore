using System;
using System.Collections.Generic;
using System.Text;
using TalentVN.ApplicationCore.Entities;

namespace TalentVN.ApplicationCore.Entities
{
    public class NotifyGroup
    {
        public string GroupID { get; set; }
        public Group Group { get; set; }

        public string NotifyID { get; set; }
        public Notify Notify { get; set; }
    }
}
