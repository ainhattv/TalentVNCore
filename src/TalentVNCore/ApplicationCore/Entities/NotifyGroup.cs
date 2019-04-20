using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class NotifyGroup : BaseEntity
    {
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        public int NotifyId { get; set; }

        public virtual Notify Notify { get; set; }
    }
}
