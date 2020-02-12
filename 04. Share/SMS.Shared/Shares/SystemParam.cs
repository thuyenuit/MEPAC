﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Shared.Shares
{
    public static class SystemParam
    {
        public const int PAGE_SIZE = 10;
        public const int PAGE = 1;

        public const string ERROR = "error";
        public const string SUCCESS = "success";
        public const string MESSAGE_ADD_ERROR = "Thêm mới thất bại";
        public const string MESSAGE_ADD_SUCCESS = "Thêm mới thành công";
        public const string MESSAGE_EDIT_ERROR = "Cập nhật thất bại";
        public const string MESSAGE_EDIT_SUCCESS = "Cập nhật thành công";

        public const string BASE_API = "http://localhost:56935/";
        /// <summary>
        /// 
        /// </summary>
        public const string LOGIN_API = BASE_API + "api/account/login";

        public const string BASE_ACCOUNT_URL = BASE_API + "api/account/";    
        // Url Thể loại
        public const string BASE_CATEGORY_URL = BASE_API + "api/category/";
        // Url Sản phẩm
        public const string BASE_PRODUCT_URL = BASE_API + "api/product/";

        public const string BASE_MENU_PERMISSION_URL = BASE_API + "api/menupermission/";
    }
}
