using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Model.Models
{
    [Table("MetaImage")]
    public class MetaImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MetaImageID { get; set; }
        
        public int ParentID { get; set; }

        public string Link { get; set; }

        public int TypeID { get; set; }

        public bool IsActive { get; set; }

    }
}
