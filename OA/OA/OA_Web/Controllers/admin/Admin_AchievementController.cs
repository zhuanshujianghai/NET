using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_BLL.admin;

namespace OA_Web.Controllers.admin
{
    /// <summary>
    /// 后台-绩效
    /// </summary>
    public class Admin_AchievementController : Admin_BaseController
    {
        /// <summary>
        /// 绩效制度列表
        /// </summary>
        /// <returns></returns>
        public ActionResult InstitutionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Achievement_InstitutionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑绩效制度
        /// </summary>
        /// <returns></returns>
        public ActionResult EditInstitution()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Achievement_EditInstitution.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["id"].ToLong();
            contentstr = contentstr.Replace("$id$", id + "");
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_achievementmanage_achievement aa = new admin_achievementmanage_achievement();
            var it = aa.QueryInstitution(id, companyid);
            if (it == null)
            {
                contentstr = contentstr.Replace("$title$", "");
                contentstr = contentstr.Replace("$content$", "");
                contentstr = contentstr.Replace("$impactscore$", "");
            }
            else
            {
                contentstr = contentstr.Replace("$title$", it.title);
                contentstr = contentstr.Replace("$content$", it.content);
                contentstr = contentstr.Replace("$impactscore$", it.impactscore + "");
            }
            return Content(contentstr);
        }

        /// <summary>
        /// 新增绩效明细
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAchievementInfo()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Achievement_AddAchievementInfo.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 员工绩效列表
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffAchievementList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Achievement_StaffAchievementList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 员工绩效明细
        /// </summary>
        /// <returns></returns>
        public ActionResult AchievementInfoList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Achievement_AchievementInfoList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }
	}
}