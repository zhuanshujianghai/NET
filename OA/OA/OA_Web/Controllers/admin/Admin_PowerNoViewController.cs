using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_BLL.admin;
using OA_Common;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity.Core.Objects;

namespace OA_Web.Controllers.admin
{
    public class Admin_PowerNoViewController : Admin_BaseController
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            returnresult rr = new returnresult();
            //用户名
            string username = Request.Form["username"];
            //密码
            string password = Request.Form["password"];
            //口令
            string userword = Request.Form["userword"];
            //验证码
            string captcha = Request.Form["captcha"];
            if (CheckVerificationCode(captcha))
            {
                admin_powermanage_auser pa = new admin_powermanage_auser();
                oa_power_ausers au = pa.loginauser(username,password,userword);
                if (au == null)
                {
                    rr.status = false;
                    rr.msg = "账户名和密码和口令不匹配！";
                }
                else
                {
                    rr = pa.logincookie(au);
                    if (rr.status)
                    {
                        HttpCookie hc = new HttpCookie("adminlogincookie");
                        hc.Value = rr.msg;
                        hc.Expires = DateTime.Now.AddDays(30);
                        Response.AppendCookie(hc);

                        rr.status = true;
                        rr.msg = "登陆成功！";
                    }
                }
            }
            else
            {
                rr.status = false;
                rr.msg = "验证码有误！";
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 检测验证码是否正确
        /// </summary>
        /// <param name="code">用户输入</param>
        /// <returns>布尔值</returns>
        public bool CheckVerificationCode(string captcha)
        {
            if (TempData["captcha"] == null || TempData["captcha"].ToString() != captcha.ToLower())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region 菜单

        /// <summary>
        /// 后台菜单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryPageMenuList(DataTableParameter param)
        {
            admin_powermanage_power pp = new admin_powermanage_power();
            var lstdt = pp.QueryPageMenuList();
            int total = lstdt.Count;
            lstdt = lstdt.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstdt
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 后台菜单列表-下拉列表（顶级菜单，本系统只有两级菜单）
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryPageMenuList_Select()
        {
            var me = getauser();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_powermanage_power pp = new admin_powermanage_power();
            var lstpm = pp.QueryPageMenuList().Where(a => a.parentid == 0).ToList();
            return Json(lstpm, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPageMenu()
        {
            returnresult rr = new returnresult();
            string name = Request.Form["name"];
            string url = Request.Form["url"];
            string remark = Request.Form["remark"];
            long parentid = Request.Form["parentid"].ToLong();
            int ispage = Request.Form["ispage"].ToInt();
            long pagemenuid = Request.Form["pagemenuid"].ToLong();
            admin_powermanage_power pp = new admin_powermanage_power();
            var pm = pp.QueryPageMenuById(pagemenuid);
            if (pm == null)
            {
                pm = new oa_power_pagemenu();
                pm.image = "";
                pm.ispage = ispage;
                pm.name = name;
                pm.parentid = parentid;
                pm.remark = remark;
                pm.status = 0;
                pm.url = url;
                rr = pp.AddPageMenu(pm);
            }
            else
            {
                pm.name = name;
                pm.url = url;
                pm.remark = remark;
                pm.parentid = parentid;
                pm.ispage = ispage;
                rr = pp.UpdatePageMenu(pm);
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult DeletePageMenu()
        {
            returnresult rr = new returnresult();
            long pagemenuid = Request.Form["id"].ToLong();
            admin_powermanage_power pp = new admin_powermanage_power();
            var pm = pp.QueryPageMenuById(pagemenuid);
            if (pm == null)
            {
                rr.status = false;
                rr.msg = "未找到该菜单";
            }
            else
            {
                rr.status = pp.DeletePageMenu(pm);
                if (rr.status)
                {
                    rr.msg = "删除成功";
                }
                else
                {
                    rr.msg = "删除失败";
                }
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region 权限日志、操作日志
        /// <summary>
        /// 权限日志
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryPowerLogList(DataTableParameter param)
        {
            string username = Request.QueryString["username"];
            int type = Request.QueryString["type"].ToInt();
            string begintimestr = Request["begintime"];
            string endtimestr = Request["endtime"];
            if (begintimestr != null && begintimestr != "")
            {
                begintimestr = begintimestr + " 23:59:59.999";
            }
            if (endtimestr != null && endtimestr != "")
            {
                endtimestr = endtimestr + " 23:59:59.999";
            }
            else
            {
                endtimestr = DateTime.Now.Date.ToString("yyyy/MM/dd") + " 23:59:59.999";
            }

            admin_powermanage_power pp = new admin_powermanage_power();
            var lstdt = pp.QueryPowerLogList();

            if (type!=0)
            {
                lstdt = lstdt.Where(a => a.type==type).ToList();
            }
            if (username != null)
            {
                lstdt = lstdt.Where(a => a.username.ToLower().Contains(username)).ToList();
            }
            if (begintimestr != null)
            {
                lstdt = lstdt.Where(a => a.addtime > begintimestr.ToDatetime().AddDays(-1) && a.addtime <= endtimestr.ToDatetime()).ToList();
            }

            int total = lstdt.Count;
            lstdt = lstdt.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstdt
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryDoLogList(DataTableParameter param)
        {
            string username = Request.QueryString["username"];
            string type = Request.QueryString["type"];
            string begintimestr = Request["begintime"];
            string endtimestr = Request["endtime"];
            if (begintimestr != null && begintimestr != "")
            {
                begintimestr = begintimestr + " 23:59:59.999";
            }
            if (endtimestr != null && endtimestr != "")
            {
                endtimestr = endtimestr + " 23:59:59.999";
            }
            else
            {
                endtimestr = DateTime.Now.Date.ToString("yyyy/MM/dd") + " 23:59:59.999";
            }
            admin_powermanage_power pp = new admin_powermanage_power();
            var lstdt = pp.QueryDoLogList();

            if (type != null)
            {
                lstdt = lstdt.Where(a => a.type.ToLower().Contains(type)).ToList();
            }
            if (username != null)
            {
                lstdt = lstdt.Where(a => a.username.ToLower().Contains(username)).ToList();
            }
            if (begintimestr != null)
            {
                lstdt = lstdt.Where(a => a.addtime > begintimestr.ToDatetime().AddDays(-1) && a.addtime <= endtimestr.ToDatetime()).ToList();
            }

            int total = lstdt.Count;
            lstdt = lstdt.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstdt
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 管理员、角色、功能、ACTION
        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryAuserList(DataTableParameter param)
        {
            string username = Request.QueryString["username"];
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var lstau = pa.QueryAuserList();
            if (username != null)
            {
                lstau = lstau.Where(a => a.username.ToLower().Contains(username)).ToList();
            }
            int total = lstau.Count;
            lstau = lstau.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstau
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑管理员
        /// </summary>
        /// <returns></returns>
        public ActionResult EditAuser()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            string username = Request.Form["username"];
            string realname = Request.Form["realname"];
            long roleid = Request.Form["roleid"].ToLong();
            string qq = Request.Form["qq"];
            string email = Request.Form["email"];
            string phone = Request.Form["phone"];
            string password = Request.Form["password"];
            string userword = Request.Form["userword"];
            int result = 0;
            string msg = "";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@id",id),
                new SqlParameter("@roleid",roleid),
                new SqlParameter("@username",username),
                new SqlParameter("@password",password),
                new SqlParameter("@userword",userword),
                new SqlParameter("@realname",realname),
                new SqlParameter("@qq",qq),
                new SqlParameter("@email",email),
                new SqlParameter("@phone",phone),
                new SqlParameter("@result",result),
                new SqlParameter("@msg",msg)
            };
            para[9].Direction = ParameterDirection.InputOutput;
            para[10].Direction = ParameterDirection.InputOutput;
            para[10].Size = 20;
            admin_powermanage_auser pa = new admin_powermanage_auser();
            string word = "@id,@roleid,@username,@password,@userword,@realname,@qq,@email,@phone,@result output,@msg output";
            int re = pa.execproc("proc_updateauser",word,para);
            result = para[9].Value.ToInt();
            if (re == -1024)
            {
                rr.status = false;
                rr.msg = "操作失败";
            }
            else
            {
                //新增
                if (id == 0)
                {
                    if (result == 0)
                    {
                        rr.status = false;
                        
                    }
                    else
                    {
                        rr.status = true;
                    }
                }
                else
                {
                    if (result == 0)
                    {
                        rr.status = false;
                    }
                    else
                    {
                        rr.status = true;
                    }
                }
                rr.msg = msg;
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteAuser()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var au = pa.QueryAuserById(id);
            if (au == null)
            {
                rr.status = false;
                rr.msg = "未找到该管理员";
            }
            else
            {
                rr.status = pa.DeleteAuser(au);
                if (rr.status)
                {
                    rr.msg = "删除成功";
                }
                else
                {
                    rr.msg = "删除失败";
                }
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryRoleList(DataTableParameter param)
        {
            string rolename = Request.QueryString["rolename"];
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var lstrl = pa.QueryRoleList();
            if (rolename != null)
            {
                lstrl = lstrl.Where(a => a.rolename.ToLower().Contains(rolename)).ToList();
            }
            int total = lstrl.Count;
            lstrl = lstrl.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstrl
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryRoleList_Select()
        {
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var lstrl = pa.QueryRoleList();
            int total = lstrl.Count;
            return Json(lstrl, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <returns></returns>
        public ActionResult EditRole()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            string rolename = Request.Form["rolename"];
            string roleinfo = Request.Form["roleinfo"];
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var rl = pa.QueryRoleById(id);
            //新增
            if (rl == null)
            {
                rl = new oa_power_roles();
                rl.rolename = rolename;
                rl.roleinfo = roleinfo;
                rr  = pa.AddRole(rl);
            }
            else
            {
                rl.rolename = rolename;
                rl.roleinfo = roleinfo;
                rr = pa.UpdateRole(rl);
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteRole()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var rl = pa.QueryRoleById(id);
            if (rl == null)
            {
                rr.status = false;
                rr.msg = "未找到该角色";
            }
            else
            {
                rr = pa.DeleteRole(rl);
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 顶级功能列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryTopFunctionList(DataTableParameter param)
        {
            string functionname = Request.QueryString["functionname"];
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var lstfun = pa.QueryFunctionList().Where(a=>a.parentid==0).ToList();
            if (functionname != null)
            {
                lstfun = lstfun.Where(a => a.functionname.ToLower().Contains(functionname)).ToList();
            }
            int total = lstfun.Count;
            lstfun = lstfun.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstfun
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryFunctionList(DataTableParameter param)
        {
            string functionname = Request.QueryString["functionname"];
            long pid = Request.QueryString["pid"].ToLong();
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var lstfun = pa.QueryFunctionList().Where(a=>a.parentid==pid).ToList();
            if (functionname != null)
            {
                lstfun = lstfun.Where(a => a.functionname.ToLower().Contains(functionname)).ToList();
            }
            int total = lstfun.Count;
            lstfun = lstfun.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstfun
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑功能
        /// </summary>
        /// <returns></returns>
        public ActionResult EditFunction()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            string functionname = Request.Form["functionname"];
            string functioninfo = Request.Form["functioninfo"];
            long pid = Request.Form["pid"].ToLong();
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var fun = pa.QueryFunctionById(id);
            //新增
            if (fun == null)
            {
                fun = new oa_power_functions();
                fun.functionname = functionname;
                fun.functioninfo = functioninfo;
                fun.parentid = pid;
                rr = pa.AddFunction(fun);
            }
            else
            {
                fun.functionname = functionname;
                fun.functioninfo = functioninfo;
                fun.parentid = pid;
                rr = pa.UpdateFunction(fun);
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除功能
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteFunction()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var fun = pa.QueryFunctionById(id);
            if (fun == null)
            {
                rr.status = false;
                rr.msg = "未找到该角色";
            }
            else
            {
                rr.status = pa.DeleteFunction(fun);
                if (rr.status)
                {
                    rr.msg = "删除成功";
                }
                else
                {
                    rr.msg = "删除失败";
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 角色功能列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryRoleFunctionList()
        {
            long roleid = Request.Form["roleid"].ToLong();
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var lstrf = pa.QueryViewRoleFunctionListByRoleid(roleid);
            return Json(new
            {
                bbData = lstrf.Where(a=>a.parentid!=0).ToList(),
                aaData = lstrf.Where(a=>a.parentid==0).ToList()
            }, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑角色功能
        /// </summary>
        /// <returns></returns>
        public ActionResult EditRoleFunction()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long roleid = Request.Form["roleid"].ToLong();
            admin_powermanage_auser pa = new admin_powermanage_auser();
            //全选和反选
            if (roleid > 0)
            {
                var lstrf = pa.QueryRoleFunctionListByRoleid(roleid);
                var temp = lstrf.Where(a => a.status == 0).ToList().Count > 0 ? 1 : 0;
                foreach (var item in lstrf)
                {
                    item.status = temp;
                }
                rr.status = pa.UpdateRoleFunctionList(lstrf);
                if (rr.status)
                {
                    rr.msg = "修改成功";
                }
                else
                {
                    rr.msg = "修改失败";
                }
            }
            else
            {
                var rf = pa.QueryRoleFunctionById(id);
                if (rf == null)
                {
                    rr.status = false;
                    rr.msg = "未找到该选项";
                }
                else
                {
                    var fun = pa.QueryFunctionById(rf.functionid);
                    //勾选顶级功能（则修改其所属功能）
                    if (fun.parentid == 0)
                    {
                        List<oa_power_rolesfunctions> lstrf = new List<oa_power_rolesfunctions>();
                        var lstfun = pa.QueryFunctionByPid(id);
                        lstfun.Add(fun);
                        foreach (var item in lstfun)
                        {
                            rf = pa.QueryRoleFunctionByRoleIdFunctionId(rf.roleid,item.id);
                            lstrf.Add(rf);
                        }
                        var temp = lstrf.Where(a => a.status == 0).ToList().Count > 0 ? 1 : 0;
                        foreach (var item in lstrf)
                        {
                            item.status = temp;
                        }
                        rr.status = pa.UpdateRoleFunctionList(lstrf);
                        if (rr.status)
                        {
                            rr.msg = "修改成功";
                        }
                        else
                        {
                            rr.msg = "修改失败";
                        }
                    }
                    else//修改单个
                    {
                        List<oa_power_rolesfunctions> lstrf = new List<oa_power_rolesfunctions>();
                        rf.status = rf.status == 0 ? 1 : 0;
                        lstrf.Add(rf);
                        if (rf.status == 0)
                        {
                            //获取父级功能，并修改其选中状态
                            var temprf = pa.QueryRoleFunctionByRoleIdFunctionId(rf.roleid, fun.parentid);
                            temprf.status = rf.status;
                            lstrf.Add(temprf);
                        }
                        else
                        {
                            List<oa_power_rolesfunctions> lstrf1 = new List<oa_power_rolesfunctions>();
                            var lstfun = pa.QueryFunctionByPid(fun.parentid);
                            foreach (var item in lstfun)
                            {
                                rf = pa.QueryRoleFunctionByRoleIdFunctionId(rf.roleid, item.id);
                                lstrf1.Add(rf);
                            }
                            var temp = lstrf1.Where(a => a.status == 0 && a.id!=rf.id).ToList().Count > 0 ? 0 : 1;
                            //获取父级功能，并修改其选中状态
                            var temprf = pa.QueryRoleFunctionByRoleIdFunctionId(rf.roleid, fun.parentid);
                            temprf.status = temp;
                            lstrf.Add(temprf);
                        }
                        rr.status = pa.UpdateRoleFunctionList(lstrf);
                        if (rr.status)
                        {
                            rr.msg = "修改成功";
                        }
                        else
                        {
                            rr.msg = "修改失败";
                        }
                    }
                }
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// ACTION列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryActionList(DataTableParameter param)
        {
            string actionname = Request.QueryString["actionname"];
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var lstat = pa.QueryActionList();
            if (actionname != null)
            {
                lstat = lstat.Where(a => a.actionname.ToLower().Contains(actionname)).ToList();
            }
            int total = lstat.Count;
            lstat = lstat.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstat
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑action
        /// </summary>
        /// <returns></returns>
        public ActionResult EditAction()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            string actionname = Request.Form["actionname"];
            string actioninfo = Request.Form["actioninfo"];
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var at = pa.QueryActionById(id);
            //新增
            if (at == null)
            {
                at = new oa_power_actions();
                at.actionname = actionname;
                at.actioninfo = actioninfo;
                rr = pa.AddAction(at);
            }
            else
            {
                at.actioninfo = actioninfo;
                at.actionname = actionname;
                rr = pa.UpdateAction(at);
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 重新读取程序集中的ACTION
        /// </summary>
        /// <returns></returns>
        public ActionResult ReadAllAction()
        {
            returnresult rr = new returnresult();
            admin_powermanage_auser pa = new admin_powermanage_auser();
            List<oa_power_actions> lsta = new List<oa_power_actions>();
            int count = 0;
            //获取数据库action
            var action = pa.QueryActionList();
            //获取当前程序集的所有action
            List<string> lststr = Common.GetALLPageByReflection();
            //如果数据库的action条数为0，则进行第一次添加，直接添加全部，如果不是第一次添加，则添加两者之间的差距
            if (action.Count == 0)
            {
                foreach (var item in lststr)
                {
                    oa_power_actions ac = new oa_power_actions();
                    ac.actionname = item;
                    ac.actioninfo = "";
                    lsta.Add(ac);
                }
                rr = pa.AddActionList(lsta);
                count = rr.status ? lsta.Count : 0;
            }
            else
            {
                //不是第一次添加，先判断两个结合的数量是否不同
                if (action.Count != lststr.Count)
                {
                    //先求两个集合的差集
                    List<string> lstacstr = new List<string>();
                    List<oa_power_actions> lstac = pa.QueryActionList();
                    foreach (var item in lstac)
                    {
                        lstacstr.Add(item.actionname);
                    }
                    List<string> lstaction = lststr.Except(lstacstr).ToList();

                    //将差集添加进数据库
                    foreach (var item in lstaction)
                    {
                        oa_power_actions ac = new oa_power_actions();
                        ac.actionname = item;
                        ac.actioninfo = "";
                        lsta.Add(ac);
                    }
                    rr = pa.AddActionList(lsta);
                    count = rr.status ? lstaction.Count : 0;
                }
                else
                {
                    count = 0;
                }
            }
            try
            {
                return Json(count,JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(0, JsonRequestBehavior.DenyGet);
            }
        }

        /// <summary>
        /// 功能ACTION列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryFunctionActionList(DataTableParameter param)
        {
            string actionname = Request.QueryString["actionname"];
            long fid = Request.QueryString["fid"].ToLong();
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var lstat = pa.QueryViewFunctionActionList(fid);
            if (actionname != null)
            {
                lstat = lstat.Where(a => a.actionname.ToLower().Contains(actionname)).ToList();
            }
            int total = lstat.Count;
            lstat = lstat.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstat
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改功能action
        /// </summary>
        /// <returns></returns>
        public ActionResult EditFunctionAction()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var fa = pa.QueryFunctionActionById(id);
            if (fa == null)
            {
                rr.status = false;
                rr.msg = "未找到该功能action";
            }
            else
            {
                fa.status = fa.status == 0 ? 1 : 0;
                rr.status = pa.UpdateFunctionAction(fa);
                if (rr.status)
                {
                    rr.msg = "修改成功";
                }
                else
                {
                    rr.msg = "修改失败";
                }
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}