using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_Models;

namespace OA_IBLL.admin
{
    public interface Iadmin_powermanage_auser
    {
        /// <summary>
        /// 新增管理员
        /// </summary>
        /// <param name="au"></param>
        /// <returns></returns>
        returnresult AddAuser(oa_power_ausers au);

        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="au"></param>
        /// <returns></returns>
        returnresult UpdateAuser(oa_power_ausers au);

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="au"></param>
        /// <returns></returns>
        bool DeleteAuser(oa_power_ausers au);

        /// <summary>
        /// 根据管理员id查询管理员信息
        /// </summary>
        /// <param name="id">管理员id</param>
        /// <returns></returns>
        oa_power_ausers QueryAuserById(long id);

        /// <summary>
        /// 根据管理员id查询管理员视图信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        view_oa_power_auserlist QueryViewAuserById(long id);

        /// <summary>
        /// 根据用户名查询管理员信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        oa_power_ausers QueryAuserByUsername(string username);

        /// <summary>
        /// 查询管理员列表
        /// </summary>
        /// <returns></returns>
        List<view_oa_power_auserlist> QueryAuserList();

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="rl"></param>
        /// <returns></returns>
        returnresult AddRole(oa_power_roles rl);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="rl"></param>
        /// <returns></returns>
        returnresult UpdateRole(oa_power_roles rl);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="rl"></param>
        /// <returns></returns>
        returnresult DeleteRole(oa_power_roles rl);

        /// <summary>
        /// 根据角色id查询角色信息
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns></returns>
        oa_power_roles QueryRoleById(long id);

        /// <summary>
        /// 查询角色列表
        /// </summary>
        /// <returns></returns>
        List<oa_power_roles> QueryRoleList();

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="ft"></param>
        /// <returns></returns>
        returnresult AddFunction(oa_power_functions ft);

        /// <summary>
        /// 修改功能
        /// </summary>
        /// <param name="ft"></param>
        /// <returns></returns>
        returnresult UpdateFunction(oa_power_functions ft);

        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="ft"></param>
        /// <returns></returns>
        bool DeleteFunction(oa_power_functions ft);

        /// <summary>
        /// 根据id查询功能信息
        /// </summary>
        /// <param name="id">功能id</param>
        /// <returns></returns>
        oa_power_functions QueryFunctionById(long id);

        /// <summary>
        /// 根据父级id查询功能信息列表
        /// </summary>
        /// <param name="pid">父级id</param>
        /// <returns></returns>
        List<oa_power_functions> QueryFunctionByPid(long pid);

        /// <summary>
        /// 查询功能列表
        /// </summary>
        /// <returns></returns>
        List<oa_power_functions> QueryFunctionList();

        /// <summary>
        /// 修改角色功能
        /// </summary>
        /// <param name="rf"></param>
        /// <returns></returns>
        bool UpdateRoleFunction(oa_power_rolesfunctions rf);

        /// <summary>
        /// 批量修改角色功能
        /// </summary>
        /// <param name="lstrf"></param>
        /// <returns></returns>
        bool UpdateRoleFunctionList(List<oa_power_rolesfunctions> lstrf);

        /// <summary>
        /// 根据id查询角色功能信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        oa_power_rolesfunctions QueryRoleFunctionById(long id);

        /// <summary>
        /// 根据角色id和功能id查询角色功能信息
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        oa_power_rolesfunctions QueryRoleFunctionByRoleIdFunctionId(long roleid, long functionid);

        /// <summary>
        /// 根据角色id查询角色功能列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <returns></returns>
        List<view_oa_power_rolefunctionlist> QueryViewRoleFunctionListByRoleid(long roleid);

        /// <summary>
        /// 根据角色id查询角色功能列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <returns></returns>
        List<oa_power_rolesfunctions> QueryRoleFunctionListByRoleid(long roleid);

        /// <summary>
        /// 新增action
        /// </summary>
        /// <param name="at"></param>
        /// <returns></returns>
        returnresult AddAction(oa_power_actions at);

        /// <summary>
        /// 批量新增action
        /// </summary>
        /// <param name="lstat"></param>
        /// <returns></returns>
        returnresult AddActionList(List<oa_power_actions> lstat);

        /// <summary>
        /// 修改action
        /// </summary>
        /// <param name="at"></param>
        /// <returns></returns>
        returnresult UpdateAction(oa_power_actions at);

        /// <summary>
        /// 删除action
        /// </summary>
        /// <param name="at"></param>
        /// <returns></returns>
        bool DeleteAction(oa_power_actions at);

        /// <summary>
        /// 根据id查询action信息
        /// </summary>
        /// <param name="id">actionid</param>
        /// <returns></returns>
        oa_power_actions QueryActionById(long id);

        /// <summary>
        /// 查询action列表
        /// </summary>
        /// <returns></returns>
        List<oa_power_actions> QueryActionList();

        /// <summary>
        /// 修改功能action
        /// </summary>
        /// <param name="fa"></param>
        /// <returns></returns>
        bool UpdateFunctionAction(oa_power_functionsactions fa);

        /// <summary>
        /// 根据id查询功能action信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        oa_power_functionsactions QueryFunctionActionById(long id);

        /// <summary>
        /// 查询功能action视图列表
        /// </summary>
        /// <param name="functionid"></param>
        /// <returns></returns>
        List<view_oa_power_functionactionlist> QueryViewFunctionActionList(long functionid);

        #region 后台登陆
        /// <summary>
        /// 管理员登陆
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="userword">口令</param>
        /// <returns></returns>
        oa_power_ausers loginauser(string username, string password, string userword);

        /// <summary>
        /// 登陆存入cookie
        /// </summary>
        /// <param name="sf">员工表实体</param>
        /// <returns>布尔值</returns>
        returnresult logincookie(oa_power_ausers au);

        /// <summary>
        /// 根据cookie字符串检查用户登录情况
        /// </summary>
        /// <param name="cookie">cookie字符串</param>
        /// <returns>登录人id</returns>
        long checkadminlogincookie(string cookie);

        /// <summary>
        /// 更新登陆时间
        /// </summary>
        /// <param name="auserid">管理员id</param>
        void refreshadminlogintime(long auserid);
        #endregion
    }
}
