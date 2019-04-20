using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Notify : BaseEntity
    {
        public int Type { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public int NotifyDetailId { get; set; }

        public virtual NotifyDetail NotifyDetail { get; set; }

        public virtual ICollection<NotifyGroup> NotifyGroups { get; set; }
    }
}
