using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class StudentTableSheet : BaseEntity
    {
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

        public int TableSheetId { get; set; }

        public virtual TableSheet TableSheet { get; set; }
    }
}
