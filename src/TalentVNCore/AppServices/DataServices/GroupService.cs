using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentVN.ApplicationCore.Entities;
using TalentVN.ApplicationCore.Interfaces;
using TalentVN.Infrastructure.Data;

namespace AppServices.DataServices
{
    public class GroupService : IGroupService
    {
        private readonly IAsyncRepository<Group> _groupRepository;
        private readonly IAppLogger<GroupService> _logger;
        private readonly AppDbContext _context;

        public GroupService(IAsyncRepository<Group> asyncRepository, AppDbContext context, IAppLogger<GroupService> logger)
        {
            _groupRepository = asyncRepository;
            _logger = logger;
            _context = context;
        }

        public IEnumerable<Student> AvailableStudents(string groupID)
        {
            var studentDB = _context.Students.Include(x => x.Account).Include(g => g.Account.GroupAccounts).Where(x => !x.Account.GroupAccounts.Any(a => a.GroupID == groupID));

            return studentDB.ToList();
        }

        public IEnumerable<Student> GroupStudents(string groupID)
        {
            var studentDB = _context.Students.Include(x => x.Account).Include(g => g.Account.GroupAccounts).Where(x => x.Account.GroupAccounts.Any(a => a.GroupID == groupID));

            return studentDB.ToList();
        }

        public async Task<bool> AddNewStudent(string groupID, string studentID)
        {
            var @group = await _context.Groups.FindAsync(groupID);
            var @student = await _context.Students.Include(s => s.Account).SingleOrDefaultAsync(s => s.StudentID == studentID);

            if (@group == null || @student == null)
            {
                return false;
            }
            else
            {
                @group.GroupAccounts.Add(new GroupAccount { Account = @student.Account, Group = @group });

                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> AddNewStudents(string groupID, List<string> studentIDs)
        {
            var group = await _context.Groups.FindAsync(groupID);
            var students = await _context.Students.Include(s => s.Account).Where(s => studentIDs.Contains(s.StudentID)).ToListAsync();

            if (group == null || students == null)
            {
                return false;
            }
            else
            {
                foreach(var student in @students)
                {
                    @group.GroupAccounts.Add(new GroupAccount { Account = student.Account, Group = @group });
                }              

                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> RemoveStudent(string groupID, string accountID)
        {
            var groupAccount = await _context.GroupAccounts.SingleOrDefaultAsync(x => x.AccountID == accountID && x.GroupID == groupID);

            if (groupAccount != null)
            {
                _context.GroupAccounts.Remove(groupAccount);
                await _context.SaveChangesAsync();
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RemoveStudents(string groupID, List<string> studentIDs)
        {
            var students = _context.Students.Include(x => x.Account).Where(s => studentIDs.Contains(s.StudentID));

            var groupAccounts = await _context.GroupAccounts.Where(x => students.Any(s => s.AccountID == x.AccountID) && x.GroupID == groupID).ToListAsync();

            if (groupAccounts != null)
            {
                _context.GroupAccounts.RemoveRange(groupAccounts);
                await _context.SaveChangesAsync();
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
