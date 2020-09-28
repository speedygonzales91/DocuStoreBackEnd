using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocuStore.DAL.Models
{
    public class Item
    {
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public long? ParentItemId { get; set; }
        public long? Size { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int Provider { get; set; }

        // Navigation properties
        public virtual Type Type { get; set; }
        public virtual Project  Project { get; set; }
    }
}
