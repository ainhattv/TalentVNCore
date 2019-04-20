using System;

namespace ApplicationCore.Entities
{
    public class News : BaseEntity
    {
        public string Name { get; set; }

        public string Header { get; set; }

        public DateTime CreatedDate { get; set; }

        public int AccountId { get; set; }

        public virtual Account Account { get; set; }

        public int DetailId { get; set; }

        public virtual NewsDetail NewsDetail { get; set; }
    }
}
