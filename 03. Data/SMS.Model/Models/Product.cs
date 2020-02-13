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
        public long ProductID { get; set; }

        [Required]
        public int ProductCategoryID { get; set; }

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

        [Required]
        public decimal PriceInput { get; set; }

        [Required]
        public decimal PriceOutput { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        [Required]
        public int Sequence { get; set; }

        public bool IsHomeFlag { get; set; }

        public bool IsHotFlag { get; set; }

        public int Status { get; set; }

        [MaxLength(500)]
        public string Tags { get; set; }

        [MaxLength(500)]
        public string MetaKeyword { get; set; }

        [MaxLength(500)]
        public string MetaDescription { get; set; }

        [ForeignKey("ProductCategoryID")]
        public virtual ProductCategory ProductCategory { get; set; }

        //public virtual IEnumerable<FieldProduct> FieldProducts { get; set; }

        public virtual IEnumerable<StockProduct> StockProducts { get; set; }

        public virtual IEnumerable<ColorProduct> ColorProducts { get; set; }
    }
}
