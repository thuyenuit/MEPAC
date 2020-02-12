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
    [Table("ProductDetail")]
    public class ProductDetail: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductDetailID { get; set; }
     
        public long ProductID { get; set; }

        public int Quantity { get; set; }

        public decimal? PriceInput { get; set; }

        public decimal? PriceSale { get; set; }
    }
}
