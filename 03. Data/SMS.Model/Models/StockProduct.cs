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
    [Table("StockProducts")]
    public class StockProduct : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StockProductID { get; set; }

        [Required]
        public int StockID { get; set; }

        [Required]
        public long ProductID { get; set; }

        public int ColorID { get; set; }

        public string Size { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("StockID")]
        public virtual Stock Stock { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
