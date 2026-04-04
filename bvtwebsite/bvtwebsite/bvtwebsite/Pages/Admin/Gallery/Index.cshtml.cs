using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace bvtwebsite.Pages.Admin.Gallery
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<GalleryItem> GalleryItems { get; set; } = new List<GalleryItem>();

        public async Task OnGetAsync()
        {
            GalleryItems = await _context.GalleryItems
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();
        }
    }
}