using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class GroupAccount : BaseEntity
    {
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        public int AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
