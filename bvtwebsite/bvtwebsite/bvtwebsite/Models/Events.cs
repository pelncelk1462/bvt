using System.ComponentModel.DataAnnotations;

namespace bvtwebsite.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(160)]
        [Display(Name = "Başlık")]
        public string Title { get; set; } = string.Empty;

        [StringLength(180)]
        [Display(Name = "Kısa Özet")]
        public string Summary { get; set; } = string.Empty;

        [Required]
        [StringLength(5000)]
        [Display(Name = "Detaylı Açıklama")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = "Bitiş Tarihi")]
        public DateTime? EndDate { get; set; }

        [StringLength(200)]
        [Display(Name = "Konum Adı")]
        public string LocationName { get; set; } = string.Empty;

        [StringLength(300)]
        [Display(Name = "Adres")]
        public string? Address { get; set; }

        [StringLength(500)]
        [Display(Name = "Harita Linki")]
        public string? MapUrl { get; set; }

        [Display(Name = "Kapak Görseli")]
        public string? ImagePath { get; set; }

        [StringLength(500)]
        [Display(Name = "Başvuru Linki")]
        public string? RegistrationUrl { get; set; }

        [Display(Name = "Kontenjan")]
        public int? Capacity { get; set; }

        [Display(Name = "Öne Çıkan")]
        public bool IsFeatured { get; set; } = false;

        [Display(Name = "Ana Sayfada Göster")]
        public bool ShowOnHomePage { get; set; } = false;

        [StringLength(180)]
        [Display(Name = "Slug")]
        public string Slug { get; set; } = string.Empty;

        [StringLength(160)]
        [Display(Name = "SEO Başlığı")]
        public string? SeoTitle { get; set; }

        [StringLength(300)]
        [Display(Name = "SEO Açıklaması")]
        public string? SeoDescription { get; set; }

        [Display(Name = "Yayına Hazır")]
        public bool IsPublished { get; set; } = false;

        [Display(Name = "Admin Onayladı")]
        public bool IsApproved { get; set; } = false;

        [Display(Name = "Onaya Gönderildi")]
        public bool IsSubmittedForApproval { get; set; } = false;

        [StringLength(100)]
        [Display(Name = "İçerik Durumu")]
        public string Status { get; set; } = "Draft";
        // Draft, PendingApproval, Approved, Rejected

        [StringLength(450)]
        public string? CreatedByUserId { get; set; }

        [StringLength(450)]
        public string? ApprovedByUserId { get; set; }

        [Display(Name = "Onay Tarihi")]
        public DateTime? ApprovedAt { get; set; }

        [StringLength(1000)]
        [Display(Name = "Admin Notu")]
        public string? AdminNote { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime? UpdatedAt { get; set; }
    }
}