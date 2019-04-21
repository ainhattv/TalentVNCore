using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentVN.ApplicationCore.Entities;

namespace TalentVN.SchoolCMS.ViewModels
{
    public class GroupViewModel
    {
        public Group Group { get; set; }

        public IEnumerable<Student> Students { get; set; }
    }
}
