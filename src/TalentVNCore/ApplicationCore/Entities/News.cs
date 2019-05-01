using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TalentVN.ApplicationCore.Entities
{
    public class News
    {
        [Key]
        public string NewsID { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; } = DateTime.Now;

        public string Header { get; set; }

        public string Body { get; set; }

        public string ImageUrl { get; set; }

        public NewsType NewsType { get; set; }

        public bool IsActive { get; set; }

    }

    public enum NewsType
    {
        None,
        Public,
        Private,
    }
}
