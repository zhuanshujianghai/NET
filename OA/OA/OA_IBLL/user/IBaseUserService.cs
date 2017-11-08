using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_Models;

namespace OA_IBLL.user
{
    public interface IBaseUserService:IBaseService
    {
        /// <summary>
        /// 写入操作日志
        /// </summary>
        /// <param name="doinfo">操作详情</param>
        /// <param name="type">操作类型</param>
        /// <param name="staffid">操作人id</param>
        /// <param name="dt">时间</param>
        /// <returns>布尔值</returns>
        bool writedolog(string doinfo, string type, long staffid, DateTime dt);

        /// <summary>
        /// 写入用户权限操作日志
        /// </summary>
        /// <param name="pl">用户权限表实体</param>
        /// <returns></returns>
        bool writeuserpowerdolog(oa_userpower_powerlogs pl);
    }
}
