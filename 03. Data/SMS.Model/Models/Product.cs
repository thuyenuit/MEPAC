using SMS.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Models
{
    [Table("Products")]
    public class Product: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Required]
        public long ProductCategoryID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string Alias { get; set; }

        public byte[] Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        [Required]
        public int Sequence { get; set; }

        public bool IsHomeFlag { get; set; }

        public bool IsHotFlag { get; set; }

        public int Status { get; set; }

        [MaxLength(500)]
        public string Tags { get; set; }

        [ForeignKey("FK_Product_ProductCategory")]
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
