using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Model.Models
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string Url { get; set; }

        [Required]
        public int Sequence { get; set; }

        public int ParentID { get; set; }

        public bool IsParent { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public bool IsDisplay { get; set; }
    
        public bool IsActive { get; set; }

        public virtual IEnumerable<MenuPermission> MenuPermissions { get; set; }
    }
}
