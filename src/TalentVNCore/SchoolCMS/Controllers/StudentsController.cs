using ApplicationCore.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentVN.Infrastructure.Data;
using TalentVN.SchoolCMS.ViewModels;

namespace SchoolCMS.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            // var appDbContext = _context.Students.Include(s => s.Account);
            return View();
        }

        // GET: Students
        public async Task<IActionResult> Index_List([DataSourceRequest] DataSourceRequest request)
        {
            var appDbContext = _context.Students.Include(s => s.Account);

            var results = await appDbContext.ToListAsync();

            var viewresults = new List<StudentViewModel>();

            foreach (var result in results)
            {
                viewresults.Add(new StudentViewModel
                {
                    StudentID = result.StudentID,
                    MSSV = result.MSSV,
                    FirstName = result.Account != null ? result.Account.FirstName : "",
                    LastName = result.Account != null ? result.Account.LastName : "",
                    Address = result.Account != null ? result.Account.Address : ""
                });
            }

            return Json(viewresults.ToDataSourceResult(request));
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Account)
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["AccountID"] = new SelectList(_context.Accounts, "AccountID", "AccountID");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,MSSV,AccountID")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "AccountID", "AccountID", student.AccountID);
            return View(student);
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> EditingPopup_Create([DataSourceRequest] DataSourceRequest request, StudentViewModel studentViewModel)
        {
            if (studentViewModel != null && ModelState.IsValid)
            {
                Student student = new Student()
                {
                    StudentID = Guid.NewGuid().ToString(),
                    MSSV = studentViewModel.MSSV,
                    Account = new Account
                    {
                        AccountID = Guid.NewGuid().ToString(),
                        FirstName = studentViewModel.FirstName,
                        LastName = studentViewModel.LastName,
                        Address = studentViewModel.Address
                    },
                    
                };

                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { studentViewModel }.ToDataSourceResult(request, ModelState));
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["AccountID"] = new SelectList(_context.Accounts, "AccountID", "AccountID", student.AccountID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentID,MSSV,AccountID")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
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
            ViewData["AccountID"] = new SelectList(_context.Accounts, "AccountID", "AccountID", student.AccountID);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Account)
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
    }
}
