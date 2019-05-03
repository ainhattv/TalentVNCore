using ApplicationCore.Interfaces;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IGroupService _groupService;

        public GroupsController(AppDbContext context, IGroupService groupService)
        {
            _context = context;
            _groupService = groupService;
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

            return Ok(await _groupService.AddNewStudent(groupID, studentViewModel.StudentID));
        }

        /// <summary>
        /// Remove student from group
        /// </summary>
        /// <param name="request"></param>
        /// <param name="studentViewModel"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveStudent([DataSourceRequest] DataSourceRequest request, StudentViewModel studentViewModel, string groupID)
        {
            if (groupID == null || studentViewModel == null)
            {
                return NotFound();
            }
         
            return Ok(await _groupService.RemoveStudent(groupID, studentViewModel.AccountID));
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
        public IActionResult AvailableStudents([DataSourceRequest] DataSourceRequest request, string groupID)
        {
            if (groupID == null)
            {
                return NotFound();
            }

            var students = _groupService.AvailableStudents(groupID);

            var viewresults = new List<StudentViewModel>();

            foreach (var result in students)
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
        public IActionResult GroupStudents([DataSourceRequest] DataSourceRequest request, string groupID)
        {
            if (groupID == null)
            {
                return NotFound();
            }

            var students = _groupService.GroupStudents(groupID);

            var viewresults = new List<StudentViewModel>();

            foreach (var result in students)
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

        [HttpPost]
        public async Task<IActionResult> RemoveStudents(string groupID, string studentIDs)
        {
            if (groupID == null || studentIDs == null)
            {
                return NotFound();
            }

            return Ok(await _groupService.RemoveStudents(groupID, studentIDs.Split(",").ToList()));
        }
    }
}
