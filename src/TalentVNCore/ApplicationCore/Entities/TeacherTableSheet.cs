using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class TeacherTableSheet : BaseEntity
    {
        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public int TableSheetId { get; set; }

        public virtual TableSheet TableSheet { get; set; }
    }
}
