using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Models
{
    [Table("InventoryReceivingVouchers")]
    public class InventoryReceivingVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InventoryDeliveryVoucherID { get; set; }

        public int? SupplierID { get; set; }

        public int? TaxImport { get; set; }

        public int? TaxVAT { get; set; }

        [Required]
        public int TotalQuantity { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public decimal AmountPaid { get; set; }

        public decimal AmountOwed { get; set; }

        public int Status { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public virtual IEnumerable<InventoryDeliveryVoucherDetail> Details { get; set; }
    }
}
