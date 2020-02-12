using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMS.Shared.Shares
{
    public static class Utils
    {
        public static string GetSeoTitle(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            //Đổi chữ hoa thành chữ thường
            string slug = input.Trim().ToLower();

            //Đổi ký tự có dấu thành không dấu
            slug = slug.StripVNSign();

            //Xóa ký tự đặc biệt
            //var pattern = new Regex("[:!@#$%^&*()}{|\":?><\\[\\]\\;'/.,~+-~]");
            //slug = pattern.Replace(slug, "");
      
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
            slug = slug.Replace(" ", "-");
            return slug;
        }

        public static string StripVNSignAndSpace(this string text)
        {
            var pattern = new Regex("[:!@#$%^&*()}{|\":?><\\[\\]\\;'/.,~]");
            text = pattern.Replace(text, "");

            text = StripVNSign(text);
            text = text.Replace(" ", "");
            text = text.Replace("@", "");
            text = text.Replace("'", "");
            text = text.Replace("/*", "");
            text = text.Replace("\"", "");
            text = text.Replace("//", "");
            return text;
        }

        public static string StripVNSign(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            List<string> ListAllVNChars = new List<string>{
                "à", "á", "ạ", "ả", "ã", "â", "ầ", "ấ", "ậ", "ẩ",
            "ẫ", "ă", "ằ", "ắ", "ặ", "ẳ", "ẵ", "è", "é", "ẹ", "ẻ", "ẽ", "ê",
            "ề", "ế", "ệ", "ể", "ễ", "ì", "í", "ị", "ỉ", "ĩ", "ò", "ó", "ọ",
            "ỏ", "õ", "ô", "ồ", "ố", "ộ", "ổ", "ỗ", "ơ", "ờ", "ớ", "ợ", "ở",
            "ỡ", "ù", "ú", "ụ", "ủ", "ũ", "ư", "ừ", "ứ", "ự", "ử", "ữ", "ỳ",
            "ý", "ỵ", "ỷ", "ỹ", "đ", "À", "Á", "Ạ", "Ả", "Ã", "Â", "Ầ", "Ấ",
            "Ậ", "Ẩ", "Ẫ", "Ă", "Ằ", "Ắ", "Ặ", "Ẳ", "Ẵ", "È", "É", "Ẹ", "Ẻ",
            "Ẽ", "Ê", "Ề", "Ế", "Ệ", "Ể", "Ễ", "Ì", "Í", "Ị", "Ỉ", "Ĩ", "Ò",
            "Ó", "Ọ", "Ỏ", "Õ", "Ô", "Ồ", "Ố", "Ộ", "Ổ", "Ỗ", "Ơ", "Ờ", "Ớ",
            "Ợ", "Ở", "Ỡ", "Ù", "Ú", "Ụ", "Ủ", "Ũ", "Ư", "Ừ", "Ứ", "Ự", "Ử",
            "Ữ", "Ỳ", "Ý", "Ỵ", "Ỷ", "Ỹ", "Đ", "ê", "ù", "à",

            "á", "ạ", "ả", "ã", "â", "ầ", "ấ", "ậ", "ẩ", "ẫ", "ă",
            "ằ", "ắ", "ặ", "ẳ", "ẵ", "è", "é", "ẹ", "ẻ", "ẽ", "ê",
            "ề", "ế", "ệ", "ể", "ễ", "ì", "í", "ị", "ỉ", "ĩ", "ò",
            "ó", "ọ", "ỏ", "õ", "ô", "ồ", "ố", "ộ", "ổ", "ỗ", "ơ",
            "ờ", "ớ", "ợ", "ở", "ỡ", "ù", "ú", "ụ", "ủ", "ũ", "ư",
            "ừ", "ứ", "ự", "ử", "ữ", "ỳ", "ý", "ỵ", "ỷ", "ỹ", "đ",
            "À", "Á", "Ạ", "Ả", "Ã", "Â", "Ầ", "Ấ", "Ậ", "Ẩ", "Ẫ",
            "Ă", "Ằ", "Ắ", "Ặ", "Ẳ", "Ẵ", "È", "É", "Ẹ", "Ẻ", "Ẽ",
            "Ê", "Ề", "Ế", "Ệ", "Ể", "Ễ", "Ì", "Í", "Ị", "Ỉ", "Ĩ",
            "Ò", "Ó", "Ọ", "Ỏ", "Õ", "Ô", "Ồ", "Ố", "Ộ", "Ổ", "Ỗ",
            "Ơ", "Ờ", "Ớ", "Ợ", "Ở", "Ỡ", "Ù", "Ú", "Ụ", "Ủ", "Ũ",
            "Ư", "Ừ", "Ứ", "Ự", "Ử", "Ữ", "Ỳ", "Ý", "Ỵ", "Ỷ", "Ỹ",
            "Đ"
            };

            List<string> ListAllUnsignChars = new List<string>{
                "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "a", "a", "a", "a", "a", "a", "a", "e", "e", "e", "e", "e", "e",
            "e", "e", "e", "e", "e", "i", "i", "i", "i", "i", "o", "o", "o",
            "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o",
            "o", "u", "u", "u", "u", "u", "u", "u", "u", "u", "u", "u", "y",
            "y", "y", "y", "y", "d", "A", "A", "A", "A", "A", "A", "A", "A",
            "A", "A", "A", "A", "A", "A", "A", "A", "A", "E", "E", "E", "E",
            "E", "E", "E", "E", "E", "E", "E", "I", "I", "I", "I", "I", "O",
            "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O",
            "O", "O", "O", "U", "U", "U", "U", "U", "U", "U", "U", "U", "U",
            "U", "Y", "Y", "Y", "Y", "Y", "D", "e", "u", "a",

            "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "a", "a", "a", "e", "e", "e", "e", "e", "e", "e", "e", "e", "e",
            "e", "i", "i", "i", "i", "i", "o", "o", "o", "o", "o", "o", "o",
            "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "u", "u", "u",
            "u", "u", "u", "u", "u", "u", "u", "u", "y", "y", "y", "y", "y",
            "d", "A", "A", "A", "A", "A", "A", "A", "A", "A", "A", "A", "A",
            "A", "A", "A", "A", "A", "E", "E", "E", "E", "E", "E", "E", "E",
            "E", "E", "E", "I", "I", "I", "I", "I", "O", "O", "O", "O", "O",
            "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "U",
            "U", "U", "U", "U", "U", "U", "U", "U", "U", "U", "Y", "Y", "Y",
            "Y", "Y", "D"
            };

            for (int i = 0; i < ListAllVNChars.Count; i++)
            {
                text = text.Replace(ListAllVNChars[i], ListAllUnsignChars[i]);
            }
            return text;
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
