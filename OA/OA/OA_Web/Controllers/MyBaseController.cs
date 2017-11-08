using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace OA_Web.Controllers
{
    public class MyBaseController : Controller
    {
        //常用固定常量

        /// <summary>
        /// bug类型
        /// </summary>
        public enum enum_projectsbugs_type { 代码错误 = 0, 界面优化 = 1, 设计缺陷 = 2, 配置相关 = 3, 安装部署 = 4, 安全相关 = 5, 性能问题 = 6, 标准规范 = 7, 测试脚本 = 8, 其他 = 9 }
        /// <summary>
        /// bug状态
        /// </summary>
        public enum enum_projecstbugs_status { 未确认 = 0, 未解决 = 1, 未关闭 = 2, 已完成 = 3 }
        /// <summary>
        /// bug严重优先级别
        /// </summary>
        public enum enum_projectsbugs_degree { 普通 = 0, 重要 = 1, 紧急 = 2 }



        #region 类型转换扩展方法
        /// <summary>
        /// 检查字符串是否为null
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>布尔值（true：字符串为null，false：字符串不为null）</returns>
        public bool checknull(string str)
        {
            bool result = false;
            if (str == null)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 检查字符串是否为空字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>布尔值（true：字符串为空，false：字符串不为空）</returns>
        public bool checkkong(string str)
        {
            bool result = false;
            if (str == "")
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 检查字符串是否为空字符或null
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>布尔值（true：字符串为null或空，false：字符串不为null且不为空）</returns>
        public bool checknullorkong(string str)
        {
            bool result = false;
            if (str == "" || str == null)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 检查字符串是否为纯汉字
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>布尔值（true：字符串为纯汉字，false：字符串不为纯汉字）</returns>
        public bool checkhanzi(string str)
        {
            bool result = false;
            if (Regex.IsMatch(str, @"^[\u4e00-\u9fa5]+$"))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 检查字符串长度
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="min">最小长度（包含该值）</param>
        /// <param name="max">最大长度（包含该值）</param>
        /// <returns>布尔值（true：字符串符合长度要求，false：字符串不符合长度要求）</returns>
        public bool checklength(string str, int min, int max)
        {
            bool result = false;
            if (str.Length < min || str.Length > max)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 将枚举转换为select下拉列表选项
        /// </summary>
        /// <param name="enmu">枚举</param>
        /// <param name="id">默认选中id</param>
        /// <returns></returns>
        public string getoption<T>(long id)
        {
            string result = string.Empty;
            //遍历枚举所有值
            foreach (int i in Enum.GetValues(typeof(T)))
            {
                string temp = string.Format("<option value='{0}'>{1}</option>", i, Enum.GetName(typeof(T), i));
                if (id == i)
                {
                    result = temp + result;
                }
                else
                {
                    result = result + temp;
                }
            }
            return "\"" + result + "\"";
        }
        /// <summary>
        /// 根据值获取枚举的名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public string getnamebyvalue<T>(long value)
        {
            return Enum.GetName(typeof(T), value);
        }
        #endregion
	}
}