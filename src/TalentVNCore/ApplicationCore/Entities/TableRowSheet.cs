using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class TableRowSheet : BaseEntity
    {
        public int CourseId { get; set; }

        // TODO: need to implement Course

        public int TableSheetId { get; set; }

        public TableSheet TableSheet { get; set; }

        public DateTime StartTime { get; set; }
    }
}
