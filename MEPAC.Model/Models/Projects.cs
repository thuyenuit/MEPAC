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
    [Table("Projects")]
    public class Projects : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }

        [MaxLength(200)]
        public string Display { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsShow { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime? PostDate { get; set; }

        public string PostBy { get; set; }

        public bool IsRepresentative { get; set; }
    }
}
