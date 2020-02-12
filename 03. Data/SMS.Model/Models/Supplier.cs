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
    [Table("Suppliers")]
    public class Supplier: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(15)]
        public string Phone { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
