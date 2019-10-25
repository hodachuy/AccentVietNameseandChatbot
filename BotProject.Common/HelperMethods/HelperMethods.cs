using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BotProject.Common
{
    public static class HelperMethods
    {
        public static string ToUnsignString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }
            return str2;
        }

        /// <summary>
        /// Replace HTML template with values
        /// </summary>
        /// <param name="template">Template content HTML</param>
        /// <param name="replacements">Dictionary with key/value</param>
        /// <returns></returns>
        public static string Parse(this string template, Dictionary<string, string> replacements)
        {
            if (replacements.Count > 0)
            {
                template = replacements.Keys
                            .Aggregate(template, (current, key) => current.Replace(key, replacements[key]));
            }
            return template;
        }


		/// <summary>
		/// Check extension of file image
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		public static string ImageType(this Image image)
		{
			if (image.RawFormat.Equals(ImageFormat.Bmp))
			{
				return "Bmp";
			}
			else if (image.RawFormat.Equals(ImageFormat.MemoryBmp))
			{
				return "BMP";
			}
			else if (image.RawFormat.Equals(ImageFormat.Wmf))
			{
				return "Emf";
			}
			else if (image.RawFormat.Equals(ImageFormat.Wmf))
			{
				return "Wmf";
			}
			else if (image.RawFormat.Equals(ImageFormat.Gif))
			{
				return ".gif";
			}
			else if (image.RawFormat.Equals(ImageFormat.Jpeg))
			{
				return ".jpg";
			}
			else if (image.RawFormat.Equals(ImageFormat.Png))
			{
				return ".png";
			}
			else if (image.RawFormat.Equals(ImageFormat.Tiff))
			{
				return "Tiff";
			}
			else if (image.RawFormat.Equals(ImageFormat.Exif))
			{
				return "Exif";
			}
			else if (image.RawFormat.Equals(ImageFormat.Icon))
			{
				return ".ico";
			}

			return ".jpg";
		}

	    public static void SaveImageByFormat(Image image,string filePath)
        {
            if (image.RawFormat.Equals(ImageFormat.Png))
            {
                image.Save(filePath, ImageFormat.Png);
            }
            else if (image.RawFormat.Equals(ImageFormat.Jpeg))
            {
                image.Save(filePath, ImageFormat.Jpeg);
            }
            else if (image.RawFormat.Equals(ImageFormat.Gif))
            {
                image.Save(filePath, ImageFormat.Gif);
            }
            else
            {
                image.Save(filePath, ImageFormat.Jpeg);
            }
        }

        public static string EscapeXml(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return text.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("'", "&apos;").Replace("<", "&lt;").Replace(">", "&gt;");
            }
            return text;
        }

        public const int OnWeekDay = 1;
        public const int OffWeekDay = 0;
        /// <summary>
        /// Thời gian làm việc trả lời khách hàng
        /// T2-CN : 8h00 - 12h00, 13h00 - 17h30
        /// </summary>
        /// <returns></returns>
        public static bool IsTimeInWorks()
        {
            bool rs = false;
            int T7CN = Int32.Parse(ConfigHelper.ReadString("T7CN"));
            DateTime timeCurrent = DateTime.Now;
            if(T7CN == OnWeekDay)
            {
                if ((timeCurrent.DayOfWeek == DayOfWeek.Saturday) || (timeCurrent.DayOfWeek == DayOfWeek.Sunday))
                {
                    rs = false;
                    return rs;
                }
            }
            if ((timeCurrent.Hour >= 8 && timeCurrent.Hour < 12))
            {
                rs = true;
            }
            else if (timeCurrent.Hour >= 13 && (timeCurrent.TimeOfDay < System.TimeSpan.Parse("17:30:00")))
            {
                rs = true;
            }
            else
            {
                rs = false;
            }

            return rs;
        }
	}
}
