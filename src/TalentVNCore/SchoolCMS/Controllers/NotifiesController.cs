using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TalentVN.ApplicationCore.Entities;
using TalentVN.Infrastructure.Data;
using TalentVN.SchoolCMS.Services;

namespace SchoolCMS.Controllers
{
    [Authorize]
    public class NotifiesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly INotificationService _notificationService;

        public NotifiesController(AppDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        // GET: Notifies
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Notifys.Include(n => n.Teacher).Include(x => x.NotifyGroups);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Notifies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notify = await _context.Notifys
                .Include(n => n.Teacher)
                .FirstOrDefaultAsync(m => m.NotifyID == id);
            if (notify == null)
            {
                return NotFound();
            }

            return View(notify);
        }

        // GET: Notifies/Create
        public IActionResult Create()
        {
            ViewData["AvailabelGroups"] = new SelectList(_context.Groups, "GroupID", "Name");
            // ViewData["TeacherID"] = new SelectList(_context.Teachers, "TeacherID", "MSGV");
            return View();
        }

        // POST: Notifies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotifyID,Type,Message,TeacherID")] Notify notify)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notify);
                await _context.SaveChangesAsync();

                var result = _notificationService.SendNotification("ExponentPushToken[Pg382dMtG765XgVZeWXn0t]");

                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherID"] = new SelectList(_context.Teachers, "TeacherID", "TeacherID", notify.TeacherID);
            return View(notify);
        }

        // GET: Notifies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notify = await _context.Notifys.FindAsync(id);
            if (notify == null)
            {
                return NotFound();
            }
            ViewData["TeacherID"] = new SelectList(_context.Teachers, "TeacherID", "TeacherID", notify.TeacherID);
            return View(notify);
        }

        // POST: Notifies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NotifyID,Type,Message,TeacherID")] Notify notify)
        {
            if (id != notify.NotifyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notify);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotifyExists(notify.NotifyID))
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
            ViewData["TeacherID"] = new SelectList(_context.Teachers, "TeacherID", "TeacherID", notify.TeacherID);
            return View(notify);
        }

        // GET: Notifies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notify = await _context.Notifys
                .Include(n => n.Teacher)
                .FirstOrDefaultAsync(m => m.NotifyID == id);
            if (notify == null)
            {
                return NotFound();
            }

            return View(notify);
        }

        // POST: Notifies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var notify = await _context.Notifys.FindAsync(id);
            _context.Notifys.Remove(notify);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotifyExists(string id)
        {
            return _context.Notifys.Any(e => e.NotifyID == id);
        }
    }
}
