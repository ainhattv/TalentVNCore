using TalentVN.ApplicationCore.Entities;
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
using Microsoft.AspNetCore.Authorization;

namespace SchoolCMS.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public IActionResult Index()
        {
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
                    Address = result.Account != null ? result.Account.Address : "",
                    AccountID = result.Account != null ? result.Account.AccountID : "",
                });
            }

            return Json(viewresults.ToDataSourceResult(request));
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

        [AcceptVerbs("Post")]
        public async Task<ActionResult> EditingPopup_Update([DataSourceRequest] DataSourceRequest request, StudentViewModel studentViewModel)
        {
            if (studentViewModel != null && ModelState.IsValid)
            {
                Student student = new Student()
                {
                    StudentID = studentViewModel.StudentID,
                    MSSV = studentViewModel.MSSV,
                    Account = new Account
                    {
                        AccountID = studentViewModel.AccountID,
                        FirstName = studentViewModel.FirstName,
                        LastName = studentViewModel.LastName,
                        Address = studentViewModel.Address
                    },

                };

                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { studentViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, StudentViewModel studentViewModel)
        {
            if (studentViewModel != null && ModelState.IsValid)
            {
                Student student = await _context.Students.SingleOrDefaultAsync(s => s.StudentID.Equals(studentViewModel.StudentID));

                if(student != null)
                    _context.Students.Remove(student);

                Account account = await _context.Accounts.SingleOrDefaultAsync(a => a.AccountID.Equals(studentViewModel.AccountID));
                if(account != null)
                    _context.Accounts.Remove(account);

                await _context.SaveChangesAsync();
            }

            return Json(new[] { studentViewModel }.ToDataSourceResult(request, ModelState));
        }

    }
}
