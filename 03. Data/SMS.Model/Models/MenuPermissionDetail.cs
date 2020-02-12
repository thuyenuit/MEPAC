using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Models
{
    [Table("MenuPermissionDetails")]
    public class MenuPermissionDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string MenuPermissionID { get; set; }

        [Required]
        public bool IsView { get; set; }

        [Required]
        public bool IsAdd { get; set; }

        [Required]
        public bool IsEdit { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
    }
}
