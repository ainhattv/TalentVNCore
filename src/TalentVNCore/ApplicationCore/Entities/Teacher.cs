using System;
using System.Collections.Generic;
using System.Text;

namespace TalentVN.ApplicationCore.Entities
{
    public class Teacher
    {
        public string TeacherID { get; set; }

        public string MSGV { get; set; }

        public string AccountID { get; set; }
        public Account Account { get; set; }
    }
}
