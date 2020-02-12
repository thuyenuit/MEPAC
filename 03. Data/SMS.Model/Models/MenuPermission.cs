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
        [Required]
        [Key, Column(Order = 1)]
        public string UserID { get; set; }

        [Required]
        [Key, Column(Order = 2)]
        public int MenuID { get; set; }

        public int SubMenuID { get; set; }
    }
}
