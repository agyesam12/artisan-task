using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition;
using static System.Collections.Specialized.BitVector32;

namespace ArtisanMarketPlace.Models
{
    // Corresponds to the Django Comment model
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign Key to the User who posted the comment
        [Required]
        public Guid UserId { get; set; }

        // Foreign Key to the Feed the comment is on
        [Required]
        public Guid FeedId { get; set; }

        // ==================== Content and Hierarchy ====================

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        // Self-referencing Foreign Key for replies (Parent Comment ID)
        public Guid? ParentCommentId { get; set; } // Nullable Guid

        // ==================== Engagement & Timestamps ====================

        public int LikesCount { get; set; } = 0;
        public int DislikesCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        // ==================== Moderation ====================
        public bool IsEdited { get; set; } = false;
        public bool IsFlagged { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        // ==================== Navigation Properties ====================

        public virtual ApplicationUser User { get; set; }
        public virtual Feed Feed { get; set; }

        // Navigation for the Parent/Reply structure:
        public virtual Comment ParentComment { get; set; } // The comment this replies to
        public virtual ICollection<Comment> Replies { get; set; } // Comments that reply to this one

        // Engagement and Moderation Relationships:
        public virtual ICollection<Reaction> Reactions { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}