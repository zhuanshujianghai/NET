using OA_IBLL.admin;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.admin
{
    public class admin_powermanage_auser : BaseAdminService, Iadmin_powermanage_auser
    {
        /// <summary>
        /// 新增管理员
        /// </summary>
        /// <param name="au"></param>
        /// <returns></returns>
        public returnresult AddAuser(oa_power_ausers au)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_power_ausers>(a => a.username == au.username))
            {
                rr.status = false;
                rr.msg = "用户名已存在";
            }
            else
            {
                rr.status = Add<oa_power_ausers>(au);
                if (rr.status)
                {
                    rr.msg = "新增成功";
                }
                else
                {
                    rr.msg = "新增失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="au"></param>
        /// <returns></returns>
        public returnresult UpdateAuser(oa_power_ausers au)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_power_ausers>(a => a.username == au.username && a.id != au.id))
            {
                rr.status = false;
                rr.msg = "用户名已存在";
            }
            else
            {
                rr.status = Update<oa_power_ausers>(au);
                if (rr.status)
                {
                    rr.msg = "修改成功";
                }
                else
                {
                    rr.msg = "修改失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="au"></param>
        /// <returns></returns>
        public bool DeleteAuser(oa_power_ausers au)
        {
            au.status = -1;
            return Update<oa_power_ausers>(au);
        }

        /// <summary>
        /// 根据管理员id查询管理员信息
        /// </summary>
        /// <param name="id">管理员id</param>
        /// <returns></returns>
        public oa_power_ausers QueryAuserById(long id)
        {
            return Query<oa_power_ausers>(a => a.id == id);
        }

        /// <summary>
        /// 根据管理员id查询管理员视图信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public view_oa_power_auserlist QueryViewAuserById(long id)
        {
            return Query<view_oa_power_auserlist>(a => a.id == id);
        }

        /// <summary>
        /// 根据用户名查询管理员信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public oa_power_ausers QueryAuserByUsername(string username)
        {
            return Query<oa_power_ausers>(a => a.username == username);
        }

        /// <summary>
        /// 查询管理员列表
        /// </summary>
        /// <returns></returns>
        public List<view_oa_power_auserlist> QueryAuserList()
        {
            return QueryList<view_oa_power_auserlist>(a => a.status != -1).OrderByDescending(a => a.id).ToList();
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="rl"></param>
        /// <returns></returns>
        public returnresult AddRole(oa_power_roles rl)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_power_roles>(a => a.rolename == rl.rolename))
            {
                rr.status = false;
                rr.msg = "该角色名称已存在";
            }
            else
            {
                rr.status = Add<oa_power_roles>(rl);
                if (rr.status)
                {
                    rr.msg = "新增角色成功";
                    List<oa_power_rolesfunctions> lstrf = new List<oa_power_rolesfunctions>();
                    var lstfu = QueryList<oa_power_functions>(a => a.id > 0);
                    foreach (var item in lstfu)
                    {
                        oa_power_rolesfunctions rf = new oa_power_rolesfunctions();
                        rf.functionid = item.id;
                        rf.roleid = rl.id;
                        rf.status = 0;
                        lstrf.Add(rf);
                    }
                    rr.status = AddIQueryable<oa_power_rolesfunctions>(lstrf);
                    if (rr.status)
                    {
                        rr.msg += ",新增角色功能成功";
                    }
                    else
                    {
                        rr.msg += ",新增角色功能失败";
                    }
                }
                else
                {
                    rr.msg = "新增角色失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="rl"></param>
        /// <returns></returns>
        public returnresult UpdateRole(oa_power_roles rl)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_power_roles>(a => a.rolename == rl.rolename && a.id != rl.id))
            {
                rr.status = false;
                rr.msg = "该角色名称已存在";
            }
            else
            {
                rr.status = Update<oa_power_roles>(rl);
                if (rr.status)
                {
                    rr.msg = "修改角色成功";
                }
                else
                {
                    rr.msg = "修改角色失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="rl"></param>
        /// <returns></returns>
        public returnresult DeleteRole(oa_power_roles rl)
        {
            returnresult rr = new returnresult();
            var au = Query<oa_power_ausers>(a => a.roleid == rl.id);
            if (au == null)
            {
                rr.status = Delete<oa_power_roles>(rl);
                if (rr.status)
                {
                    rr.msg = "删除成功";
                }
                else
                {
                    rr.msg = "删除失败";
                }
            }
            else
            {
                rr.status = false;
                rr.msg = "该角色仍在被使用，不能删除";
            }
            return rr;
        }

        /// <summary>
        /// 根据角色id查询角色信息
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns></returns>
        public oa_power_roles QueryRoleById(long id)
        {
            return Query<oa_power_roles>(a => a.id == id);
        }

        /// <summary>
        /// 查询角色列表
        /// </summary>
        /// <returns></returns>
        public List<oa_power_roles> QueryRoleList()
        {
            return QueryList<oa_power_roles>(a => a.id > 0).OrderByDescending(a => a.id).ToList();
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="ft"></param>
        /// <returns></returns>
        public returnresult AddFunction(oa_power_functions ft)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_power_functions>(a => a.functionname == ft.functionname))
            {
                rr.status = false;
                rr.msg = "该功能名称已存在";
            }
            else
            {
                rr.status = Add<oa_power_functions>(ft);
                if (rr.status)
                {
                    rr.msg = "新增功能成功";
                    /**************新增角色功能***************/
                    List<oa_power_rolesfunctions> lstrf = new List<oa_power_rolesfunctions>();
                    var lstrl = QueryList<oa_power_roles>(a => a.id > 0);
                    foreach (var item in lstrl)
                    {
                        oa_power_rolesfunctions rf = new oa_power_rolesfunctions();
                        rf.functionid = ft.id;
                        rf.roleid = item.id;
                        rf.status = 0;
                        lstrf.Add(rf);
                    }
                    rr.status = AddIQueryable<oa_power_rolesfunctions>(lstrf);
                    if (rr.status)
                    {
                        rr.msg += ",新增角色功能成功";
                    }
                    else
                    {
                        rr.msg += ",新增角色功能失败";
                    }
                    /**************新增角色功能***************/

                    /**************新增功能action***************/
                    List<oa_power_functionsactions> lstfa = new List<oa_power_functionsactions>();
                    var lstat = QueryList<oa_power_actions>(a => a.id > 0);
                    foreach (var item in lstat)
                    {
                        oa_power_functionsactions fa = new oa_power_functionsactions();
                        fa.functionid = ft.id;
                        fa.actionid = item.id;
                        fa.status = 0;
                        lstfa.Add(fa);
                    }
                    rr.status = AddIQueryable<oa_power_functionsactions>(lstfa);
                    if (rr.status)
                    {
                        rr.msg += ",新增功能ACTION成功";
                    }
                    else
                    {
                        rr.msg += ",新增功能ACTION失败";
                    }
                    /**************新增功能action***************/
                }
                else
                {
                    rr.msg = "新增功能失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 修改功能
        /// </summary>
        /// <param name="ft"></param>
        /// <returns></returns>
        public returnresult UpdateFunction(oa_power_functions ft)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_power_functions>(a => a.functionname == ft.functionname && a.id != ft.id))
            {
                rr.status = false;
                rr.msg = "该功能名称已存在";
            }
            else
            {
                rr.status = Update<oa_power_functions>(ft);
                if (rr.status)
                {
                    rr.msg = "修改功能成功";
                }
                else
                {
                    rr.msg = "修改功能失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="ft"></param>
        /// <returns></returns>
        public bool DeleteFunction(oa_power_functions ft)
        {
            var lstrf = QueryList<oa_power_rolesfunctions>(a => a.functionid == ft.id);
            if (lstrf.Count > 0)
            {
                bool bo = DeleteIQueryable<oa_power_rolesfunctions>(lstrf);
                if (bo)
                {
                    return Delete<oa_power_functions>(ft);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return Delete<oa_power_functions>(ft);
            }
        }

        /// <summary>
        /// 根据id查询功能信息
        /// </summary>
        /// <param name="id">功能id</param>
        /// <returns></returns>
        public oa_power_functions QueryFunctionById(long id)
        {
            return Query<oa_power_functions>(a => a.id == id);
        }

        /// <summary>
        /// 根据父级id查询功能信息列表
        /// </summary>
        /// <param name="pid">父级id</param>
        /// <returns></returns>
        public List<oa_power_functions> QueryFunctionByPid(long pid)
        {
            return QueryList<oa_power_functions>(a => a.parentid == pid);
        }

        /// <summary>
        /// 查询功能列表
        /// </summary>
        /// <returns></returns>
        public List<oa_power_functions> QueryFunctionList()
        {
            return QueryList<oa_power_functions>(a => a.id > 0).OrderByDescending(a => a.id).ToList();
        }

        /// <summary>
        /// 修改角色功能
        /// </summary>
        /// <param name="rf"></param>
        /// <returns></returns>
        public bool UpdateRoleFunction(oa_power_rolesfunctions rf)
        {
            return Update<oa_power_rolesfunctions>(rf);
        }

        /// <summary>
        /// 批量修改角色功能
        /// </summary>
        /// <param name="lstrf"></param>
        /// <returns></returns>
        public bool UpdateRoleFunctionList(List<oa_power_rolesfunctions> lstrf)
        {
            return UpdateIQueryable<oa_power_rolesfunctions>(lstrf);
        }

        /// <summary>
        /// 根据id查询角色功能信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public oa_power_rolesfunctions QueryRoleFunctionById(long id)
        {
            return Query<oa_power_rolesfunctions>(a => a.id == id);
        }

        /// <summary>
        /// 根据角色id和功能id查询角色功能信息
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        public oa_power_rolesfunctions QueryRoleFunctionByRoleIdFunctionId(long roleid, long functionid)
        {
            return Query<oa_power_rolesfunctions>(a => a.roleid == roleid && a.functionid == functionid);
        }

        /// <summary>
        /// 根据角色id查询角色功能视图列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <returns></returns>
        public List<view_oa_power_rolefunctionlist> QueryViewRoleFunctionListByRoleid(long roleid)
        {
            return QueryList<view_oa_power_rolefunctionlist>(a => a.roleid == roleid);
        }

        /// <summary>
        /// 根据角色id查询角色功能列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <returns></returns>
        public List<oa_power_rolesfunctions> QueryRoleFunctionListByRoleid(long roleid)
        {
            return QueryList<oa_power_rolesfunctions>(a => a.roleid == roleid);
        }

        /// <summary>
        /// 新增action
        /// </summary>
        /// <param name="at"></param>
        /// <returns></returns>
        public returnresult AddAction(oa_power_actions at)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_power_actions>(a => a.actionname == at.actionname))
            {
                rr.status = false;
                rr.msg = "该ACTION名称已存在";
            }
            else
            {
                rr.status = Add<oa_power_actions>(at);
                if (rr.status)
                {
                    rr.msg = "新增ACTION成功";
                    List<oa_power_functionsactions> lstfa = new List<oa_power_functionsactions>();
                    var lstfu = QueryList<oa_power_functions>(a => a.id > 0);
                    foreach (var item in lstfu)
                    {
                        oa_power_functionsactions fa = new oa_power_functionsactions();
                        fa.functionid = item.id;
                        fa.actionid = at.id;
                        fa.status = 0;
                        lstfa.Add(fa);
                    }
                    rr.status = AddIQueryable<oa_power_functionsactions>(lstfa);
                    if (rr.status)
                    {
                        rr.msg += ",新增功能ACTION成功";
                    }
                    else
                    {
                        rr.msg += ",新增角色ACTION失败";
                    }
                }
                else
                {
                    rr.msg = "新增ACTION失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 批量新增action
        /// </summary>
        /// <param name="lstat"></param>
        /// <returns></returns>
        public returnresult AddActionList(List<oa_power_actions> lstat)
        {
            returnresult rr = new returnresult();
            rr.status = AddIQueryable<oa_power_actions>(lstat);
            if (rr.status)
            {
                List<oa_power_functionsactions> lstfa = new List<oa_power_functionsactions>();
                rr.msg = "批量新增ACTION成功";
                foreach (var at in lstat)
                {
                    var lstfu = QueryList<oa_power_functions>(a => a.id > 0);
                    foreach (var item in lstfu)
                    {
                        oa_power_functionsactions fa = new oa_power_functionsactions();
                        fa.functionid = item.id;
                        fa.actionid = at.id;
                        fa.status = 0;
                        lstfa.Add(fa);
                    }
                }
                rr.status = AddIQueryable<oa_power_functionsactions>(lstfa);
                if (rr.status)
                {
                    rr.msg += ",新增功能ACTION成功";
                }
                else
                {
                    rr.msg += ",新增角色ACTION失败";
                }
            }
            else
            {
                rr.msg = "批量新增ACTION失败";
            }
            return rr;
        }

        /// <summary>
        /// 修改action
        /// </summary>
        /// <param name="at"></param>
        /// <returns></returns>
        public returnresult UpdateAction(oa_power_actions at)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_power_actions>(a => a.actionname == at.actionname && a.id != at.id))
            {
                rr.status = false;
                rr.msg = "该ACTION名称已存在";
            }
            else
            {
                rr.status = Update<oa_power_actions>(at);
                if (rr.status)
                {
                    rr.msg = "修改ACTION成功";
                }
                else
                {
                    rr.msg = "修改ACTION失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 删除action
        /// </summary>
        /// <param name="at"></param>
        /// <returns></returns>
        public bool DeleteAction(oa_power_actions at)
        {
            return Delete<oa_power_actions>(at);
        }

        /// <summary>
        /// 根据id查询action信息
        /// </summary>
        /// <param name="id">actiondi</param>
        /// <returns></returns>
        public oa_power_actions QueryActionById(long id)
        {
            return Query<oa_power_actions>(a => a.id == id);
        }

        /// <summary>
        /// 查询action列表
        /// </summary>
        /// <returns></returns>
        public List<oa_power_actions> QueryActionList()
        {
            return QueryList<oa_power_actions>(a => a.id > 0).OrderByDescending(a => a.id).ToList();
        }

        /// <summary>
        /// 修改功能action
        /// </summary>
        /// <param name="fa"></param>
        /// <returns></returns>
        public bool UpdateFunctionAction(oa_power_functionsactions fa)
        {
            return Update<oa_power_functionsactions>(fa);
        }

        /// <summary>
        /// 根据id查询功能action信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public oa_power_functionsactions QueryFunctionActionById(long id)
        {
            return Query<oa_power_functionsactions>(a => a.id == id);
        }

        /// <summary>
        /// 查询功能action视图列表
        /// </summary>
        /// <param name="functionid"></param>
        /// <returns></returns>
        public List<view_oa_power_functionactionlist> QueryViewFunctionActionList(long functionid)
        {
            return QueryList<view_oa_power_functionactionlist>(a => a.functionid == functionid);
        }

        #region 后台登陆

        /// <summary>
        /// 管理员登陆
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="userword">口令</param>
        /// <returns></returns>
        public oa_power_ausers loginauser(string username, string password, string userword)
        {
            return Query<oa_power_ausers>(a => a.username == username && a.password == password && a.userword == userword);
        }

        /// <summary>
        /// 登陆存入cookie
        /// </summary>
        /// <param name="sf">员工表实体</param>
        /// <returns>布尔值</returns>
        public returnresult logincookie(oa_power_ausers au)
        {
            DateTime dt = DateTime.Now;
            returnresult rr = new returnresult();
            string key = "myadminkey";
            string cookie = MD5_32(au.username + key + DateTime.Now.ToString("yyyyMMDDHHmmss"));
            oa_power_auserslogins al = new oa_power_auserslogins();
            al.addtime = dt;
            al.addip = getip();
            al.auserid = au.id;
            al.cookie = cookie;
            al.dotime = dt;
            al.expiretime = 30;
            if (Exist<oa_power_auserslogins>(a => a.auserid == au.id))
            {
                al = Query<oa_power_auserslogins>(a => a.auserid == au.id);
                al.addtime = dt;
                al.dotime = dt;
                al.cookie = cookie;
                rr.status = Update<oa_power_auserslogins>(al);
            }
            else
            {
                rr.status = Add<oa_power_auserslogins>(al);
            }
            if (rr.status)
            {
                //写入操作日志
                rr.status = writedolog("进行登陆", "login", au.id, dt);
                if (rr.status)
                {
                    rr.msg = cookie;
                }
                else
                {
                    rr.msg = "写入操作日志失败";
                    al.cookie += "写入操作日志失败";
                    Update<oa_power_auserslogins>(al);
                }
            }
            else
            {
                rr.msg = "COOKIE写入失败";
            }
            return rr;
        }

        /// <summary>
        /// 根据cookie字符串检查用户登录情况
        /// </summary>
        /// <param name="cookie">cookie字符串</param>
        /// <returns>登录人id</returns>
        public long checkadminlogincookie(string cookie)
        {
            long result = 0;
            oa_power_auserslogins sl = Query<oa_power_auserslogins>(a => a.cookie == cookie);
            if (sl == null)
            {
                result = 0;
            }
            else
            {
                if (sl.dotime.AddMinutes(sl.expiretime) > DateTime.Now)
                {
                    result = sl.auserid;
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }
        /// <summary>
        /// 更新登陆时间
        /// </summary>
        /// <param name="auserid">管理员id</param>
        public void refreshadminlogintime(long auserid)
        {
            var sl = Query<oa_power_auserslogins>(a => a.auserid == auserid);
            sl.dotime = DateTime.Now;
            Update<oa_power_auserslogins>(sl);
        }
        #endregion
    }
}
