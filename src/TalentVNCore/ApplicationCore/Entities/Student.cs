using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Student : BaseEntity
    {
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }

        public virtual ICollection<StudentTableSheet> StudentTableSheets { get; set; }
    }
}
