using System.Collections.Generic;
using System.Threading.Tasks;
using TalentVN.ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IGroupService
    {
        IEnumerable<Student> AvailableStudents(string groupID);

        IEnumerable<Student> GroupStudents(string groupID);

        Task<bool> AddNewStudent(string groupID, string studentID);
        Task<bool> AddNewStudents(string groupID, List<string> studentIDs);

        Task<bool> RemoveStudent(string groupID, string accountID);
        Task<bool> RemoveStudents(string groupID, List<string> accountIDs);
    }
}
