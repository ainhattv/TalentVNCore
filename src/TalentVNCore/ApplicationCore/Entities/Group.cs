using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }

        public virtual ICollection<GroupAccount> GroupAccounts { get; set; }

        public virtual ICollection<NotifyGroup> NotifyGroups { get; set; }
    }
}
