using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Web;

namespace BotProject.Common
{
    /// <summary>
    /// Convert data 
    /// </summary>
    public class ConvertData
    {
        /// <summary>
        /// Convert base64 string to image.
        /// </summary>
        /// <param name="base64String"> base64 string data image.</param>
        /// <returns> return image</returns>
        public static Image Base64ToImage(string base64String)
        {
            var base64Data = Regex.Match(base64String, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64Data);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        /// <summary>
        /// Convert image to base 64 string.
        /// </summary>
        /// <param name="inforImage">information path of image suchas: name,id</param>
        /// <returns> return base 64 string</returns>
        public static string ImageToBase64String(string inforImage,string path)
        {
            string base64String = "";
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/fileman/Uploads/")+ path +"/" + inforImage);
			if (File.Exists(filePath))
			{
				using (Image image = Image.FromFile(filePath))
				{
					using (MemoryStream m = new MemoryStream())
					{
						image.Save(m, image.RawFormat);
						byte[] imageBytes = m.ToArray();

						// Convert to file image.
						//return "<img src=\"data:image/"
						//   + Path.GetExtension(filePath).Replace(".", "")
						//   + ";base64,"
						//   + Convert.ToBase64String(File.ReadAllBytes(filePath)) + "\" />";

						// Convert to string data.
						base64String = "data:image/" + Path.GetExtension(filePath).Replace(".", "") + "; base64," + Convert.ToBase64String(imageBytes);
					}
				}
			}
			return base64String;
		}
    }
}
