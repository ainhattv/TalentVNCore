using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentVN.ApplicationCore.Entities;
using TalentVN.Infrastructure.Data;
using TalentVN.SchoolCMS.ViewModels;

namespace SchoolCMS.Controllers
{
    public class GroupsController : Controller
    {
        private readonly AppDbContext _context;

        public GroupsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var result = await _context.Groups.Include(g => g.GroupAccounts).ToListAsync();

            return View(result);
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.GroupID == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupID,Name,Description")] Group @group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("GroupID,Name,Description")] Group @group)
        {
            if (id != @group.GroupID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.GroupID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.GroupID == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @group = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> AddNewStudent([DataSourceRequest] DataSourceRequest request, StudentViewModel studentViewModel, string groupID)
        {
            if (groupID == null || studentViewModel == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(groupID);
            var @student = await _context.Students.Include(s => s.Account).SingleOrDefaultAsync(s => s.StudentID == studentViewModel.StudentID);


            if (@group == null || @student == null)
            {
                return NotFound();
            }
            else
            {
                @group.GroupAccounts.Add(new GroupAccount { Account = @student.Account, Group = @group });
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> RemoveStudent([DataSourceRequest] DataSourceRequest request, StudentViewModel studentViewModel, string groupID)
        {
            if (groupID == null || studentViewModel == null)
            {
                return NotFound();
            }

            var groupAccount = await _context.GroupAccounts.SingleOrDefaultAsync(x => x.AccountID == studentViewModel.AccountID && x.GroupID == groupID);

            if (groupAccount != null)
            {
                _context.GroupAccounts.Remove(groupAccount);
                await _context.SaveChangesAsync();
            }        

            return Ok();
        }

        private bool GroupExists(string id)
        {
            return _context.Groups.Any(e => e.GroupID == id);
        }

        /// <summary>
        /// Get all Students not used in a group
        /// </summary>
        /// <param name="request"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public async Task<IActionResult> AvailableStudents([DataSourceRequest] DataSourceRequest request, string groupID)
        {
            var studentDB = _context.Students.Include(x => x.Account).Include(g => g.Account.GroupAccounts);

            var results = await studentDB.Where(x => !x.Account.GroupAccounts.Any(a => a.GroupID == groupID)).ToListAsync();

            var viewresults = new List<StudentViewModel>();

            foreach (var result in results)
            {
                viewresults.Add(new StudentViewModel
                {
                    StudentID = result.StudentID,
                    MSSV = result.MSSV,
                    FirstName = result.Account != null ? result.Account.FirstName : "",
                    LastName = result.Account != null ? result.Account.LastName : "",
                    Address = result.Account != null ? result.Account.Address : "",
                    AccountID = result.Account != null ? result.Account.AccountID : "",
                });
            }

            return Json(viewresults.ToDataSourceResult(request));
        }

        /// <summary>
        /// Get all available students used in a group
        /// </summary>
        /// <param name="request"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public async Task<IActionResult> GroupStudents([DataSourceRequest] DataSourceRequest request, string groupID)
        {
            var studentDB = _context.Students.Include(x => x.Account).Include(g => g.Account.GroupAccounts);

            var results = await studentDB.Where(x => x.Account.GroupAccounts.Any(a => a.GroupID == groupID)).ToListAsync();

            var viewresults = new List<StudentViewModel>();

            foreach (var result in results)
            {
                viewresults.Add(new StudentViewModel
                {
                    StudentID = result.StudentID,
                    MSSV = result.MSSV,
                    FirstName = result.Account != null ? result.Account.FirstName : "",
                    LastName = result.Account != null ? result.Account.LastName : "",
                    Address = result.Account != null ? result.Account.Address : "",
                    AccountID = result.Account != null ? result.Account.AccountID : "",
                });
            }

            return Json(viewresults.ToDataSourceResult(request));
        }
    }
}
