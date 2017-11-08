using OA_IBLL.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.admin
{
    /// <summary>
    /// 首页类
    /// </summary>
    public class admin_membermanage_index : BaseAdminService, Iadmin_membermanage_index
    {
        /// <summary>
        /// 根据url获取天气
        /// </summary>
        /// <param name="url">如http://php.weather.sina.com.cn/xml.php?city=%D6%D8%C7%EC&password=DJOYnieT8234jlsK&day=4</param>
        /// <returns>天气信息</returns>
        public string getweather(string url)
        {
            string result = "";
            
            return result;
        }
    }
}
