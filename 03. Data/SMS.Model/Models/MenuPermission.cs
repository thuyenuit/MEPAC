using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Models
{
    [Table("MenuPermissions")]
    public class MenuPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MenuPermissionID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public int MenuID { get; set; }

        [ForeignKey("MenuID")]
        public virtual Menu Menu { get; set; }

        public virtual IEnumerable<MenuPermissionDetail> MenuPermissionDetails { get; set; }
    }
}
