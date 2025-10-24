using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ArtisanMarketPlace.Models
{
    // Corresponds to the Django Report model
    [Table("Reports")]
    public class Report
    {
        public enum ReportReason
        {
            SPAM, INAPPROPRIATE, SCAM, MISLEADING, HARASSMENT, COPYRIGHT, OTHER
        }

        public enum ReportStatus
        {
            PENDING, UNDER_REVIEW, RESOLVED, DISMISSED
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ReporterId { get; set; }

        // Target Content Foreign Keys (at least one must be non-null, enforced in DbContext)
        public Guid? FeedId { get; set; }
        public Guid? CommentId { get; set; }
        public Guid? ReportedUserId { get; set; } // The user being reported

        // ==================== Report Details ====================

        public ReportReason Reason { get; set; }

        [Required]
        public string Description { get; set; }

        public ReportStatus Status { get; set; } = ReportStatus.PENDING;

        // ==================== Resolution ====================

        public Guid? ReviewedById { get; set; } // User ID of the Moderator

        public string ResolutionNotes { get; set; }

        // ==================== Timestamps ====================

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ReviewedAt { get; set; }

        // ==================== Navigation Properties ====================

        public virtual ApplicationUser Reporter { get; set; }
        public virtual Feed Feed { get; set; }
        public virtual Comment Comment { get; set; }

        // Explicit navigation to the Reported User and the Reviewer:
        [ForeignKey(nameof(ReportedUserId))]
        public virtual ApplicationUser ReportedUser { get; set; }

        [ForeignKey(nameof(ReviewedById))]
        public virtual ApplicationUser ReviewedBy { get; set; }
    }
}