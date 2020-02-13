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
    [Table("ColorProducts")]
    public class ColorProduct : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColorProductID { get; set; }

        [Required]
        public int ColorID { get; set; }

        [Required]
        public long ProductID { get; set; }

        [ForeignKey("ColorID")]
        public virtual Color Color { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
