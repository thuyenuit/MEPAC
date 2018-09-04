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
    [Table("Slide")]
    public class Slide : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SlideID { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Content { get; set; }

        public bool IsActive { get; set; }

        public string Image { get; set; }
    }
}
