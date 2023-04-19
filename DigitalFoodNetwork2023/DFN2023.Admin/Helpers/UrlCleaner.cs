using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFN2023.Admin.Helpers
{
    public class UrlCleaner
    { 

        public static string GetSanitizedTextForURL(string dirtyText)
        {
            return SanitizeURL(dirtyText);
        }
        public static string SanitizeURL(string strKeyword, bool replacePlusSign = true)
        {
            if (strKeyword != null)
            {
                if (strKeyword.Contains(" "))
                {
                    strKeyword = strKeyword.Replace(" ", "-");
                    strKeyword = strKeyword.Replace("_-", "--");
                }

                if (strKeyword.Contains("Ç") || strKeyword.Contains("ç"))
                {
                    strKeyword = strKeyword.Replace("Ç", "C");
                    strKeyword = strKeyword.Replace("ç", "c");
                }

                if (strKeyword.Contains("ı"))
                {
                    strKeyword = strKeyword.Replace("ı", "i");
                }

                if (strKeyword.Contains("Ğ") || strKeyword.Contains("ğ"))
                {
                    strKeyword = strKeyword.Replace("Ğ", "G");
                    strKeyword = strKeyword.Replace("ğ", "g");
                }

                if (strKeyword.Contains("İ"))
                {
                    strKeyword = strKeyword.Replace("İ", "I");
                }

                if (strKeyword.Contains("Ö") || strKeyword.Contains("ö"))
                {
                    strKeyword = strKeyword.Replace("Ö", "O");
                    strKeyword = strKeyword.Replace("ö", "o");
                }

                if (strKeyword.Contains("Ü") || strKeyword.Contains("ü"))
                {
                    strKeyword = strKeyword.Replace("Ü", "U");
                    strKeyword = strKeyword.Replace("ü", "u");
                }

                if (strKeyword.Contains("Ş") || strKeyword.Contains("ş"))
                {
                    strKeyword = strKeyword.Replace("Ş", "S");
                    strKeyword = strKeyword.Replace("ş", "s");
                }

                strKeyword = strKeyword.Replace("<br />", "-");
                strKeyword = strKeyword.Replace("<br/>", "-");
                strKeyword = strKeyword.Replace("<br>", "-");

                strKeyword = strKeyword.Replace("\"", "");
                strKeyword = strKeyword.Replace("'", "");
                strKeyword = strKeyword.Replace("ˈ", "");
                strKeyword = strKeyword.Replace("ˊ", "");
                strKeyword = strKeyword.Replace("ˋ", "");
                strKeyword = strKeyword.Replace("ʹ", "");
                strKeyword = strKeyword.Replace("ʺ", "");
                strKeyword = strKeyword.Replace("ʻ", "");
                strKeyword = strKeyword.Replace("ʼ", "");
                strKeyword = strKeyword.Replace("ʽ", "");
                strKeyword = strKeyword.Replace("ʾ", "");
                strKeyword = strKeyword.Replace("ʿ", "");
                strKeyword = strKeyword.Replace("`", "");
                strKeyword = strKeyword.Replace("´", "");
                strKeyword = strKeyword.Replace("'", "");
                strKeyword = strKeyword.Replace("“", "");
                strKeyword = strKeyword.Replace("”", "");


                strKeyword = strKeyword.Replace(":", "-");
                strKeyword = strKeyword.Replace(",", "-");
                strKeyword = strKeyword.Replace(".", "-");
                // 15.02.2017
                strKeyword = strKeyword.Replace("&#8594;", "-");
                strKeyword = strKeyword.Replace("&", "-");
                strKeyword = strKeyword.Replace("!", "");
                strKeyword = strKeyword.Replace("?", "");
                strKeyword = strKeyword.Replace("%", "");
                strKeyword = strKeyword.Replace("$", "-");
                strKeyword = strKeyword.Replace("/", "-");
                strKeyword = strKeyword.Replace("*", "-");
                if (replacePlusSign)
                    strKeyword = strKeyword.Replace("+", "-");
                else
                    strKeyword = strKeyword.Replace("+", "");
                strKeyword = strKeyword.Replace("^", "-");
                strKeyword = strKeyword.Replace("~", "-");
                strKeyword = strKeyword.Replace("(", "-");
                strKeyword = strKeyword.Replace(")", "-");
                strKeyword = strKeyword.Replace("[", "-");
                strKeyword = strKeyword.Replace("]", "-");
                strKeyword = strKeyword.Replace("@", "-");
                strKeyword = strKeyword.Replace("#", "-");
                strKeyword = strKeyword.Replace("$", "-");
                strKeyword = strKeyword.Replace("£", "-");
                strKeyword = strKeyword.Replace("|", "-");
                strKeyword = strKeyword.Replace("_-", "--");
                strKeyword = strKeyword.Replace("----", "-");
                strKeyword = strKeyword.Replace("---", "-");
                strKeyword = strKeyword.Replace("--", "-");
                strKeyword = strKeyword.Replace("→", "-");


                return strKeyword;
            }
            else return "x";
        }

        public static string RemoveImagesFromBlog(string IncomingHTML, int length)
        {
            string resstring = IncomingHTML.Substring(0, IncomingHTML.Substring(0, length).LastIndexOf(" "));

            if (resstring.IndexOf("<img") > 0)
            {
                int strt = 0;
                int end = 0;

                // img tagın başından kapanışına kadar olan alanı yok et 
                strt = resstring.IndexOf("<img");
                end = resstring.Substring(strt).IndexOf(">");
                resstring = resstring.Replace(resstring.Substring(strt, strt + end + 2), "");

            }
            resstring = resstring.Replace("<p> </p>", "");
            resstring = resstring.Replace("<em>", "");
            resstring = resstring.Replace("</em>", "");

            return resstring;

        }

    }

}
