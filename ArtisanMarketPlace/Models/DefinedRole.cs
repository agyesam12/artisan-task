using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtisanMarketPlace.Models
{
    // Corresponds to the Django DefinedRole model
    [Table("DefinedRoles")]
    public class DefinedRole
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)] // EF Core/Database index for uniqueness and lookup efficiency
        public string Name { get; set; } // e.g., "ARTISAN", "ADMIN"

        [Required]
        [MaxLength(100)]
        public string DisplayName { get; set; }

        public string Description { get; set; }

        // Optional: Flag for system roles that shouldn't be deleted/modified easily
        public bool IsSystemRole { get; set; } = false;


        // ==================== Navigation Properties (Permissions & Assignments) ====================

        // 1. Permissions (M:M relationship)
        // Note: In EF Core, a traditional M:M with Identity permissions is complex.
        // We often use a join entity (like IdentityRoleClaim) or a dedicated mapping entity.
        // For simplicity at the model level, we'll assume a mapping table will exist in the DbContext.
        // For now, we rely on the Join Entity/Claim table inherent in Identity, or a custom one if needed.

        // 2. User Assignments - related_name='user_assignments'
        // A DefinedRole can be assigned to multiple users
        public virtual ICollection<UserRole> UserAssignments { get; set; }
    }
}