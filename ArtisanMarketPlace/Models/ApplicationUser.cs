using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic; // Required for Navigation Properties
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtisanMarketPlace.Models
{
    // Inherit from IdentityUser<Guid> to use Guid (UUID) as the primary key type
    // Note: The base IdentityUser class already provides Id, UserName, Email, and PasswordHash.
    [Table("Users")] // Matches Django's db_table = 'users'
    public class ApplicationUser : IdentityUser<Guid>
    {
        // ==================== Overrides & Required Fields ====================

        // We use 'Email' from the base class but enforce uniqueness at the model level 
        // using IdentityUser's built-in functionality and DbContext configuration.

        // This is equivalent to Django's 'full_name' field
        [Required(ErrorMessage = "Full name is required.")]
        [MaxLength(255)]
        public string FullName { get; set; }

        // Custom validation for phone number format (similar to Django's RegexValidator)
        // Note: The Regex itself is often a strict match of the Python version's requirements.
        [RegularExpression(@"^\+?1?\d{9,15}$", ErrorMessage = "Phone number must be entered in the format: '+999999999'. Up to 15 digits allowed.")]
        [MaxLength(17)]
        public string PhoneNumber { get; set; } // Base class already has a PhoneNumber property, but we redefine it with validators

        public string Bio { get; set; }

        public string ProfilePicture { get; set; } // Store path/URL to the image

        public DateTime DateJoined { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // EF Core equivalent of auto_now=True
        public DateTime LastUpdated { get; set; }

        public bool IsVerified { get; set; } = false;


        // ==================== Address Fields ====================

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(20)]
        public string PostalCode { get; set; }


        // ==================== Navigation Properties (Relationships) ====================

        // 1. UserRole (was 'Role' in Django) - related_name='user_roles'
        // A User can have multiple Role assignments
        public virtual ICollection<UserRole> UserRoles { get; set; }

        // 2. ArtisanProfile - related_name='artisan_profile' (One-to-One)
        // ? means nullable (optional) for the one-to-one relationship
        public virtual ArtisanProfile ArtisanProfile { get; set; }

        // 3. Feeds - related_name='feeds'
        // A User can author multiple Feeds
        public virtual ICollection<Feed> Feeds { get; set; }

        // 4. Comments - related_name='comments'
        public virtual ICollection<Comment> Comments { get; set; }

        // 5. Reactions - related_name='reactions'
        public virtual ICollection<Reaction> Reactions { get; set; }

        // 6. Reports Made - related_name='reports_made'
        public virtual ICollection<Report> ReportsMade { get; set; }

        // 7. Reports Received - related_name='reports_received'
        // A User can be the target of multiple Reports
        public virtual ICollection<Report> ReportsReceived { get; set; }

        // 8. Reports Reviewed - related_name='reports_reviewed'
        public virtual ICollection<Report> ReportsReviewed { get; set; }
    }
}