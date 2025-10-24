using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ArtisanMarketPlace.Models
{
    // Corresponds to the Django ArtisanProfile model
    [Table("ArtisanProfiles")]
    public class ArtisanProfile
    {
        // Enums or constants are typically used for choices in C#
        public enum ExperienceLevel
        {
            BEGINNER, INTERMEDIATE, EXPERIENCED, EXPERT
        }

        public enum AvailabilityStatus
        {
            AVAILABLE, BUSY, UNAVAILABLE
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign Key (One-to-One relationship with User)
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string BusinessName { get; set; }

        [MaxLength(255)]
        public string Slug { get; set; } // Used for SEO-friendly URLs

        [MaxLength(100)]
        public string Specialization { get; set; }


        // ==================== Professional Details ====================

        [Required]
        [Range(0, 100)] // Simple range constraint for years of experience
        public int YearsOfExperience { get; set; }

        public ExperienceLevel CurrentExperienceLevel { get; set; } // Stored as integer enum value

        [MaxLength(100)]
        public string LicenseNumber { get; set; }

        public string CertificationPath { get; set; } // Store path/URL to the file


        // ==================== Business Information ====================

        [MaxLength(100)]
        public string BusinessRegistration { get; set; }

        [MaxLength(50)]
        public string TaxId { get; set; }

        public string InsuranceDetails { get; set; }


        // ==================== Ratings and Reputation ====================

        [Column(TypeName = "decimal(3, 2)")] // Explicitly define precision for decimals (3 digits total, 2 after decimal)
        [Range(0.0, 5.0)]
        public decimal AverageRating { get; set; } = 0.0m;

        public int TotalReviews { get; set; } = 0;

        public int CompletedProjects { get; set; } = 0;


        // ==================== Availability ====================

        public AvailabilityStatus CurrentAvailabilityStatus { get; set; } = AvailabilityStatus.AVAILABLE;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? HourlyRate { get; set; } // Nullable decimal

        // Kilometers (Nullable)
        public int? ServiceRadiusKm { get; set; }


        // ==================== Description & Services ====================

        [MaxLength(2000)]
        public string About { get; set; }

        // Store services as a comma-separated string, or consider a dedicated ServiceTag entity later
        public string ServicesOffered { get; set; }


        // ==================== Verification & Timestamps ====================

        public bool IsVerified { get; set; } = false;

        public DateTime? VerifiedDate { get; set; } // Nullable DateTime

        public string VerificationDocumentsPath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }


        // ==================== Navigation Properties ====================

        // One-to-One relationship back to the User
        public virtual ApplicationUser User { get; set; }

        // One-to-Many relationships to Work, Feeds, and Proposals
        public virtual ICollection<ArtisanWork> PortfolioWorks { get; set; }
        public virtual ICollection<Feed> Feeds { get; set; }
        public virtual ICollection<ArtisanProposal> Proposals { get; set; }
    }
}