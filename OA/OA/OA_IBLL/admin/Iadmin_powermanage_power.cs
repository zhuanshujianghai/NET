using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_Models;

namespace OA_IBLL.admin
{
    /// <summary>
    /// 权限
    /// </summary>
    public interface Iadmin_powermanage_power
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        returnresult AddPageMenu(oa_power_pagemenu pm);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        returnresult UpdatePageMenu(oa_power_pagemenu pm);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        bool DeletePageMenu(oa_power_pagemenu pm);

        /// <summary>
        /// 根据菜单id查询菜单信息
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <returns></returns>
        oa_power_pagemenu QueryPageMenuById(long id);

        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <returns></returns>
        List<oa_power_pagemenu> QueryPageMenuList();

        /// <summary>
        /// 查询权限日志
        /// </summary>
        /// <returns></returns>
        List<view_oa_power_powerloglist> QueryPowerLogList();

        /// <summary>
        /// 查询管理员操作日志
        /// </summary>
        /// <returns></returns>
        List<view_oa_power_dologlist> QueryDoLogList();
    }
}
