using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TalentVN.ApplicationCore.Entities;
using TalentVN.Infrastructure.Data;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using TalentVN.SchoolCMS.ViewModels;

namespace SchoolCMS.Controllers
{
    public class TeachersController : Controller
    {
        private readonly AppDbContext _context;

        public TeachersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public IActionResult Index()
        {
            return View();
        }

        // GET: Students
        public async Task<IActionResult> Index_List([DataSourceRequest] DataSourceRequest request)
        {
            var appDbContext = _context.Teachers.Include(s => s.Account);

            var results = await appDbContext.ToListAsync();

            var viewresults = new List<TeacherViewModel>();

            foreach (var result in results)
            {
                viewresults.Add(new TeacherViewModel
                {
                    TeacherID = result.TeacherID,
                    MSGV = result.MSGV,
                    FirstName = result.Account != null ? result.Account.FirstName : "",
                    LastName = result.Account != null ? result.Account.LastName : "",
                    Address = result.Account != null ? result.Account.Address : "",
                    AccountID = result.Account != null ? result.Account.AccountID : "",
                });
            }

            return Json(viewresults.ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> EditingPopup_Create([DataSourceRequest] DataSourceRequest request, TeacherViewModel teacherViewModel)
        {
            if (teacherViewModel != null && ModelState.IsValid)
            {
                Teacher teacher = new Teacher()
                {
                    TeacherID = Guid.NewGuid().ToString(),
                    MSGV = teacherViewModel.MSGV,
                    Account = new Account
                    {
                        AccountID = Guid.NewGuid().ToString(),
                        FirstName = teacherViewModel.FirstName,
                        LastName = teacherViewModel.LastName,
                        Address = teacherViewModel.Address
                    },

                };

                await _context.Teachers.AddAsync(teacher);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { teacherViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> EditingPopup_Update([DataSourceRequest] DataSourceRequest request, TeacherViewModel teacherViewModel)
        {
            if (teacherViewModel != null && ModelState.IsValid)
            {
                Teacher teacher = new Teacher()
                {
                    TeacherID = teacherViewModel.TeacherID,
                    MSGV = teacherViewModel.MSGV,
                    Account = new Account
                    {
                        AccountID = teacherViewModel.AccountID,
                        FirstName = teacherViewModel.FirstName,
                        LastName = teacherViewModel.LastName,
                        Address = teacherViewModel.Address
                    },

                };

                _context.Teachers.Update(teacher);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { teacherViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, TeacherViewModel teacherViewModel)
        {
            if (teacherViewModel != null && ModelState.IsValid)
            {
                Teacher teacher = await _context.Teachers.SingleOrDefaultAsync(s => s.TeacherID.Equals(teacherViewModel.TeacherID));

                if (teacher != null)
                    _context.Teachers.Remove(teacher);

                Account account = await _context.Accounts.SingleOrDefaultAsync(a => a.AccountID.Equals(teacherViewModel.AccountID));
                if (account != null)
                    _context.Accounts.Remove(account);

                await _context.SaveChangesAsync();
            }

            return Json(new[] { teacherViewModel }.ToDataSourceResult(request, ModelState));
        }

    }
}
