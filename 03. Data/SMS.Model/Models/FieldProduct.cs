using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Models
{
    [Table("FieldProduct")]
    public class FieldProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long FieldProductID { get; set; }

        [Required]
        public long ProductID { get; set; }

        [Required]
        public long FieldID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Value { get; set; }

        //[ForeignKey("ProductID")]
        //public virtual Product Product { get; set; }

        [ForeignKey("FieldID")]
        public virtual Field Field { get; set; }
    }
}
