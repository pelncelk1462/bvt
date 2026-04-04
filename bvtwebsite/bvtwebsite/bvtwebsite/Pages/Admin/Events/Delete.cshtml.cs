using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bvtwebsite.Pages.Admin.Events
{
    [Authorize(Roles = "Admin,Editor")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }

            Event = eventItem;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var eventItem = await _context.Events.FindAsync(Event.Id);

            if (eventItem == null)
            {
                return NotFound();
            }

            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}