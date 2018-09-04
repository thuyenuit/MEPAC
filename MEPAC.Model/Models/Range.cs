using MEPAC.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Model.Models
{
    [Table("Range")]
    public class Range : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RangeID { get; set; }

        public int SubMenuID { get; set; }

        public string Cotntent { get; set; }

        [ForeignKey("SubMenuID")]
        public virtual SubMenu SubMenu { get; set; }



    }
}
