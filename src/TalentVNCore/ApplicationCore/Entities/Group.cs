using System;
using System.Collections.Generic;
using System.Text;

namespace TalentVN.ApplicationCore.Entities
{
    public class Group
    {
        public string GroupID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<GroupAccount> GroupAccounts { get; set; }
    }
}
