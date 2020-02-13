using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service.ServiceObject
{
    public class ProductSO
    {
        public long ProductID { get; set; }   

        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public string Alias { get; set; }

        public byte[] Image { get; set; }

        public string MoreImage { get; set; }

        public string Content { get; set; }

        public int OrderNumber { get; set; }

        public bool HomeFlag { get; set; }

        public bool HotFlag { get; set; }

        public int Status { get; set; }

        public string Tags { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string UpdateBy { get; set; }

        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }

        public bool IsActive { get; set; }

        // Product detail
        public Guid ProductDetailID { get; set; }

        public int Quantity { get; set; }

        public decimal? PriceInput { get; set; }

        public decimal? PriceSale { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string AliasCategory { get; set; }

        public int OrderNumberCategroy { get; set; }
    }
}
