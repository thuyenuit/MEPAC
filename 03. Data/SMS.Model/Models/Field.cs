using SMS.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Models
{
    [Table("Fields")]
    public class Field
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long FieldID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int FieldGroupID { get; set; }

        [Required]
        public int FieldType { get; set; }

        [Required]
        public int Sequence { get; set; }

        public string Setting { get; set; } // Json

        [ForeignKey("FieldGroupID")]
        public virtual FieldGroup FieldGroup { get; set; }

        public virtual IEnumerable<FieldProduct> FieldProducts { get; set; }
    }
}
