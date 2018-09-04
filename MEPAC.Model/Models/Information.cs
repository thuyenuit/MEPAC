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
    public class Information : Auditable
    {
        [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InformationID { get; set; }

        [MaxLength(500)]
        public string Display { get; set; }

        [MaxLength(200)]
        public string ShortDisplay { get; set; }

        [MaxLength(500)]
        public string Address1 { get; set; }

        [MaxLength(500)]
        public string Address2 { get; set; }

        [MaxLength(500)]
        public string Address3 { get; set; }

        [MaxLength(500)]
        public string Address4 { get; set; }

        [MaxLength(500)]
        public string Address5 { get; set; }

        [MaxLength(100)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Website { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        public string Content { get; set; }

    }
}
