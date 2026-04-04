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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(
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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }

            Event = eventItem;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var eventInDb = await _context.Events.FindAsync(Event.Id);

            if (eventInDb == null)
            {
                return NotFound();
            }

            eventInDb.Title = Event.Title;
            eventInDb.Summary = Event.Summary;
            eventInDb.Description = Event.Description;
            eventInDb.StartDate = Event.StartDate;
            eventInDb.EndDate = Event.EndDate;
            eventInDb.LocationName = Event.LocationName;
            eventInDb.Address = Event.Address;
            eventInDb.MapUrl = Event.MapUrl;
            eventInDb.RegistrationUrl = Event.RegistrationUrl;
            eventInDb.Capacity = Event.Capacity;
            eventInDb.IsFeatured = Event.IsFeatured;
            eventInDb.ShowOnHomePage = Event.ShowOnHomePage;
            eventInDb.SeoTitle = Event.SeoTitle;
            eventInDb.SeoDescription = Event.SeoDescription;
            eventInDb.AdminNote = Event.AdminNote;
            eventInDb.UpdatedAt = DateTime.Now;
            eventInDb.Slug = GenerateSlug(Event.Title);

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "events");
                Directory.CreateDirectory(uploadsFolder);

                var extension = Path.GetExtension(ImageFile.FileName);
                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await ImageFile.CopyToAsync(stream);

                eventInDb.ImagePath = $"/uploads/events/{fileName}";
            }

            if (User.IsInRole("Admin"))
            {
                eventInDb.IsPublished = true;
                eventInDb.IsApproved = true;
                eventInDb.Status = "Approved";
                eventInDb.ApprovedByUserId = _userManager.GetUserId(User);
                eventInDb.ApprovedAt = DateTime.Now;
            }
            else
            {
                if (SubmitAction == "send")
                {
                    eventInDb.IsPublished = false;
                    eventInDb.IsApproved = false;
                    eventInDb.Status = "PendingApproval";
                }
                else
                {
                    eventInDb.IsPublished = false;
                    eventInDb.IsApproved = false;
                    eventInDb.Status = "Draft";
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        private static string GenerateSlug(string text)
        {
            text = text.ToLowerInvariant();
            text = text.Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u")
                       .Replace("ş", "s").Replace("ö", "o").Replace("ç", "c");
            text = Regex.Replace(text, @"[^a-z0-9\s-]", "");
            text = Regex.Replace(text, @"\s+", "-").Trim('-');
            return text;
        }
    }
}