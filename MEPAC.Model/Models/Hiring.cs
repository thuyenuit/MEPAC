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
    [Table("Hiring")]
    public class Hiring : Auditable
    {
        [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HiringID { get; set; }

        [MaxLength(200)]
        public string Position { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EnddDate { get; set; }

        public string Content { get; set; }

        public DateTime? PostDate { get; set; }

        public string PostBy { get; set; }
    }
}
