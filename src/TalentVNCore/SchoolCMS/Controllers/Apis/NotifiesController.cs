using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolCMS.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentVN.ApplicationCore.Entities;
using TalentVN.Infrastructure.Data;
using TalentVN.SchoolCMS.Services;

namespace SchoolCMS.Controllers.Apis
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NotifiesController : BaseApiController
    {
        private readonly AppDbContext _context;
        private readonly INotificationService _notificationService;

        public NotifiesController(AppDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        // POST: api/Notifies/RegisterNotify
        [HttpPost("RegisterNotify")]
        public async Task<ActionResult> RegisterNotifyAsync(NotificationRegisterModel model)
        {
            if (model == null)
            {
                return BadRequest(model);
            }
            else
            {
                var result = await _notificationService.SendNotification(model.DeviceToken);
            }

            return Json(model);
        }

        // GET: api/Notifies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notify>>> GetNotifys()
        {
            return await _context.Notifys.ToListAsync();
        }

        // GET: api/Notifies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notify>> GetNotify(string id)
        {
            var notify = await _context.Notifys.FindAsync(id);

            if (notify == null)
            {
                return NotFound();
            }

            return notify;
        }

        // PUT: api/Notifies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotify(string id, Notify notify)
        {
            if (id != notify.NotifyID)
            {
                return BadRequest();
            }

            _context.Entry(notify).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotifyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notifies
        [HttpPost]
        public async Task<ActionResult<Notify>> PostNotify(Notify notify)
        {
            _context.Notifys.Add(notify);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotify", new { id = notify.NotifyID }, notify);
        }

        // DELETE: api/Notifies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Notify>> DeleteNotify(string id)
        {
            var notify = await _context.Notifys.FindAsync(id);
            if (notify == null)
            {
                return NotFound();
            }

            _context.Notifys.Remove(notify);
            await _context.SaveChangesAsync();

            return notify;
        }

        private bool NotifyExists(string id)
        {
            return _context.Notifys.Any(e => e.NotifyID == id);
        }
    }
}
