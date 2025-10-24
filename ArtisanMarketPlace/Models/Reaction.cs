using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtisanMarketPlace.Models
{
    // Corresponds to the Django Reaction model
    [Table("Reactions")]
    public class Reaction
    {
        public enum ReactionType
        {
            LIKE, DISLIKE
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        public ReactionType Type { get; set; }

        // Foreign Keys for the target content (Polymorphic relationship)
        // Only one of these should be non-null (enforced in DbContext)
        public Guid? FeedId { get; set; } // Nullable
        public Guid? CommentId { get; set; } // Nullable

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ==================== Navigation Properties ====================

        public virtual ApplicationUser User { get; set; }
        public virtual Feed Feed { get; set; }
        public virtual Comment Comment { get; set; }
    }
}