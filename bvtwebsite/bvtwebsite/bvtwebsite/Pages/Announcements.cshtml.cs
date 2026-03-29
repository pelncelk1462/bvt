using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace bvtwebsite.Pages
{
    public class AnnouncementsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Announcement> Announcements { get; set; } = new List<Announcement>();

        public async Task OnGetAsync()
        {
            Announcements = await _context.Announcements
                .Where(a => a.IsPublished)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }
    }
}