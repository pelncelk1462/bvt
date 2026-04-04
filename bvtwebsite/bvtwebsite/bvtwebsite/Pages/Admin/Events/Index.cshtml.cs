using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace bvtwebsite.Pages.Admin.Events
{
    [Authorize(Roles = "Admin,Editor")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Event> Events { get; set; } = new List<Event>();

        public async Task OnGetAsync()
        {
            Events = await _context.Events
                .OrderByDescending(e => e.StartDate)
                .ToListAsync();
        }
    }
}