using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MEPAC.Web.Models
{
    public class ProjectsViewModel
    {
        public int ProjectID { get; set; }
        /// <summary>
        /// Tên dự án
        /// </summary>
        public string Display { get; set; }
        /// <summary>
        /// Link hình ảnh
        /// </summary>
        public string LinkImage { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public string DisplayTime { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// MeteDescription
        /// </summary>
        public string MetaDescription { get; set; }
        /// <summary>
        /// MetaKeyword
        /// </summary>
        public string MetaKeyword { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// Người cập nhật
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// Ngày đăng
        /// </summary>
        public DateTime? PostDate { get; set; }
        /// <summary>
        /// Đăng bởi
        /// </summary>
        public string PostBy { get; set; }
        /// <summary>
        /// Đăng bài hoặc không
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public bool IsActive { get; set; }

    }
}