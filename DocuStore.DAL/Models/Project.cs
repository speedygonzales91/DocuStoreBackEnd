using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocuStore.DAL.Models
{
    public class Project
    {
        public Project()
        {
            Items = new List<Item>();
            ProjectRoles = new List<ProjectRoles>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<ProjectRoles> ProjectRoles { get; set; }
    }
}
