using OA_IBLL.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_Models;
using System.Web.Routing;

namespace OA_BLL.user
{
    public class BaseUserService : BaseService, IBaseUserService
    {
        /// <summary>
        /// 写入操作日志
        /// </summary>
        /// <param name="doinfo">操作详情</param>
        /// <param name="type">操作类型</param>
        /// <param name="staffid">操作人id</param>
        /// <param name="dt">时间</param>
        /// <returns>布尔值</returns>
        public bool writedolog(string doinfo, string type, long staffid, DateTime dt)
        {
            //写入操作日志
            oa_member_dologs dg = new oa_member_dologs();
            dg.addip = getip();
            dg.addtime = dt;
            dg.doinfo = doinfo;
            dg.staffid = staffid;
            dg.type = type;
            return Add<oa_member_dologs>(dg);
        }

        /// <summary>
        /// 写入用户权限操作日志
        /// </summary>
        /// <param name="pl">用户权限表实体</param>
        /// <returns></returns>
        public bool writeuserpowerdolog(oa_userpower_powerlogs pl)
        {
            return Add<oa_userpower_powerlogs>(pl);
        }
    }
}
