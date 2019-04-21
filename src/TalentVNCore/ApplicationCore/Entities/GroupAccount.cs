using System;
using System.Collections.Generic;
using System.Text;

namespace TalentVN.ApplicationCore.Entities
{
    public class GroupAccount
    {
        public string GroupID { get; set; }
        public Group Group { get; set; }

        public string AccountID { get; set; }
        public Account Account { get; set; }
    }
}
