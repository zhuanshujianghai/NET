using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_IBLL.admin
{
    /// <summary>
    /// memmber首页接口
    /// </summary>
    public interface Iadmin_membermanage_index
    {
        /// <summary>
        /// 根据url获取天气
        /// </summary>
        /// <param name="url">如http://php.weather.sina.com.cn/xml.php?city=%D6%D8%C7%EC&password=DJOYnieT8234jlsK&day=4</param>
        /// <returns>天气信息</returns>
        string getweather(string url);
    }
}
