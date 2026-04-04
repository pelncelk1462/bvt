using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bvtwebsite.Pages.Admin.Gallery
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var itemInDb = await _context.GalleryItems.FindAsync(GalleryItem.Id);

            if (itemInDb == null)
            {
                return NotFound();
            }

            itemInDb.Title = GalleryItem.Title;
            itemInDb.Description = GalleryItem.Description;
            itemInDb.ImagePath = GalleryItem.ImagePath;
            itemInDb.IsPublished = GalleryItem.IsPublished;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}