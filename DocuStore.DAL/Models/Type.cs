using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocuStore.DAL.Models
{
    public class Type
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        // Navigation properties
        public virtual ICollection<Item> Items { get; set; }
    }
}
