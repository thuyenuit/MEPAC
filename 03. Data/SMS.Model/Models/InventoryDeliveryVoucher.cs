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
    [Table("InventoryDeliveryVouchers")]
    public class InventoryDeliveryVoucher : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InventoryDeliveryVoucherID { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string OrderCode { get; set; }

        public long CustomerID { get; set; }

        [MaxLength(255)]
        public string CustomerName { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(15)]
        [Column(TypeName = "varchar")]
        public string Phone { get; set; }

        public int OrderType { get; set; }

        [Required]
        public int TotalQuantity { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public int Payment { get; set; }

        public int Status { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public virtual IEnumerable<InventoryDeliveryVoucherDetail> Details { get; set; }
    }
}
