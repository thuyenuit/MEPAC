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
    [Table("News")]
    public class News : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NewsID { get; set; }

        [MaxLength(500)]
        public string DisplayName { get; set; }

        public int SubMenu { get; set; }

        public string Content { get; set; }

        [MaxLength(500)]
        public string ShortContent { get; set; }

        public bool IsActive { get; set; }

        public string Image { get; set; }

        public int SubMenuID { get; set; }

        public DateTime? PostDate { get; set; }

        public string PostBy { get; set; }

        [ForeignKey("SubMenuID")]
        public SubMenu SubMenus { get; set; }
    }
}
