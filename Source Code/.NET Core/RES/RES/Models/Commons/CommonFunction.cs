using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RES.Models.Common
{
    public static class CommonFunction
    {
        public static string getExtensionFileName(string nameOfFile)
        {
            string result = "";

            for (int i = nameOfFile.Length - 1; i >= 0; i--)
            {
                if (nameOfFile[i] == '.')
                {
                    result = "." + result;
                    break;
                }
                else
                {
                    result = nameOfFile[i] + result;
                }
            }

            return result;
        }

        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                "đ","é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ","í","ì","ỉ","ĩ","ị","ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ",
                "ơ","ớ","ờ","ở","ỡ","ợ","ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự","ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                "a", "a", "a", "a","d","e","e","e","e","e","e","e","e","e","e","e","i","i","i","i","i","o",
                "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","u","u","u","u","u","u","u",
                "u","u","u","u","y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }

            text = RemoveSpecialCharacters(text);

            return text;
        }

        private static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '-' || c == ' ' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static DateTime FirstDateInWeek(DateTime dt, DayOfWeek weekStartDay)
        {
            while (dt.DayOfWeek != weekStartDay)
                dt = dt.AddDays(-1);
            return dt;
        }

        public static string getAddress(string input)
        {
            string result = "";
            int check = 0;

            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (input[i] == ',')
                {
                    check++;
                    if (check < 2)
                    {
                        result = input[i] + result;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    result = input[i] + result;
                }
            }

            return result;
        }

        public static string getDetailAddress(string input)
        {
            string result = "";
            int check = 0;

            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (input[i] == ',')
                {
                    check++;
                    if (check == 2)
                    {
                        result = input.Substring(0, i);
                    }
                }
            }

            if (result == "")
                return input;

            return result;
        }

        public static string GetTimeOfPost(DateTime? dtNull)
        {
            DateTime dt = dtNull ?? DateTime.Now;

            string result = (DateTime.Now.Subtract(dt).Days / 365).ToString();

            if (result != "0")
            {
                result += " năm trước";
            }
            else
            {
                result = (DateTime.Now.Subtract(dt).Days / 30).ToString();

                if (result != "0")
                {
                    result += " tháng trước";
                }
                else
                {
                    result = (DateTime.Now.Subtract(dt).Days / 7).ToString();

                    if (result != "0")
                    {
                        result += " tuần trước";
                    }
                    else
                    {
                        result = DateTime.Now.Subtract(dt).Days.ToString();

                        if (result != "0")
                        {
                            result += " ngày trước";
                        }
                        else
                        {
                            result = DateTime.Now.Subtract(dt).Hours.ToString();

                            if (result != "0")
                            {
                                result += " giờ trước";
                            }
                            else
                            {
                                result = DateTime.Now.Subtract(dt).Minutes.ToString();

                                if (result != "0")
                                {
                                    result += " phút trước";
                                }
                                else
                                {
                                    result = DateTime.Now.Subtract(dt).Seconds.ToString();

                                    result += " giây trước";
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
