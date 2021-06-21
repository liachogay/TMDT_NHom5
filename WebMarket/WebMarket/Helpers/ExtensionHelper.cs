using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace WebMarket.Helpers
{
    public static class ExtensionHelper
    {
        public static string ToVnd(this double giaTri)
        {
            return $"{giaTri:#,##0.00} đ";
        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
        public static string UploadFile(IFormFile file,string folder="")
        {
            if (file == null || file.Length == 0)
                return "error";
            else
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images",folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                string newpath = string.Concat("~/images/",folder,"/",file.FileName);
                return newpath;
            }
        }
        public static bool isLogin { get; set; } = false;
    }
}