using Microsoft.AspNetCore.Components;

namespace FLL.Utils
{
    public static class ClassExtension
    {
        /// <summary>
        /// Replace \n by <br/>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringWithBr(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return str.Replace("\n", "<br/>");
        }

        public static MarkupString ToMarkup(this string str)
        {
            return (MarkupString)str;
        }

        public static string Bold(this string str)
        {
            return str.AddTag("span", "fw-bold");
        }

        /// <summary>
        /// String format
        /// </summary>
        /// <param name="str"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string F(this string str, params object[] p)
        {
            return string.Format(str, p);
        }

        /// <summary>
        /// Add br
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Br(this string str)
        {
            return $"{str}<br/>";
        }

        /// <summary>
        /// Add ...
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Dots(this string str)
        {
            return $"{str}...";
        }

        /// <summary>
        /// Surrond with <p>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string P(this string str)
        {
            return str.AddTag("p");
        }

        public static string AddTag(this string str, string tag, string classValue = "")
        {
            if (string.IsNullOrEmpty(classValue))
                return $"<{tag}>{str}</{tag}>";
            else
                return $"<{tag} class='{classValue}'>{str}</{tag}>";
        }

        public static string NoWrap(this string str)
        {
            return str.AddTag("div", "d-inline text-nowrap me-2");
        }

        public static string CleanUrl(this string str)
        {
            string newStr = str;
            newStr = newStr.RemoveStart("http://");
            newStr = newStr.RemoveStart("https://");
            newStr = newStr.RemoveStart("www.");
            newStr = newStr.RemoveStart("facebook.com");
            newStr = newStr.RemoveStart("fb.com");
            newStr = newStr.RemoveStart("instagram.com");
            newStr = newStr.RemoveStart("twitter.com");
            newStr = newStr.RemoveStart("youtube.com");
            newStr = newStr.RemoveStart("m.youtube.com");
            newStr = newStr.RemoveStart("youtu.be");
            newStr = newStr.RemoveStart("tiktok.com");
            newStr = newStr.RemoveStart("/");
            newStr = newStr.RemoveStart("@");

            newStr = newStr.RemoveEnd("hl=en");
            newStr = newStr.RemoveEnd("hl=fr");
            newStr = newStr.RemoveEnd("lang=en");
            newStr = newStr.RemoveEnd("utm_medium=copy_link");
            newStr = newStr.RemoveEnd("/");
            newStr = newStr.RemoveEnd("?");
            newStr = newStr.RemoveEnd("/");
            return newStr;
        }

        public static string LimitLength(this string str, int length)
        {
            if (str.Length < length)
                return str;
            return str[..length] + "...";
        }

        public static string CheckUrl(this string str, string media)
        {
            string newStr = str.CleanUrl();

            media = media.ToUpper();

            if (media.Contains("FACEBOOK"))
                return $"https://facebook.com/{newStr}";
            if (media.Contains("INSTA"))
                return $"https://instagram.com/{newStr}";
            if (media.Contains("TWITTER"))
                return $"https://twitter.com/{newStr}";
            if (media.Contains("YOUTUBE"))
                return $"https://youtube.com/{newStr}";
            if (media.Contains("TIKTOK"))
                return $"https://tiktok.com/@{newStr}";

            if (str.StartsWith("http://") || str.StartsWith("https://"))
                return str;
            else
                return $"https://{str}";
        }

        /// <summary>
        /// If exists at the beginning of the string, remove the needle string.
        /// Case insensitive
        /// </summary>
        /// <param name="str"></param>
        /// <param name="needle"></param>
        /// <returns></returns>
        public static string RemoveStart(this string str, string needle)
        {
            if (str.ToUpper().StartsWith(needle.ToUpper()))
                return str[needle.Length..];
            return str;
        }

        public static string RemoveEnd(this string str, string needle)
        {
            if (str.ToUpper().EndsWith(needle.ToUpper()))
                return str[0..^needle.Length];
            return str;
        }

        public static string Anonymize(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (str.Length < 2)
                return "**";

            if (str.Length < 4)
                return str[0] + "**" + str[^1];

            if (str.Contains('@'))
                return str[0..2] + "**@**" + str[^2..];

            return str[0..2] + "***" + str[^2..];
        }
    }
}