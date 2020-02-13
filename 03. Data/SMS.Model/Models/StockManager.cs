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
    [Table("StockManagers")]
    public class StockManager : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StockManagerID { get; set; }

        [Required]
        public int StockID { get; set; }

        [Required]
        public string UserID { get; set; }

        [ForeignKey("StockID")]
        public virtual Stock Stock { get; set; }

    }
}
