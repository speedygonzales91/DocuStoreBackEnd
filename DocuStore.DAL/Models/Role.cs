using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocuStore.DAL.Models
{
    public class Role
    {
        public Role()
        {
            ProjectRoles = new List<ProjectRoles>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        // Navigation properties
        public virtual ICollection<ProjectRoles> ProjectRoles { get; set; }
    }
}
