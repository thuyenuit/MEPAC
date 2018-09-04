using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Model.Models
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuID { get; set; }

        [MaxLength(100)]
        public string Display { get; set; }

        public bool IsActive { get; set; }

        public int Order { get; set; }

        public string Router { get; set; }

        public int IsSubMenu { get; set; }

        public virtual ICollection<SubMenu> SubMenus { get; set; }
    }
}
