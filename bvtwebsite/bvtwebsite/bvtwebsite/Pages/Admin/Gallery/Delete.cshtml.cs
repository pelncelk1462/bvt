using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bvtwebsite.Pages.Admin.Gallery
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GalleryItem GalleryItem { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var item = await _context.GalleryItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            GalleryItem = item;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var item = await _context.GalleryItems.FindAsync(GalleryItem.Id);

            if (item == null)
            {
                return NotFound();
            }

            _context.GalleryItems.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}