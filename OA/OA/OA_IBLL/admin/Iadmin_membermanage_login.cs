using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_IBLL.admin
{
    /// <summary>
    /// 登陆接口
    /// </summary>
    public interface Iadmin_membermanage_login
    {
        #region 登陆
        /// <summary>
        /// 登陆（用户名、邮箱、手机三合一）
        /// </summary>
        /// <param name="lg">登陆实体</param>
        /// <returns>员工表实体</returns>
        oa_member_staffs loginusername(logintype lg);

        /// <summary>
        /// 登陆（有口令，用户名、邮箱、手机三合一）
        /// </summary>
        /// <param name="lg">登陆实体</param>
        /// <returns>员工表实体</returns>
        oa_member_staffs loginusernameuserword(logintype lg);

        /// <summary>
        /// 写入登陆日志
        /// </summary>
        /// <param name="sf">员工表实体</param>
        /// <returns>布尔值</returns>
        returnresult logincookie(oa_member_staffs sf);

        /// <summary>
        /// 更新登陆时间
        /// </summary>
        /// <param name="staffid"></param>
        void refreshlogintime(long staffid);
        
        #endregion

        #region 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="rg">员工表实体</param>
        /// <returns>结果实体</returns>
        returnresult register(oa_member_staffs sf);
        #endregion
    }
}
