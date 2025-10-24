using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ArtisanMarketPlace.Models
{
    // Corresponds to the Django ArtisanWork model
    [Table("ArtisanWorks")]
    public class ArtisanWork
    {
        public enum ProjectStatus
        {
            COMPLETED, IN_PROGRESS, PLANNED
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign Key to ArtisanProfile
        [Required]
        public Guid ArtisanProfileId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Slug { get; set; }

        [Required]
        public string Description { get; set; }


        // ==================== Project Details ====================

        [MaxLength(100)]
        public string ProjectType { get; set; }

        public ProjectStatus Status { get; set; } = ProjectStatus.COMPLETED;

        [Range(1, int.MaxValue)]
        public int DurationDays { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ProjectCost { get; set; }


        // ==================== Location & Media ====================

        [MaxLength(255)]
        public string Location { get; set; }

        [Required]
        public string FeaturedImagePath { get; set; } // Store path/URL


        // ==================== Client Information ====================

        [MaxLength(255)]
        public string ClientName { get; set; }

        public string ClientTestimonial { get; set; }

        [Range(1, 5)]
        public int? ClientRating { get; set; } // Nullable int


        // ==================== Engagement & Timestamps ====================

        public int ViewsCount { get; set; } = 0;
        public int LikesCount { get; set; } = 0;

        public DateTime? CompletionDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }


        // ==================== Visibility ====================

        public bool IsFeatured { get; set; } = false;
        public bool IsPublic { get; set; } = true;


        // ==================== Navigation Properties ====================

        public virtual ArtisanProfile Artisan { get; set; }

        // One-to-Many relationship to additional images
        public virtual ICollection<ArtisanWorkImage> Images { get; set; }
    }
}