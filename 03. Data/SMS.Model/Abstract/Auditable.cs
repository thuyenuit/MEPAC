using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Abstract
{
    public interface IAuditable
    {
        DateTime CreateDate { get; set; }

        DateTime? ModifiedDate { get; set; }

        string CreateBy { get; set; }      

        string UpdateBy { get; set; }

        bool IsActive { get; set; }
    }

    public class Auditable : IAuditable
    {
        public DateTime CreateDate { get; set; }

        [MaxLength(255)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [MaxLength(255)]
        public string UpdateBy { get; set; }
  
        public bool IsActive { get; set; }
    }
}
