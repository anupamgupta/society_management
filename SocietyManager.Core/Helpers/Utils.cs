using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace SocietyManager.Core.Helpers
{
    public static class Utils
    {
        /// <summary>
        /// Calculates new rating based on old ratings and new rating
        /// </summary>
        /// <param name="raters">Old Raters</param>
        /// <param name="rating">Old Rating</param>
        /// <param name="newRating">New Rating</param>
        /// <returns>Returns new calculated rating</returns>
        public static double CalculateRating(int raters, double rating, double newRating)
        {
            double calcRating = ((raters * rating) + newRating) / (raters + 1);
            calcRating = Math.Floor((calcRating * 2) + .5) / 2;
            return calcRating;
        }

        /// <summary>
        /// Returns MD5 hash of given input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5Hash(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
                return String.Empty;

            //Step 1 : Calculate MD5 Hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            //Step 2 : Convert byte array to Hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        public static string GetSHA256Hash(string input)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            using (HashAlgorithm sha = new SHA256Managed())
            {
                byte[] encryptedBytes = sha.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public static string GenerateRandomString()
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < 6; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static string GetSummary(string html, int length)
        {
            string text = Utils.StripHtml(html);
            if (text.Length > length)
            {
                text = text.Substring(0, length) + "...";
                text = "\"" + text.Trim() + "\"";
            }
            return text;
        }

        public static string RemoveIllegalCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            text = text.Replace(":", string.Empty);
            text = text.Replace("/", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("#", string.Empty);
            text = text.Replace("[", string.Empty);
            text = text.Replace("]", string.Empty);
            text = text.Replace("@", string.Empty);
            text = text.Replace("*", string.Empty);
            text = text.Replace(".", string.Empty);
            text = text.Replace(",", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("&", string.Empty);
            text = text.Replace("'", string.Empty);
            text = text.Replace(" ", "-");
            text = RemoveDiacritics(text);
            text = RemoveExtraHyphen(text);

            return HttpUtility.UrlEncode(text).Replace("%", string.Empty);
        }

        private static string RemoveExtraHyphen(string text)
        {
            if (text.Contains("--"))
            {
                text = text.Replace("--", "-");
                return RemoveExtraHyphen(text);
            }
            return text;
        }

        private static string RemoveDiacritics(string text)
        {
            String normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < normalized.Length; i++)
            {
                Char c = normalized[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString();
        }

        private static Regex _Regex = new Regex("<[^>]*]>", RegexOptions.Compiled);
        /// <summary>
        /// Removes all HTML Tags from a given string
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string StripHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return _Regex.Replace(html, string.Empty);
        }

    }
}
