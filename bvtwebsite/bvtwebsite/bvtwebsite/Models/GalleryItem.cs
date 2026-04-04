using System.ComponentModel.DataAnnotations;

namespace bvtwebsite.Models
{
    public class GalleryItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Görsel Yolu")]
        public string ImagePath { get; set; } = string.Empty;

        [Display(Name = "Yayınlandı mı?")]
        public bool IsPublished { get; set; } = true;

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}