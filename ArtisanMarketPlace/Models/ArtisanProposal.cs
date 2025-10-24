using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtisanMarketPlace.Models
{
    // Corresponds to the Django ArtisanProposal model
    [Table("ArtisanProposals")]
    public class ArtisanProposal
    {
        public enum ProposalStatus
        {
            PENDING, ACCEPTED, REJECTED, WITHDRAWN
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign Key to Feed (must be a JOB_REQUEST)
        [Required]
        public Guid FeedId { get; set; }

        // Foreign Key to ArtisanProfile
        [Required]
        public Guid ArtisanProfileId { get; set; }

        // ==================== Proposal Details ====================

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ProposedPrice { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int EstimatedDurationDays { get; set; }

        [Required]
        public string Message { get; set; }


        // ==================== Terms and Status ====================

        public string TermsConditions { get; set; }
        public string PaymentTerms { get; set; }

        public string QuoteDocumentPath { get; set; }

        public ProposalStatus Status { get; set; } = ProposalStatus.PENDING;


        // ==================== Timestamps ====================

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        public DateTime? AcceptedAt { get; set; }


        // ==================== Navigation Properties ====================

        // Relationships back to Feed and ArtisanProfile
        public virtual Feed Feed { get; set; }
        public virtual ArtisanProfile Artisan { get; set; }
    }
}