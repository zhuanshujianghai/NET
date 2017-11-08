using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_BLL.admin;
namespace OA_Web.Controllers.admin
{
    public class Admin_PowerController : Admin_BaseController
    {
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_Login.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            return Content(contentstr);
        }

        /// <summary>
        /// 后台菜单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult PageMenuList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_PageMenuList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPageMenu()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_EditPageMenu.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long pagemenuid = Request.QueryString["id"].ToLong();
            contentstr = contentstr.Replace("$pagemenuid$",pagemenuid+"");
            admin_powermanage_power pp = new admin_powermanage_power();
            var pm = pp.QueryPageMenuById(pagemenuid);
            //新增
            if (pm == null)
            {
                contentstr = contentstr.Replace("$name$", "");
                contentstr = contentstr.Replace("$url$", "");
                contentstr = contentstr.Replace("$parentid$", "");
                contentstr = contentstr.Replace("$remark$", "");
                contentstr = contentstr.Replace("$ispage$", "");
            }
            else//修改
            {
                contentstr = contentstr.Replace("$name$", pm.name);
                contentstr = contentstr.Replace("$url$", pm.url);
                contentstr = contentstr.Replace("$parentid$", pm.parentid+"");
                contentstr = contentstr.Replace("$remark$", pm.remark);
                contentstr = contentstr.Replace("$ispage$", pm.ispage+"");
            }
            return Content(contentstr);
        }
        /// <summary>
        /// 权限日志
        /// </summary>
        /// <returns></returns>
        public ActionResult PowerLogList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_PowerLogList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <returns></returns>
        public ActionResult DoLogList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_DoLogList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <returns></returns>
        public ActionResult AuserList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_AuserList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑管理员
        /// </summary>
        /// <returns></returns>
        public ActionResult EditAuser()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_EditAuser.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            long id = Request.QueryString["id"].ToLong();
            contentstr = contentstr.Replace("$id$", id+"");
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var au = pa.QueryViewAuserById(id);
            //新增
            if (au == null)
            {
                contentstr = contentstr.Replace("$username$", "");
                contentstr = contentstr.Replace("$realname$", "");
                contentstr = contentstr.Replace("$roleid$", "0");
                contentstr = contentstr.Replace("$qq$", "");
                contentstr = contentstr.Replace("$email$", "");
                contentstr = contentstr.Replace("$phone$", "");
                contentstr = contentstr.Replace("$password$","");
                contentstr = contentstr.Replace("$userword$","");
            }
            else
            {
                contentstr = contentstr.Replace("$username$", au.username);
                contentstr = contentstr.Replace("$realname$", au.realname);
                contentstr = contentstr.Replace("$roleid$", au.roleid+"");
                contentstr = contentstr.Replace("$qq$", au.qq);
                contentstr = contentstr.Replace("$email$", au.email);
                contentstr = contentstr.Replace("$phone$", au.phone);
                contentstr = contentstr.Replace("$password$", "如不修改密码则不填");
                contentstr = contentstr.Replace("$userword$", "如不修改口令则不填");
            }
            return Content(contentstr);
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_RoleList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }
        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <returns></returns>
        public ActionResult EditRole()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_EditRole.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            long id = Request.QueryString["id"].ToLong();
            contentstr = contentstr.Replace("$id$",id+"");
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var rl = pa.QueryRoleById(id);
            //新增
            if (rl == null)
            {
                contentstr = contentstr.Replace("$rolename$", "");
                contentstr = contentstr.Replace("$roleinfo$", "");
            }
            else
            {
                contentstr = contentstr.Replace("$rolename$", rl.rolename);
                contentstr = contentstr.Replace("$roleinfo$", rl.roleinfo);
            }
            return Content(contentstr);
        }
        /// <summary>
        /// 顶级功能列表
        /// </summary>
        /// <returns></returns>
        public ActionResult TopFunctionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_TopFunctionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public ActionResult FunctionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_FunctionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            long pid = Request.QueryString["pid"].ToLong();
            contentstr = contentstr.Replace("$pid$",pid+"");
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑功能
        /// </summary>
        /// <returns></returns>
        public ActionResult EditFunction()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_EditFunction.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            long pid = Request.QueryString["pid"].ToLong();
            contentstr = contentstr.Replace("$pid$",pid+"");
            long id = Request.QueryString["id"].ToLong();
            contentstr = contentstr.Replace("$id$",id+"");
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var fun = pa.QueryFunctionById(id);
            //新增
            if (fun == null)
            {
                contentstr = contentstr.Replace("$functionname$", "");
                contentstr = contentstr.Replace("$functioninfo$", "");
            }
            else
            {
                contentstr = contentstr.Replace("$functionname$", fun.functionname);
                contentstr = contentstr.Replace("$functioninfo$", fun.functioninfo);
            }
            return Content(contentstr);
        }

        /// <summary>
        /// 角色功能列表
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleFunctionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_RoleFunctionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            long roleid = Request.QueryString["roleid"].ToLong();
            contentstr = contentstr.Replace("$roleid$",roleid+"");
            return Content(contentstr);
        }

        /// <summary>
        /// ACTION列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ActionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_ActionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑ACTION
        /// </summary>
        /// <returns></returns>
        public ActionResult EditAction()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_EditAction.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            long id = Request.QueryString["id"].ToLong();
            contentstr = contentstr.Replace("$id$",id+"");
            admin_powermanage_auser pa = new admin_powermanage_auser();
            var at = pa.QueryActionById(id);
            //新增
            if (at == null)
            {
                contentstr = contentstr.Replace("$actionname$", "");
                contentstr = contentstr.Replace("$actioninfo$", "");
            }
            else
            {
                contentstr = contentstr.Replace("$actionname$", at.actionname);
                contentstr = contentstr.Replace("$actioninfo$", at.actioninfo);
            }
            return Content(contentstr);
        }
        /// <summary>
        /// 功能ACTION列表
        /// </summary>
        /// <returns></returns>
        public ActionResult FunctionActionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Power_FunctionActionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            long fid = Request.QueryString["fid"].ToLong();
            contentstr = contentstr.Replace("$fid$",fid+"");
            return Content(contentstr);
        }
	}
}