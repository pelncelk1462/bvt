using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace bvtwebsite.Pages
{
    public class GalleryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GalleryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<GalleryItem> GalleryItems { get; set; } = new List<GalleryItem>();

        public async Task OnGetAsync()
        {
            GalleryItems = await _context.GalleryItems
                .Where(g => g.IsPublished)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();
        }
    }
}