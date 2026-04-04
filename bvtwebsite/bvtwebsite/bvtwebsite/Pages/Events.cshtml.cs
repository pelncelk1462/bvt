using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace bvtwebsite.Pages
{
    public class EventsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EventsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Event> Events { get; set; } = new List<Event>();

        public async Task OnGetAsync()
        {
            Events = await _context.Events
                .Where(e => e.IsPublished && e.IsApproved)
                .OrderBy(e => e.StartDate)
                .ToListAsync();
        }
    }
}