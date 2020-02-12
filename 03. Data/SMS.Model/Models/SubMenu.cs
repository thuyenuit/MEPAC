using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Models
{
    [Table("SubMenus")]
    public class SubMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string Url { get; set; }

        [Required]
        public int MenuID { get; set; }

        public string Icon { get; set; }

        [Required]
        public int OrderNumber { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public bool IsDisplay { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("FK_SubMenu_Menu")]
        public virtual Menu Menu { get; set; }
    }
}
