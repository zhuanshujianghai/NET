using OA_IBLL.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_Models;

namespace OA_BLL.admin
{
    public class admin_powermanage_power : BaseAdminService, Iadmin_powermanage_power
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public returnresult AddPageMenu(oa_power_pagemenu pm)
        { 
            returnresult rr = new returnresult();
            if (Exist<oa_power_pagemenu>(a => a.name == pm.name))
            {
                rr.status = false;
                rr.msg = "该菜单已存在";
            }
            else
            {
                rr.status = Add<oa_power_pagemenu>(pm);
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
        public returnresult UpdatePageMenu(oa_power_pagemenu pm)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_power_pagemenu>(a => a.name == pm.name && a.id!=pm.id))
            {
                rr.status = false;
                rr.msg = "该菜单已存在";
            }
            else
            {
                rr.status = Update<oa_power_pagemenu>(pm);
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
        public bool DeletePageMenu(oa_power_pagemenu pm)
        {
            return Delete<oa_power_pagemenu>(pm);
        }

        /// <summary>
        /// 根据菜单id查询菜单信息
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <returns></returns>
        public oa_power_pagemenu QueryPageMenuById(long id)
        {
            return Query<oa_power_pagemenu>(a=>a.id==id);
        }

        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <returns></returns>
        public List<oa_power_pagemenu> QueryPageMenuList()
        {
            return QueryList<oa_power_pagemenu>(a=>a.id>0);
        }

        /// <summary>
        /// 查询权限日志
        /// </summary>
        /// <returns></returns>
        public List<view_oa_power_powerloglist> QueryPowerLogList()
        {
            return QueryList<view_oa_power_powerloglist>(a=>a.id>0).OrderByDescending(a=>a.addtime).ToList();
        }

        /// <summary>
        /// 查询管理员操作日志
        /// </summary>
        /// <returns></returns>
        public List<view_oa_power_dologlist> QueryDoLogList()
        {
            return QueryList<view_oa_power_dologlist>(a => a.id > 0).OrderByDescending(a => a.addtime).ToList();
        }
    }
}
