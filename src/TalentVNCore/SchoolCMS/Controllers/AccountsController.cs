using TalentVN.ApplicationCore.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TalentVN.Infrastructure.Data;

namespace SchoolCMS.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AppDbContext _context;

        public AccountsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Accounts
        public async Task<IActionResult> Index_List([DataSourceRequest] DataSourceRequest request)
        {
            var result = await _context.Accounts.ToListAsync();

            return Json(result.ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> EditingPopup_Create([DataSourceRequest] DataSourceRequest request, Account account)
        {
            if (account != null && ModelState.IsValid)
            {
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { account }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> EditingPopup_Update([DataSourceRequest] DataSourceRequest request, Account account)
        {
            if (account != null && ModelState.IsValid)
            {
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { account }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, Account account)
        {
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }

            return Json(new[] { account }.ToDataSourceResult(request, ModelState));
        }
    }
}
