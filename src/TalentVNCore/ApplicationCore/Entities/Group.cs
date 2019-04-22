using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TalentVN.ApplicationCore.Entities
{
    public class Group : BaseEntity
    {
        public Group()
        {
            this.GroupAccounts = new HashSet<GroupAccount>();
        }

        [Key]
        public string GroupID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<GroupAccount> GroupAccounts { get; set; }
    }
}
