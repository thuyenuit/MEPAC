using SMS.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Model.Models
{
    [Table("ProductCategories")]
    public class ProductCategory : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Required]
        public long CategoryID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string Alias { get; set; }

        public byte[] Icon { get; set; }

        public int Sequence { get; set; }

        public bool IsHomeFlag { get; set; }

        [ForeignKey("FK_ProductCategory_Category")]
        public virtual Category Category { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
