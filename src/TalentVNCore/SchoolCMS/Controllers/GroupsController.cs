using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TalentVN.ApplicationCore.Entities;
using TalentVN.Infrastructure.Data;

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
        public IActionResult Index()
        {
            return View();
        }

        // GET: Accounts
        public async Task<IActionResult> Index_List([DataSourceRequest] DataSourceRequest request)
        {
            var result = await _context.Groups.ToListAsync();

            return Json(result.ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> EditingPopup_Create([DataSourceRequest] DataSourceRequest request, Group group)
        {
            if (group != null && ModelState.IsValid)
            {
                await _context.Groups.AddAsync(group);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { group }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> EditingPopup_Update([DataSourceRequest] DataSourceRequest request, Group group)
        {
            if (group != null && ModelState.IsValid)
            {
                _context.Groups.Update(group);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { group }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, Group group)
        {
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { group }.ToDataSourceResult(request, ModelState));
        }

    }
}
