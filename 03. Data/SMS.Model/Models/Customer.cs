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
    [Table("Customers")]
    public class Customer : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(15)]
        [Column(TypeName = "varchar")]
        public string CellPhone { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        [MaxLength(15)]
        [Column(TypeName = "varchar")]
        public string Identity { get; set; }
    }
}
