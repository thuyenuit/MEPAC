using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Models
{
    [Table("InventoryDeliveryVoucherDetails")]
    public class InventoryDeliveryVoucherDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InventoryDeliveryVoucherDetailID { get; set; }

        public long InventoryDeliveryVoucherID { get; set; }

        public long ProductID { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("InventoryDeliveryVoucherID")]
        public virtual InventoryDeliveryVoucher InventoryDeliveryVoucher { get; set; }
    }
}
