using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoundTools.Common
{
    public class BaseUtil
    {
        public static bool IsEmpty(object avalue)
        {
            if (avalue is string)
            {
                return (avalue.ToString().Trim()) == "";
            }
            else
            {
                return (avalue == null) || (avalue == DBNull.Value);
            }

        }

        public static bool IsStrongEmpty(object avalue)
        {
            if (avalue is int)
                return ((int)avalue) <= 0;
            else if (avalue is double)
                return BaseUtil.VarToInt(avalue) <= 0;
            else
                return IsEmpty(avalue);
        }

        public static int VarToInt(object o)
        {
            if (IsEmpty(o))
                return 0;
            else
                return Convert.ToInt32(o);
        }

        public static double VarToFloat(object o)
        {
            if (IsEmpty(o))
                return 0;
            else
                return Convert.ToDouble(o);
        }
        public static decimal VarToDecimal(object o)
        {
            if (IsEmpty(o))
                return 0;
            else
                return Convert.ToDecimal(o);
        }
        public static object VarToDecimalNull(object o)
        {
            if (IsEmpty(o))
                return null;
            else
                return Convert.ToDecimal(o);
        }
        public static byte VarToByte(object o)
        {
            if (IsEmpty(o))
                return 0;
            else
                return Convert.ToByte(o);
        }
        public static string VarToStr(object o)
        {
            if (IsEmpty(o))
                return "";
            else
                return Convert.ToString(o).Trim();
        }

        public static Boolean VarToBool(object o)
        {
            if (IsEmpty(o))
                return false;
            else
            {
                switch (BaseUtil.VarToStr(o))
                {
                    case "1":
                        return true;
                    case "0":
                        return false;
                    default:
                        return Convert.ToBoolean(o);
                }

            }
        }

        public static DateTime VarToDate(object o)
        {
            if (IsEmpty(o))
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(o);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        public static bool InSet(object source, params object[] destset)
        {
            bool b = false;
            foreach (object o in destset)
            {
                if (source.ToString() == o.ToString())
                {
                    b = true;
                    break;
                }
            }
            return b;
        }

        public static string TransformMoney(double money)
        {
            ///定义大写数组
            string[] odxc = { "分", "角", "圆", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿" };
            string[] odxs = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string result = "";
            string ms = money.ToString(".00");
            //除去小数点
            ms = ms.Replace(".", "");
            int len = ms.Length;
            if (len > odxc.Length)
            {
                return "";
            }
            //转换大写
            for (int i = 0; i < len; i++)
            {
                int v = Convert.ToInt32(ms.Substring(i, 1));
                result += odxs[v] + odxc[len - i - 1];
            }
            return result;
        }

        public static string EncodeKey(object keyValue, string addiMess)
        {
            if (!string.IsNullOrEmpty(BaseUtil.VarToStr(keyValue).Trim()))
            {
                return "[" + BaseUtil.VarToStr(keyValue).Trim() + "] " + BaseUtil.VarToStr(addiMess).Trim();
            }
            return string.Empty;
        }


        public static string[] DecodeKeys(string keys)
        {
            return keys.Split(';');
        }
        public static string GetExceptMess(Exception e)
        {
            return (e.InnerException != null) ? e.InnerException.Message : e.Message;
        }
        public static string FormatMoney(double o)
        {
            return o.ToString("0.00");
        }


    }
}
