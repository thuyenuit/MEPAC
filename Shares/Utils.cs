using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMS.Share.Shares
{
    public static class Utils
    {
        public static string GetSeoTitle(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            //Đổi chữ hoa thành chữ thường
            string slug = input.Trim().ToLower();

            //Xóa ký tự đặc biệt
            var pattern = new Regex("[:!@#$%^&*()}{|\":?><\\[\\]\\;'/.,~+-~]");
            slug = pattern.Replace(slug, "_");

            //Đổi ký tự có dấu thành không dấu
            slug = slug.Replace("/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi", "a");
            slug = slug.Replace("/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi", "e");
            slug = slug.Replace("/i|í|ì|ỉ|ĩ|ị/gi", "i");
            slug = slug.Replace("/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi", "o");
            slug = slug.Replace("/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi", "u");
            slug = slug.Replace("/ý|ỳ|ỷ|ỹ|ỵ/gi", "y");
            slug = slug.Replace("/đ/gi", "d");

            // Chuyển nhiều khoảng trắng sang một khoảng trắng
            slug = slug.Replace("  ", " ");
            slug = slug.Replace("   ", " ");
            slug = slug.Replace("    ", " ");
            slug = slug.Replace("     ", " ");
            slug = slug.Replace("      ", " ");
            slug = slug.Replace("       ", " ");
            slug = slug.Replace("        ", " ");
            slug = slug.Replace("         ", " ");
            slug = slug.Replace("          ", " ");
            slug = slug.Replace("           ", " ");

            //Đổi khoảng trắng thành ký tự gạch ngang
            slug = slug.Replace("/ /gi", "-");
            return slug;
        }

        public static string GetString(IDictionary<string, object> dic, string key, string def = "")
        {
            if (!dic.ContainsKey(key))
            {
                return def;
            }

            return GetString(dic[key], def);
        }

        private static string GetString(object val, string def = "")
        {
            if (val == null)
            {
                return def;
            }

            if (val is string)
            {
                return (string)val;
            }
            else
            {
                throw new Exception("SMS.Share.Shares.Utils: GetString error, object is not a string value");
            }
        }
    }
}
