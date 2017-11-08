using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_BLL.admin;

namespace OA_Web.Controllers.admin
{
    public class Admin_UserPowerController : Admin_BaseController
    {
        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult PageMenuList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_UserPower_PageMenuList.html");
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
            var str = Server.MapPath("/Template/admin/Admin_UserPower_EditPageMenu.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 菜单信息
            long id = Request.QueryString["id"].ToLong();
            var me = getauser();
            admin_userpowermanage_pagemenu up = new admin_userpowermanage_pagemenu();
            var menu = up.QueryPagemenuById(id);
            contentstr = contentstr.Replace("$menuid$", id + "");
            if (menu == null)
            {
                contentstr = contentstr.Replace("$name$", "");
                contentstr = contentstr.Replace("$remark$", "");
                contentstr = contentstr.Replace("$url$","");
                contentstr = contentstr.Replace("$ispage$","0");
                contentstr = contentstr.Replace("$parentid$","0");
            }
            else
            {
                contentstr = contentstr.Replace("$name$", menu.name);
                contentstr = contentstr.Replace("$remark$", menu.remark);
                contentstr = contentstr.Replace("$url$", menu.url);
                contentstr = contentstr.Replace("$ispage$", menu.ispage+"");
                contentstr = contentstr.Replace("$parentid$", menu.parentid+"");
            }
            #endregion
            return Content(contentstr);
        }

        /// <summary>
        /// 用户权限日志列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserPowerLogList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_UserPower_UserPowerLogList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 顶级功能列表
        /// </summary>
        /// <returns></returns>
        public ActionResult TopFunctionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_UserPower_TopFunctionList.html");
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
            var str = Server.MapPath("/Template/admin/Admin_UserPower_FunctionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            long pid = Request.QueryString["pid"].ToLong();
            contentstr = contentstr.Replace("$pid$", pid + "");
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑职位的功能列表
        /// </summary>
        /// <returns></returns>
        public ActionResult EditFunctionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_UserPower_EditFunctionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["pid"].ToLong();
            contentstr = contentstr.Replace("$pid$", id + "");
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑功能
        /// </summary>
        /// <returns></returns>
        public ActionResult EditFunction()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_UserPower_EditFunction.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 功能信息
            long id = Request.QueryString["id"].ToLong();
            long pid = Request.QueryString["pid"].ToLong();
            admin_userpowermanage_function uf = new admin_userpowermanage_function();
            var fun = uf.QueryFunctionById(id);
            contentstr = contentstr.Replace("$id$", id + "");
            contentstr = contentstr.Replace("$pid$",pid+"");
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
            #endregion
            return Content(contentstr);
        }

        /// <summary>
        /// Action列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ActionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_UserPower_ActionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑功能的Action列表
        /// </summary>
        /// <returns></returns>
        public ActionResult EditActionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_UserPower_EditActionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["fid"].ToLong();
            contentstr = contentstr.Replace("$fid$", id + "");
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑Action
        /// </summary>
        /// <returns></returns>
        public ActionResult EditAction()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_UserPower_EditAction.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 功能信息
            long id = Request.QueryString["aid"].ToLong();
            admin_userpowermanage_function uf = new admin_userpowermanage_function();
            var act = uf.QueryActionById(id);
            contentstr = contentstr.Replace("$id$", id + "");
            if (act == null)
            {
                contentstr = contentstr.Replace("$actionname$", "");
                contentstr = contentstr.Replace("$actioninfo$", "");
            }
            else
            {
                contentstr = contentstr.Replace("$actionname$", act.actionname);
                contentstr = contentstr.Replace("$actioninfo$", act.actioninfo);
            }
            #endregion
            return Content(contentstr);
        }
	}
}