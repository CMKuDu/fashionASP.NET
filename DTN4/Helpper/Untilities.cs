using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTN4.Helpper
{
    public static class Untilities
    {
        public static bool IsValidateEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false;
            }
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
        public static int PAGE_SIZE = 20;
        public static void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
        public static string TotitleCase(string str)
        {
            string result = str;
            if (!string.IsNullOrEmpty(str))
            {
                var words = str.Split(' ');
                for (int index = 0; index < words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
                result = string.Join(" ", words);
            }
            return result;
        }
        //public static bool IsInterger(string str)
        //{
        //    Regex regex = new Regex(@"^[0-9]+$");
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(str))
        //        {
        //            return false;
        //        }
        //        if (!regex.IsMatch(str))
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch
        //    {

        //    }
        //}
        public static string GetRandomKey(int lengh = 5)
        {
            string pattern = @"123456789zxcvbnmasdfghjklqwertyuiop[]{}:~!@#$%^&*()+";
            Random rd = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lengh; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }
            return sb.ToString();
        }
        public static string SEOUrl(string url)
        {
            url = url.ToLower();
            url = Regex.Replace(url, @"[áàaảãâấầậẩẫăắằặẳẵ]", "a");
            url = Regex.Replace(url, @"[éèẻẽẹếềểễệ]", "e");
            url = Regex.Replace(url, @"[óòỏõọốồổỗộớờởỡợ]", "o");
            url = Regex.Replace(url, @"[úùủũụửữứừự]", "u");
            url = Regex.Replace(url, @"[íìỉĩị]", "i");
            url = Regex.Replace(url, @"[ýỳỷỹỵ]", "y");
            url = Regex.Replace(url, @"[đ]", "d");

            url = Regex.Replace(url.Trim(), @"[^0-9a-z-\s]", "").Trim();
            url = Regex.Replace(url.Trim(), @"\s+", "-");
            url = Regex.Replace(url, @"\s", "-");
            while (true)
            {
                if (url.IndexOf("--") != -1)
                {
                    url = url.Replace("--", "-");
                }
                else
                {
                    break;
                }
            }
            return url;
        }
        public static async Task<string> UploadFile(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string newname = null)
        {
            try
            {
                if (newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", sDirectory);
                CreateIfMissing(path);
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", sDirectory, newname);
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif", "webp" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt.ToLower()))
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return newname;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
