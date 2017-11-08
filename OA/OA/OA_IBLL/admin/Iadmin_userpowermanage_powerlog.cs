using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_Models;

namespace OA_IBLL.admin
{
    /// <summary>
    /// 
    /// </summary>
    public interface Iadmin_userpowermanage_powerlog
    {
        /// <summary>
        /// 新增用户权限日志
        /// </summary>
        /// <param name="pl"></param>
        /// <returns></returns>
        bool AddPowerlog(oa_userpower_powerlogs pl);

        /// <summary>
        /// 根据公司id查询用户权限日志
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<oa_userpower_powerlogs> QueryPowerlogList(long companyid);
    }
}
