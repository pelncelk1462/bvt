using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bvtwebsite.Pages.Admin.Approvals
{
    [Authorize(Roles = "Admin")]
    public class DetailModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Event Event { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
                return NotFound();

            Event = eventItem;
            return Page();
        }

        public async Task<IActionResult> OnPostApproveAsync()
        {
            var eventItem = await _context.Events.FindAsync(Event.Id);

            if (eventItem == null)
                return NotFound();

            eventItem.IsApproved = true;
            eventItem.IsPublished = true;
            eventItem.Status = "Approved";
            eventItem.ApprovedByUserId = _userManager.GetUserId(User);
            eventItem.ApprovedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostRejectAsync()
        {
            var eventItem = await _context.Events.FindAsync(Event.Id);

            if (eventItem == null)
                return NotFound();

            eventItem.IsApproved = false;
            eventItem.IsPublished = false;
            eventItem.Status = "Rejected";
            eventItem.AdminNote = Event.AdminNote;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}