using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class TableSheet : BaseEntity
    {
        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public virtual ICollection<TableRowSheet> TableRowSheets { get; set; }

        public virtual ICollection<StudentTableSheet> StudentTableSheets { get; set; }


    }
}
