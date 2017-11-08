using OA_IBLL.user;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.user
{
    public class userpowermanage_pagemenu : BaseUserService, Iuserpowermanage_pagemenu
    {

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public returnresult UpdatePagemenu(oa_userpower_pagemenu pm)
        { 
            returnresult rr = new returnresult();
            if (Exist<oa_userpower_pagemenu>(a => a.name == pm.name && a.id != pm.id && a.companyid==pm.companyid))
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
        /// 根据菜单id查询菜单信息
        /// </summary>
        /// <param name="menuid">菜单id</param>
        /// <returns></returns>
        public oa_userpower_pagemenu QueryPagemenuById(long menuid)
        {
            return Query<oa_userpower_pagemenu>(a=>a.id==menuid);
        }

        /// <summary>
        /// 根据菜单id和公司id查询菜单信息
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_userpower_pagemenu QueryPageMenuByIdCompanyid(long id, long companyid)
        {
            return Query<oa_userpower_pagemenu>(a=>a.id==id && a.companyid==companyid);
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
