using TalentVN.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TalentVN.ApplicationCore.Entities
{
    public class Account
    {
        [Key]
        public string AccountID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AccountType AccountType { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string RoleID { get; set; }

        // public Role Role { get; set; }

        public string Address { get; set; }

        public bool IsLogin { get; set; }

        public bool IsActive { get; set; }

        public ICollection<GroupAccount> GroupAccounts { get; set; }
    }

    public enum AccountType
    {
        none = 0,
        Student = 1,
        Teacher = 2,
    }
}
