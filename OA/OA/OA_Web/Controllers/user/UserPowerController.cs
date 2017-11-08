using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_BLL.user;

namespace OA_Web.Controllers.user
{
    public class UserPowerController : BaseController
    {
        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult PageMenuList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/UserPower_PageMenuList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPageMenu()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/UserPower_EditPageMenu.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 菜单信息
            long id = Request.QueryString["id"].ToLong();
            var me = getme();
            userpowermanage_pagemenu up = new userpowermanage_pagemenu();
            var menu = up.QueryPageMenuByIdCompanyid(id,me.companyid);
            contentstr = contentstr.Replace("$menuid$", id + "");
            if (menu == null)
            {
                contentstr = contentstr.Replace("$name$", "");
                contentstr = contentstr.Replace("$remark$", "");
            }
            else
            {
                contentstr = contentstr.Replace("$name$", menu.name);
                contentstr = contentstr.Replace("$remark$", menu.remark);
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
            var str = Server.MapPath("/Template/user/UserPower_UserPowerLogList.html");
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
            var str = Server.MapPath("/Template/user/UserPower_FunctionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
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
            var str = Server.MapPath("/Template/user/UserPower_EditFunctionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["pid"].ToLong();
            contentstr = contentstr.Replace("$positionid$", id + "");
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑功能
        /// </summary>
        /// <returns></returns>
        public ActionResult EditFunction()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/UserPower_EditFunction.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 功能信息
            long id = Request.QueryString["fid"].ToLong();
            var me = getme();
            userpowermanage_function uf = new userpowermanage_function();
            var fun = uf.QueryFunctionByIdCompanyid(id);
            contentstr = contentstr.Replace("$id$", id + "");
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
            var str = Server.MapPath("/Template/user/UserPower_ActionList.html");
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
            var str = Server.MapPath("/Template/user/UserPower_EditActionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["fid"].ToLong();
            contentstr = contentstr.Replace("$fid$",id+"");
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑Action
        /// </summary>
        /// <returns></returns>
        public ActionResult EditAction()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/UserPower_EditAction.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 功能信息
            long id = Request.QueryString["aid"].ToLong();
            var me = getme();
            userpowermanage_function uf = new userpowermanage_function();
            var act = uf.QueryActionByIdCompanyid(id);
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