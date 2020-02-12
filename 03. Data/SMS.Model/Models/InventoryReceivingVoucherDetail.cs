using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Model.Models
{
    [Table("InventoryReceivingVoucherDetails")]
    public class InventoryReceivingVoucherDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InventoryReceivingVoucherDetailID { get; set; }

        public long InventoryReceivingVoucherID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("InventoryReceivingVoucherID")]
        public virtual InventoryReceivingVoucher InventoryReceivingVoucher { get; set; }
    }
}
