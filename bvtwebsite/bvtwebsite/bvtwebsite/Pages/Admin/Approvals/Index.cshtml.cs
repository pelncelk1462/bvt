using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace bvtwebsite.Pages.Admin.Approvals
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Event> PendingEvents { get; set; } = new List<Event>();

        public async Task OnGetAsync()
        {
            PendingEvents = await _context.Events
                .Where(e => e.Status == "PendingApproval")
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }
    }
}