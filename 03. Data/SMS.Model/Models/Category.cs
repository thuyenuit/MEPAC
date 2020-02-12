using SMS.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Model.Models
{
    [Table("Categories")]
    public class Category: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string Alias { get; set; }

        [Required]
        public int Sequence { get; set; }

        public bool IsHomeFlag { get; set; }

        [MaxLength(500)]
        public string MetaKeyword { get; set; }

        [MaxLength(500)]
        public string MetaDescription { get; set; }

        public virtual IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
