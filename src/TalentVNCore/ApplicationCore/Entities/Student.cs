using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Student
    {
        public string StudentID { get; set; }

        public string MSSV { get; set; }

        public string AccountID { get; set; }
        public Account Account { get; set; }
    }
}
