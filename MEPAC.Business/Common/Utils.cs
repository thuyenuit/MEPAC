using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MEPAC.Business.Common
{
    public static class Utils
    {
        public static string AutoGenericCode()
        {
            string code = "SP" + DateTime.Now.ToString("yyyyMMddhhmmsss");
            return code;
        }

        #region // GetListInt
        public static List<int> GetListInt(IDictionary<string, object> dic, string key, List<int> def = null)
        {
            if (!dic.ContainsKey(key))
            {
                return def = new List<int>();
            }
            return GetListInt(dic[key], def);
        }
        public static List<int> GetListInt(object val, List<int> def = null)
        {
            if (val == null)
            {
                return def = new List<int>();
            }

            if (val is List<int>)
                return (List<int>)val;
            else
            {
                try
                {
                    def = val.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => Int32.Parse(x)).ToList();
                    return def;
                }
                catch
                {
                    throw new Exception("GetInt error, object is not a List Int value");
                }
            }

        }
        #endregion

        #region // GetInt
        public static int GetInt(IDictionary<string, object> dic, string key, int def = 0)
        {
            if (!dic.ContainsKey(key))
            {
                return def;
            }
            return GetInt(dic[key], def);
        }
        public static int GetInt(object val, int def = 0)
        {
            if (val == null)
            {
                return def;
            }

            if (val is int)
                return (int)val;
            else
            {
                try
                {
                    return Int32.Parse(val.ToString());
                }
                catch
                {
                    throw new Exception("GetInt error, object is not a Int value");
                }
            }

        }
        #endregion

        #region // GetString
        public static string GetString(IDictionary<string, object> dic, string key, string def = null)
        {
            if (!dic.ContainsKey(key))
            {
                return def;
            }
            return GetString(dic[key], def);
        }
        public static string GetString(object val, string def = null)
        {
            if (val == null)
            {
                return def;
            }

            if (val is string)
                return val.ToString();
            else
            {
                try
                {
                    return val.ToString();
                }
                catch
                {
                    throw new Exception("GetString error, object is not a String value");
                }
            }

        }
        #endregion

        #region //GetDecimal
        public static decimal GetDecimal(IDictionary<string, object> dic, string key, decimal def = 0)
        {
            if (!dic.ContainsKey(key))
            {
                return def;
            }
            return GetDecimal(dic[key], def);
        }
        public static decimal GetDecimal(object val, decimal def = 0)
        {
            if (val == null)
            {
                return def;
            }

            if (val is decimal)
                return (decimal)val;
            else
            {
                try
                {
                    return decimal.Parse(val.ToString());
                }
                catch
                {
                    throw new Exception("GetDecimal error, object is not a Decimal value");
                }
            }
        }
        #endregion

        public static string FormatDecimal(this decimal? val, CultureInfo custom = null, string formatString = "#.##")
        {
            if (custom == null)
            {
                custom = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
                custom.NumberFormat.NumberDecimalSeparator = ".";
            }
            return val.HasValue ? val.Value.ToString(formatString, custom) : "";
        }

        public static bool IsDateTime(string val)
        {
            DateTime temp;
            return DateTime.TryParse(val, out temp);
        }

        public static bool IsInt32(string val)
        {
            int temp;
            return Int32.TryParse(val, out temp);
        }

        public static bool IsDecimal(string val)
        {
            decimal temp;
            return Decimal.TryParse(val, out temp);
        }

        public static string GetFullPathFile(string fileName, string root)
        {
            if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
            {
                fileName = fileName.Trim('"');
            }
            if (fileName.Contains(@"/") || fileName.Contains(@"/"))
            {
                fileName = Path.GetFileName(fileName);
            }

            var fullPath = Path.Combine(root, fileName);

            return fullPath;
        }

        public static string GetSEOTitle(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return keyword;

            //Đổi chữ hoa thành chữ thường
            string slug = keyword.ToLower();

            //Đổi ký tự có dấu thành không dấu
            slug = slug.Replace("/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi", "a");
            slug = slug.Replace("/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi", "e");
            slug = slug.Replace("/i|í|ì|ỉ|ĩ|ị/gi", "i");
            slug = slug.Replace("/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi", "o");
            slug = slug.Replace("/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi", "u");
            slug = slug.Replace("/ý|ỳ|ỷ|ỹ|ỵ/gi", "y");
            slug = slug.Replace("/đ/gi", "d");
            //Xóa các ký tự đặt biệt
            slug = slug.Replace("`", "");
            slug = slug.Replace("~", "");
            slug = slug.Replace("!", "");
            slug = slug.Replace("@", "");
            slug = slug.Replace("#", "");
            slug = slug.Replace("$", "");
            slug = slug.Replace("%", "");
            slug = slug.Replace("^", "");
            slug = slug.Replace("&", "");
            slug = slug.Replace("*", "");
            slug = slug.Replace("(", "");
            slug = slug.Replace(")", "");
            slug = slug.Replace("+", "");
            slug = slug.Replace("=", "");
            slug = slug.Replace(",", "");
            slug = slug.Replace(".", "");
            slug = slug.Replace("/", "");
            slug = slug.Replace("\\", "");
            slug = slug.Replace(">", "");
            slug = slug.Replace("<", "");
            slug = slug.Replace("'", "");
            slug = slug.Replace("\"", "");
            slug = slug.Replace(":", "");
            slug = slug.Replace(";", "");
            slug = slug.Replace("_", "");
            //Đổi khoảng trắng thành ký tự gạch ngang
            slug = slug.Replace("/ /gi", "-");
            //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
            //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
            //slug = slug.Replace(/\-\-\-\-\-/ gi, '-');
            //slug = slug.Replace(/\-\-\-\-/ gi, '-');
            //slug = slug.Replace(/\-\-\-/ gi, '-');
            //slug = slug.Replace(/\-\-/ gi, '-');
            //Xóa các ký tự gạch ngang ở đầu và cuối
            //slug = '@' + slug + '@';
            //slug = slug.replace("/\@\-|\-\@|\@/gi", "");

            return slug;
        }

        public static string ToStrimVN(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return keyword;

            string slug = keyword.ToLower();
            slug = slug.Replace("á", "a");
            slug = slug.Replace("à", "a");
            slug = slug.Replace("ả", "a");
            slug = slug.Replace("ạ", "a");
            slug = slug.Replace("ã", "a");
            slug = slug.Replace("ă", "a");
            slug = slug.Replace("ắ", "a");
            slug = slug.Replace("ằ", "a");
            slug = slug.Replace("ẳ", "a");
            slug = slug.Replace("ẵ", "a");
            slug = slug.Replace("ặ", "a");
            slug = slug.Replace("â", "a");
            slug = slug.Replace("ấ", "a");
            slug = slug.Replace("ầ", "a");
            slug = slug.Replace("ẩ", "a");
            slug = slug.Replace("ẫ", "a");
            slug = slug.Replace("ậ", "a");

            slug = slug.Replace("é", "e");
            slug = slug.Replace("è", "e");
            slug = slug.Replace("ẻ", "e");
            slug = slug.Replace("ẽ", "e");
            slug = slug.Replace("ẹ", "e");
            slug = slug.Replace("ê", "e");
            slug = slug.Replace("ế", "e");
            slug = slug.Replace("ề", "e");
            slug = slug.Replace("ể", "e");
            slug = slug.Replace("ễ", "e");
            slug = slug.Replace("ệ", "e");

            slug = slug.Replace("í", "i");
            slug = slug.Replace("ì", "i");
            slug = slug.Replace("ỉ", "i");
            slug = slug.Replace("ĩ", "i");
            slug = slug.Replace("ị", "i");

            slug = slug.Replace("ó", "o");
            slug = slug.Replace("ò", "o");
            slug = slug.Replace("ỏ", "o");
            slug = slug.Replace("õ", "o");
            slug = slug.Replace("ọ", "o");
            slug = slug.Replace("ô", "o");
            slug = slug.Replace("ố", "o");
            slug = slug.Replace("ồ", "o");
            slug = slug.Replace("ổ", "o");
            slug = slug.Replace("ỗ", "o");
            slug = slug.Replace("ộ", "o");
            slug = slug.Replace("ơ", "o");
            slug = slug.Replace("ớ", "o");
            slug = slug.Replace("ờ", "o");
            slug = slug.Replace("ở", "o");
            slug = slug.Replace("ỡ", "o");
            slug = slug.Replace("ợ", "o");

            slug = slug.Replace("ú", "u");
            slug = slug.Replace("ù", "u");
            slug = slug.Replace("ủ", "u");
            slug = slug.Replace("ũ", "u");
            slug = slug.Replace("ụ", "u");
            slug = slug.Replace("ư", "u");
            slug = slug.Replace("ứ", "u");
            slug = slug.Replace("ừ", "u");
            slug = slug.Replace("ử", "u");
            slug = slug.Replace("ữ", "u");
            slug = slug.Replace("ự", "u");

            slug = slug.Replace("ý", "y");
            slug = slug.Replace("ỳ", "y");
            slug = slug.Replace("ỷ", "y");
            slug = slug.Replace("ỹ", "y");
            slug = slug.Replace("ỵ", "y");
            slug = slug.Replace("đ", "d");

            slug = slug.Replace("`", "");
            slug = slug.Replace("~", "");
            slug = slug.Replace("!", "");
            slug = slug.Replace("@", "");
            slug = slug.Replace("#", "");
            slug = slug.Replace("$", "");
            slug = slug.Replace("%", "");
            slug = slug.Replace("^", "");
            slug = slug.Replace("&", "");
            slug = slug.Replace("*", "");
            slug = slug.Replace("(", "");
            slug = slug.Replace(")", "");
            slug = slug.Replace("+", "");
            slug = slug.Replace("=", "");
            slug = slug.Replace(",", "");
            slug = slug.Replace(".", "");
            slug = slug.Replace("/", "");
            slug = slug.Replace("\\", "");
            slug = slug.Replace(">", "");
            slug = slug.Replace("<", "");
            slug = slug.Replace("'", "");
            slug = slug.Replace("\"", "");
            slug = slug.Replace(":", "");
            slug = slug.Replace(";", "");
            slug = slug.Replace("_", "");
            return slug;
        }
    }
}
