using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bvtwebsite.Pages.Admin.Announcements
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Announcement Announcement { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);

            if (announcement == null)
            {
                return NotFound();
            }

            Announcement = announcement;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var announcementInDb = await _context.Announcements.FindAsync(Announcement.Id);

            if (announcementInDb == null)
            {
                return NotFound();
            }

            announcementInDb.Title = Announcement.Title;
            announcementInDb.Content = Announcement.Content;
            announcementInDb.IsPublished = Announcement.IsPublished;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}