using System.Text.RegularExpressions;
using bvtwebsite.Data;
using bvtwebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bvtwebsite.Pages.Admin.Events
{
    [Authorize(Roles = "Admin,Editor")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(
            ApplicationDbContext context,
            IWebHostEnvironment environment,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }

        [BindProperty]
        public Event Event { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        [BindProperty]
        public string SubmitAction { get; set; } = "draft";

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "events");
                Directory.CreateDirectory(uploadsFolder);

                var extension = Path.GetExtension(ImageFile.FileName);
                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await ImageFile.CopyToAsync(stream);

                Event.ImagePath = $"/uploads/events/{fileName}";
            }

            Event.Slug = GenerateSlug(Event.Title);
            Event.CreatedAt = DateTime.Now;
            Event.UpdatedAt = DateTime.Now;
            Event.CreatedByUserId = _userManager.GetUserId(User);

            // ADMIN ise her zaman direkt yayınlar
            if (User.IsInRole("Admin"))
            {
                Event.IsPublished = true;
                Event.IsApproved = true;
                Event.IsSubmittedForApproval = true;
                Event.Status = "Approved";
                Event.ApprovedByUserId = _userManager.GetUserId(User);
                Event.ApprovedAt = DateTime.Now;
            }
            else
            {
                // EDITOR davranışı

                if (SubmitAction == "send")
                {
                    Event.IsPublished = false;
                    Event.IsApproved = false;
                    Event.IsSubmittedForApproval = true;
                    Event.Status = "PendingApproval";
                }
                else
                {
                    Event.IsPublished = false;
                    Event.IsApproved = false;
                    Event.IsSubmittedForApproval = false;
                    Event.Status = "Draft";
                }
            }

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        private static string GenerateSlug(string text)
        {
            text = text.ToLowerInvariant();
            text = text.Replace("ı", "i")
                       .Replace("ğ", "g")
                       .Replace("ü", "u")
                       .Replace("ş", "s")
                       .Replace("ö", "o")
                       .Replace("ç", "c");

            text = Regex.Replace(text, @"[^a-z0-9\s-]", "");
            text = Regex.Replace(text, @"\s+", "-").Trim('-');

            return text;
        }
    }
}