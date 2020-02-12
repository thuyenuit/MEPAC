using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMS.API.ViewModels
{
    public class ProductViewModel
    {
        /// <summary>
        /// ID sản phẩm
        /// </summary>
        [Display(Name = "ID sản phẩm")]
        public int ProductID { get; set; }

        /// <summary>
        /// ID loại sản phẩm
        /// </summary>
        [Display(Name = "ID loại sản phẩm")]
        public int CategoryID { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; }

        /// <summary>
        /// Mô tả về sản phẩm
        /// </summary>
        [Display(Name = "Mô tả sản phẩm")]
        public string Description { get; set; }
    }
}