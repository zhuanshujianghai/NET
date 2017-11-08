using OA_IBLL.admin;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.admin
{
    public class admin_userpowermanage_pagemenu:BaseAdminService,Iadmin_userpowermanage_pagemenu
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public returnresult AddPagemenu(oa_userpower_pagemenu pm)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_userpower_pagemenu>(a => a.name == pm.name && a.companyid == pm.companyid))
            {
                rr.status = false;
                rr.msg = "菜单名称已存在";
            }
            else
            {
                rr.status = Add<oa_userpower_pagemenu>(pm);
                if (rr.status)
                {
                    rr.msg = "新增菜单成功";
                }
                else
                {
                    rr.msg = "新增菜单失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public returnresult UpdatePagemenu(oa_userpower_pagemenu pm)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_userpower_pagemenu>(a => a.name == pm.name && a.id != pm.id && a.companyid == pm.companyid))
            {
                rr.status = false;
                rr.msg = "菜单名称已存在";
            }
            else
            {
                rr.status = Update<oa_userpower_pagemenu>(pm);
                if (rr.status)
                {
                    rr.msg = "修改菜单成功";
                }
                else
                {
                    rr.msg = "修改菜单失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public bool DeletePagemenu(oa_userpower_pagemenu pm)
        {
            return Delete<oa_userpower_pagemenu>(pm);
        }

        /// <summary>
        /// 根据菜单id查询菜单信息
        /// </summary>
        /// <param name="menuid">菜单id</param>
        /// <returns></returns>
        public oa_userpower_pagemenu QueryPagemenuById(long menuid)
        {
            return Query<oa_userpower_pagemenu>(a=>a.id==menuid);
        }

        /// <summary>
        /// 根据公司id查询菜单列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<oa_userpower_pagemenu> QueryPagemenuList(long companyid)
        {
            return QueryList<oa_userpower_pagemenu>(a=>a.companyid==companyid);
        }
    }
}
