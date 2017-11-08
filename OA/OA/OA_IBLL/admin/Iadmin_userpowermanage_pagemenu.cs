using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_IBLL.admin
{
    public interface Iadmin_userpowermanage_pagemenu
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        returnresult AddPagemenu(oa_userpower_pagemenu pm);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        returnresult UpdatePagemenu(oa_userpower_pagemenu pm);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        bool DeletePagemenu(oa_userpower_pagemenu pm);

        /// <summary>
        /// 根据菜单id查询菜单信息
        /// </summary>
        /// <param name="menuid">菜单id</param>
        /// <returns></returns>
        oa_userpower_pagemenu QueryPagemenuById(long menuid);

        /// <summary>
        /// 根据公司id查询菜单列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<oa_userpower_pagemenu> QueryPagemenuList(long companyid);
    }
}
