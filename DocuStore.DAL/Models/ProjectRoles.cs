using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocuStore.DAL.Models
{
    public class ProjectRoles
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
        [Required]
        public int IdentityId { get; set; }
        [Required]
        public int IdentityType { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation Properties
        public Project Project { get; set; }
        public Role Role { get; set; }
    }
}
