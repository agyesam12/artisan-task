using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition;
using static System.Collections.Specialized.BitVector32;

namespace ArtisanMarketPlace.Models
{
    // Corresponds to the Django Feed model
    [Table("Feeds")]
    public class Feed
    {
        // Enums for choices
        public enum FeedType
        {
            JOB_REQUEST, SERVICE_OFFERING, PROMOTION, SHOWCASE, TIP, ANNOUNCEMENT, GENERAL
        }

        public enum Status
        {
            OPEN, IN_REVIEW, NEGOTIATING, CLOSED, COMPLETED, CANCELLED
        }

        public enum Priority
        {
            LOW, MEDIUM, HIGH, URGENT
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign Key to the User who authored the post
        [Required]
        public Guid AuthorId { get; set; }

        // Foreign Key to ArtisanProfile (Nullable: only set if post is by an artisan)
        public Guid? ArtisanProfileId { get; set; }

        // ==================== Basic Post Information ====================

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Slug { get; set; }

        [Required]
        public string Description { get; set; }

        public FeedType Type { get; set; }

        [Required]
        [MaxLength(100)]
        public string Category { get; set; }


        // ==================== Media ====================

        public string FeaturedImagePath { get; set; } // Nullable by default (string)
        public string VideoUrl { get; set; } // Nullable by default (string)


        // ==================== Job Request Specific Fields ====================

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? BudgetRangeMin { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? BudgetRangeMax { get; set; }

        public string InvoiceImagePath { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? InvoiceAmount { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public string AdditionalDocumentsPath { get; set; }


        // ==================== Service/Promotion Specific Fields ====================

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Price { get; set; }

        [Range(0, 100)]
        public int? DiscountPercentage { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidUntil { get; set; }


        // ==================== Location and Timeline ====================

        [MaxLength(255)]
        public string Location { get; set; }

        public DateTime? PreferredStartDate { get; set; }

        public DateTime? Deadline { get; set; }


        // ==================== Status & Priority ====================

        public Status CurrentStatus { get; set; } = Status.OPEN;

        public Priority PriorityLevel { get; set; } = Priority.MEDIUM;


        // ==================== Engagement & Timestamps ====================

        public int ViewsCount { get; set; } = 0;
        public int CommentsCount { get; set; } = 0;
        public int LikesCount { get; set; } = 0;
        public int DislikesCount { get; set; } = 0;
        public int SharesCount { get; set; } = 0;
        public int ReportsCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }


        // ==================== Visibility and Moderation ====================

        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        public bool IsPromoted { get; set; } = false;
        public bool IsFlagged { get; set; } = false;


        // ==================== Navigation Properties ====================

        // Relationships back to User and ArtisanProfile
        public virtual ApplicationUser Author { get; set; }
        public virtual ArtisanProfile ArtisanProfile { get; set; }

        // Relationships to related content
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<ArtisanProposal> Proposals { get; set; }
    }
}