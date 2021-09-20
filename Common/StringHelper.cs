using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Common
{
   public static class StringHelper
    {
        /// <summary>
        /// 将数字字符串转换成数字
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static int GetInt(this string strValue)
        {
            int reInt = 0;
            int.TryParse(strValue, out reInt);
            return reInt;
        }

        public static decimal GetDecimal(this string strValue)
        {
            decimal reInt = 0;
            decimal.TryParse(strValue, out reInt);
            return reInt;
        }

        public static int GetInt(this object oValue)
        {
            int reInt = 0;
            try
            {
                reInt = Convert.ToInt32(oValue);
            }
            catch
            {
                reInt = 0;
            }
            return reInt;
        }

        public static List<string> GetStrList(this string str,char sparater,bool toLower)
        {
            List<string> list = new List<string>();
            string[] ss = str.Split(sparater);
            foreach(var s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != sparater.ToString())
                {
                    string strValue = s;
                    if (toLower)
                    {
                        strValue = s.ToLower();
                    }
                    list.Add(strValue);
                }
            }
            return list;
        }
    }
}
