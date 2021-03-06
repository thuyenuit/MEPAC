﻿using SMS.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Models
{
    [Table("Stocks")]
    public class Stock: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(15)]
        [Column(TypeName = "varchar")]
        public string CellPhone { get; set; }

        [Required]
        public string SeniorManagerID { get; set; }

        public virtual IEnumerable<StockManager> StockManagers { get; set; }

        public virtual IEnumerable<StockProduct> StockProducts { get; set; }
    }
}
