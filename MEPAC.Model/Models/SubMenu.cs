using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Model.Models
{
    [Table("SubMenu")]
    public class SubMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubMenuID { get; set; }

        public int MenuID { get; set; }

        [MaxLength(100)]
        public string Display { get; set; }

        public int Order { get; set; }

        public string Router { get; set; }

        public bool IsActive { get; set; }

        public string Image { get; set; }

        [ForeignKey("MenuID")]
        public virtual Menu Menu { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
