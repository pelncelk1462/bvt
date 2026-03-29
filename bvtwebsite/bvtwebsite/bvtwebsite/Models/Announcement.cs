using System.ComponentModel.DataAnnotations;

namespace bvtwebsite.Models
{
    public class Announcement
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Content { get; set; } = string.Empty;

        [Display(Name = "Yayınlandı mı?")]
        public bool IsPublished { get; set; } = true;

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}