using OA_IBLL.admin;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.admin
{
    public class admin_userpowermanage_powerlog : BaseAdminService, Iadmin_userpowermanage_powerlog
    {
        /// <summary>
        /// 新增用户权限日志
        /// </summary>
        /// <param name="pl"></param>
        /// <returns></returns>
        public bool AddPowerlog(oa_userpower_powerlogs pl)
        {
            return Add<oa_userpower_powerlogs>(pl);
        }

        /// <summary>
        /// 根据公司id查询用户权限日志
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<oa_userpower_powerlogs> QueryPowerlogList(long companyid)
        {
            return QueryList<oa_userpower_powerlogs>(a=>a.companyid==companyid).OrderByDescending(a=>a.id).ToList();
        }
    }
}
