using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_IBLL.admin;
using OA_Models;

namespace OA_BLL.admin
{
    public class BaseAdminService : BaseService , IBaseAdminService
    {
        /// <summary>
        /// 写入操作日志
        /// </summary>
        /// <param name="doinfo">操作详情</param>
        /// <param name="type">操作类型</param>
        /// <param name="auserid">操作人id</param>
        /// <param name="dt">时间</param>
        /// <returns>布尔值</returns>
        public bool writedolog(string doinfo, string type, long auserid, DateTime dt)
        {
            //写入操作日志
            oa_power_dologs dg = new oa_power_dologs();
            dg.addip = getip();
            dg.addtime = dt;
            dg.doinfo = doinfo;
            dg.auserid = auserid;
            dg.type = type;
            return Add<oa_power_dologs>(dg);
        }

        /// <summary>
        /// 写入权限操作日志
        /// </summary>
        /// <param name="pl">权限表实体</param>
        /// <returns></returns>
        public bool writepowerdolog(oa_power_powerlogs pl)
        {
            return Add<oa_power_powerlogs>(pl);
        }
    }
}
