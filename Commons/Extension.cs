using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderForMeProject.Commons
{
    /// <summary>
    /// Lớp Extension chứa các hàm tái sử dụng và tiện ích
    /// </summary>
    public static class Extension
    {
        public static void SendMailError(string err)
        {

        }
        public static bool IsValidRoute(this HttpContext httpContext, out ulong guildId)
        {
            if (!httpContext.Request.RouteValues.TryGetValue("guildId", out var obj))
            {
                httpContext.Response.StatusCode = 403;
                guildId = default;
                return false;
            }

            if (ulong.TryParse($"{obj}", out guildId))
            {
                return true;
            }

            httpContext.Response.StatusCode = 403;
            return false;
        }

        public static bool TryMatchAny<T>(this T value, params T[] against) where T : struct
        {
            return against.Contains(value);
        }

        /// <summary>
        /// Lấy thông tin theo element trong file XML
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetInfoByXML(string els, string p = null)
        {
            if (!File.Exists("Language.xml"))
            {
                return new Dictionary<string, string>
                {
                    { "vn", els },
                    { "en", els },
                    { "ko", els }
                };
            }
            XElement root = XElement.Load("Language.xml");
            IEnumerable<XElement> data = (from el in root.Elements("Language")
                                          where (string)el.Attribute("Name") == els
                                          select el);
            Dictionary<string, string> lang = new Dictionary<string, string>()
                {
                    { "vn", string.Format(data.FirstOrDefault(x=>x.Attribute("Type")!=null&&x.Attribute("Type").Value=="vn")?.Attribute("Value")?.Value??els,p) },
                    { "en", string.Format(data.FirstOrDefault(x=>x.Attribute("Type")!=null&&x.Attribute("Type").Value=="en")?.Attribute("Value")?.Value??els,p) },
                    { "ko", string.Format(data.FirstOrDefault(x=>x.Attribute("Type")!=null&&x.Attribute("Type").Value=="ko")?.Attribute("Value")?.Value??els,p) }
                };
            return lang;
        }

        public static string GetIPClient()
        {
            throw new NotImplementedException();
        }

        public static string ToJson<T>(this T obj)
        {
            var serialize = JsonConvert.SerializeObject(obj);
            return serialize;
        }
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public static int GetUserId(this IPrincipal User)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                return Convert.ToInt32(userIdClaim?.Value ?? "-1");
            }
            return -1;
        }
        public static string GetUserRole(this IPrincipal User)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.Role);

                return userIdClaim?.Value;
            }
            return null;
        }
        public static string[] GetDepartment(this IPrincipal User)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == "Department");

                return userIdClaim?.Value?.Split(',');
            }
            return new string[] { };
        }
        public static string GetFullName(this IPrincipal User)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == "FullName");

                return userIdClaim?.Value;
            }
            return "";
        }
        public static string JoinAsString<T>(this IEnumerable<T> input, string seperator)
        {
            var ar = input.Select(i => i.ToString());
            return string.Join(seperator, ar);
        }
        /// <summary>
        /// Hàm lấy Claim đã Add khi Login dựa vào key
        /// </summary>
        /// <param name="User"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetClaimByType(this IPrincipal User, string type)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == type);

                return userIdClaim?.Value;
            }
            return null;
        }
        /// <summary>
        /// Kiểm tra kết nối mạng
        /// </summary>
        /// <returns></returns>
        public static bool CheckInternet()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
        public static string ToStringID(this List<int> lst)
        {
            string str = "";
            foreach (var item in lst)
            {
                str += item + ",";
            }
            return str;
        }
        /// <summary>
        /// Lấy địa chỉ IP V4 Private
        /// </summary>
        /// <returns></returns>
        public static string GetIPPrivate()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "No network adapters with an IPv4 address in the system!";
        }
        /// <summary>
        /// Hàm này mã hóa chuỗi thành MD5 (Hash 1 chiều)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToMD5(this string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }
        /// <summary>
        /// Hàm kiểm tra số điện thoại có hợp lệ không
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(this string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                string pattern = @"^-*[0-9,\.?\-?\(?\)?\ ]+$";
                return Regex.IsMatch(phone, pattern);
            }
            else
                return false;
        }
        public static DateTime ToTimeZone7(this DateTime dateTime)
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(dateTime, timeZone);
            return dt;
        }
        /// <summary>
        /// Hàm kiểm tra Email có hợp lệ không
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Chuyển ngày mặc định sang dạng dd/mm/yyyy Ví dụ: 25/12/2019
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime ToDateTime<T>(this T input)
        {
            return Convert.ToDateTime(input);
        }
        /// <summary>
        /// Hàm này chuyển từ một Object bất kỳ --->Int
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt<T>(this T input)
        {
            return Convert.ToInt32(input);
        }
        /// <summary>
        /// Hàm này chuyển từ một Object bất kỳ về Boolean
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ToBoolean<T>(this T input)
        {
            return Convert.ToBoolean(input);
        }

        /// <summary>
        /// Chuyển thành tiền tệ x.xxx.xxx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToMoney<T>(this T input)
        {
            if (input == null)
                return "";
            return string.Format("{0:n0}", input);
        }
        /// <summary>
        /// Hàm chuyển con về tiền Việt Nam
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToVND<T>(this T input)
        {
            if (input == null)
                return "";
            return string.Format("{0:n0}", input) + "đ";
        }
        /// <summary>
        /// Hàm kiểm tra số nhập vào có phải là một con số không
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumber(this string input)
        {
            int result;
            return int.TryParse(input, out result);
        }
        /// <summary>
        /// Hàm kiểm tra Email có hợp lệ hay không?
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            string type = ((MemberInfo)value.GetType()).Name;
            var a_ = ((MemberInfo)typeof(char)).Name;
            if (Convert.IsDBNull(value))
            {
                switch (type)
                {
                    case "String":
                        return string.Empty;
                    case "Int64":
                        return Convert.ToInt64(value);
                    case "Int32":
                        return Convert.ToInt32(value);
                    case "DateTime":
                        return Convert.ToDateTime(value);
                    case "Boolean":
                        return Convert.ToBoolean(value);
                    case "Double":
                        return Convert.ToDouble(value);
                    case "Single":
                        return Convert.ToSingle(value);
                    case "Int16":
                        return Convert.ToInt16(value);
                    case "Byte":
                        return Convert.ToByte(value);
                    case "Char":
                        return Convert.ToChar(value);
                    default:
                        return null;
                }
            }
            return Convert.ChangeType(value, t);
        }
        /// <summary>
        /// Convert chuỗi JSON sang DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this string data)
        {
            return JsonConvert.DeserializeObject<DataTable>(data);
        }
        public static List<T> ConvertToList<T>(this DataTable data) where T : class, new()
        {
            List<T> lst = new List<T>();
            if (data == null)
                return lst;
            foreach (var row in data.AsEnumerable())
            {
                T obj = new T();
                foreach (var prop in obj.GetType().GetProperties())
                {
                    PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                    object val = ChangeType(row[prop.Name], propertyInfo.PropertyType);
                    propertyInfo.SetValue(obj, val, null);
                }
                lst.Add(obj);
            }
            return lst;
        }
        /// <summary>
        /// Hàm này chuyển số về tiền VNĐ. Ví Dụ: 10000-> 10.000 VNĐ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int VNDToNumber(this string input)
        {
            string StringNumber = "";
            for (int i = 0; i < input?.Length; i++)
            {
                if (char.IsDigit(input[i]))
                    StringNumber += input[i];
            }
            if (StringNumber.Length > 0)
                return Convert.ToInt32(StringNumber);
            else
                return 0;
        }
        /// <summary>
        /// Hàm chuyển tất cả Tiếng Việt thành Tiếng Việt không dấu và các ký tự đặc biệt thành dấu - đồng thời chuyển thành chữ thường
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string NonUnicode(this string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
                text = text.Replace(" ", "-").Replace("+", "-").Replace(".", "-").Replace(")", "-").Replace("(", "-").Replace("@", "-").Replace("!", "-").Replace("#", "-").Replace("$", "-").Replace("%", "-").Replace("^", "-").Replace("&", "-").Replace("*", "-").Replace("{", "-").Replace("}", "-").Replace("[", "-").Replace("]", "-").Replace("|", "-").Replace("\\", "-").Replace("'", "-").Replace(";", "-").Replace(",", "-").Replace("?", "-").Replace("/", "-").Replace("\"", "-").Replace(":", "-");
            }
            return text.ToLower();
        }

        public static string ToUnicodeHaveSpace(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text.ToLower();
        }

        /// <summary>
        /// Gets the MAC address of the current PC.
        /// </summary>
        /// <returns></returns>
        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
        /// <summary>
        /// Chuyển chuỗi Base64 về lại file
        /// </summary>
        /// <param name="base64string"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        public static void Base64ToFile(this string base64string, string name, string path)
        {
            File.WriteAllBytes(Path.Combine(path, name), Convert.FromBase64String(base64string));
        }
        /// <summary>
        /// Chuyển file sang Base64
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ConvertFileToBase64(this string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// Hàm mã hóa chuỗi với key
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(this string source, string key)
        {
            using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                    tripleDESCryptoService.Key = byteHash;
                    tripleDESCryptoService.Mode = CipherMode.ECB;
                    byte[] data = Encoding.UTF8.GetBytes(source);
                    return Convert.ToBase64String(tripleDESCryptoService.CreateEncryptor().TransformFinalBlock(data, 0, data.Length));
                }
            }
        }
        /// <summary>
        /// Hàm giải mã chuỗi với key
        /// </summary>
        /// <param name="encrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(this string encrypt, string key)
        {
            using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                    tripleDESCryptoService.Key = byteHash;
                    tripleDESCryptoService.Mode = CipherMode.ECB;
                    byte[] data = Convert.FromBase64String(encrypt);
                    byte[] test = tripleDESCryptoService.CreateDecryptor().TransformFinalBlock(data, 0, data.Length);
                    return Encoding.UTF8.GetString(test);
                }
            }
        }
        public static int DateToNumber(this DateTime input)
        {
            string str = string.Concat(input.Year.ToString(), input.Month.ToString().Length == 1 ? "0" + input.Month.ToString() : input.Month.ToString(), input.Day.ToString().Length == 1 ? "0" + input.Day.ToString() : input.Day.ToString(), input.Hour.ToString().Length == 1 ? "0" + input.Hour.ToString() : input.Hour.ToString());
            return str.ToInt();
        }

        public static TimeSpan? ToTimeSpan(this string timeSpanString)
        {
            if (TimeSpan.TryParse(timeSpanString, out var _timeSpan))
            {
                return _timeSpan;
            }

            return null;
        }

        public static string TimeSpanTo_hhmm(this TimeSpan? timeSpan)
        {
            if (timeSpan == null)
            {
                return null;
            }
            return string.Format("{0:00}:{1:00}", timeSpan.Value.Hours, timeSpan.Value.Minutes);
        }

        public static DateTime StartOfDay(this DateTime utcDate)
        {
            return utcDate.Date;
        }

        public static DateTime EndOfDay(this DateTime utcDate)
        {
            return utcDate.Date.AddDays(1).AddTicks(-1);
        }

        //public static DateTime StartOfDayVn(this DateTime utcDate)
        //{
        //    return utcDate.Date.AddHours(7);
        //}

        //public static DateTime EndOfDayVn(this DateTime utcDate)
        //{
        //    return utcDate.Date.AddDays(1).AddTicks(-1).AddHours(7);
        //}
    }
    public class CloneEntities : ICloneable
    {
        object ICloneable.Clone()
        {
            return MemberwiseClone();
        }
    }


}
