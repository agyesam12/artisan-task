using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtisanMarketPlace.Models
{
    // Corresponds to the Django ArtisanWorkImage model
    [Table("ArtisanWorkImages")]
    public class ArtisanWorkImage
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign Key to ArtisanWork
        [Required]
        public Guid ArtisanWorkId { get; set; }

        [Required]
        public string ImagePath { get; set; } // Store path/URL

        [MaxLength(255)]
        public string Caption { get; set; }

        public int Order { get; set; } = 0;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;


        // ==================== Navigation Properties ====================

        // Many-to-One relationship back to the ArtisanWork
        public virtual ArtisanWork Work { get; set; }
    }
}