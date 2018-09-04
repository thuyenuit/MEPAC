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
    [Table("Partner")]
    public class Partner : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartID { get; set; }

        [MaxLength(200)]
        public string Display { get; set; }

        public string Logo { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(100)]
        public string Website { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Phone { get; set; }

    }
}
