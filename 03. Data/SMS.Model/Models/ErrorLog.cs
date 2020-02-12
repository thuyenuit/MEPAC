using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Model.Models
{
    [Table("ErrorLogs")]
    public class ErrorLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LogID { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime DateLog { get; set; }

        public string StackTrace { get; set; }

        [MaxLength(255)]
        public string Method { get; set; }

        [MaxLength(100)]
        public string Table { get; set; }
    }
}
