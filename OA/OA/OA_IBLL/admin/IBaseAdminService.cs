using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_IBLL.admin
{
    public interface IBaseAdminService:IBaseService
    {
        /// <summary>
        /// 写入操作日志
        /// </summary>
        /// <param name="doinfo">操作详情</param>
        /// <param name="type">操作类型</param>
        /// <param name="auserid">操作人id</param>
        /// <param name="dt">时间</param>
        /// <returns>布尔值</returns>
        bool writedolog(string doinfo, string type, long auserid, DateTime dt);

        /// <summary>
        /// 写入权限操作日志
        /// </summary>
        /// <param name="pl">权限表实体</param>
        /// <returns></returns>
        bool writepowerdolog(oa_power_powerlogs pl);
    }
}
