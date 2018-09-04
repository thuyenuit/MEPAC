using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Model.Models
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactID { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Email {get;set;}

        [MaxLength(1000)]
        public string Content { get; set; }

        public DateTime SendDate { get; set; }

        public bool IsReading { get; set; }

        public string ReaderBy { get; set; }

        public DateTime ReadDate { get; set; }

    }
}
