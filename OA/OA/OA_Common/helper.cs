using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Common
{

    public static class helper
    {
        #region 扩展方法
        /// <summary>
        /// 转换为int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this object obj)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }
        /// <summary>
        /// object 扩展方法转换为int 如果转换失败返回传过来的值；
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this object str, int j)
        {
            int i = 0;
            try
            {
                i = int.Parse(str.ToString());
            }
            catch (Exception)
            {
                i = j;
            }
            return i;
        }
        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToLong(this object obj)
        {
            long result = 0;
            try
            {
                result = Convert.ToInt64(obj);
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }
        /// <summary>
        /// 转换为decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj)
        {
            decimal result = 0;
            try
            {
                result = Convert.ToDecimal(obj);
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }
        /// <summary>
        /// 将时间格式字符串转换为时间(默认时间1970-01-01 08:00:00)
        /// </summary>
        /// <param name="datetimestr"></param>
        /// <returns></returns>
        public static DateTime ToDatetime(this string datetimestr)
        {
            DateTime result = new DateTime();
            try
            {
                result = Convert.ToDateTime(datetimestr);
            }
            catch (Exception)
            {
                result = Convert.ToDateTime("1970-01-01 08:00:00");
            }
            return result;
        }
        /// <summary>
        /// 将时间戳转换为时间
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ToDatetime(this int timestamp)
        {
            DateTime result = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            try
            {
                long lTime = long.Parse(timestamp + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                return result.Add(toNow);
            }
            catch (Exception)
            {
                result = Convert.ToDateTime("1970-01-01 08:00:00");
            }
            return result;
        }
        #endregion
    }
}
