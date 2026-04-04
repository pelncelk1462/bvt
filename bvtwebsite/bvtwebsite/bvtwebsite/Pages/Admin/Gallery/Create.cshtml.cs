using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bvtwebsite.Pages.Admin.Gallery
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GalleryItem GalleryItem { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            GalleryItem.CreatedAt = DateTime.Now;

            _context.GalleryItems.Add(GalleryItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}