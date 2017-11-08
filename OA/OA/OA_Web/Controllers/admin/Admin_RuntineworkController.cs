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
    /// 后台-日常工作
    /// </summary>
    public class Admin_RuntineworkController : Admin_BaseController
    {
        /// <summary>
        /// 备忘录列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Memorandumlist()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Runtinework_MemorandumList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑备忘录
        /// </summary>
        /// <returns></returns>
        public ActionResult EditMemorandum()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Runtinework_EditMemorandum.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["id"].ToLong();
            contentstr = contentstr.Replace("$id$", id + "");
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_runtineworkmanage_memorandum rm = new admin_runtineworkmanage_memorandum();
            var md = rm.QueryMemorandum(id, companyid);
            if (md == null)
            {
                contentstr = contentstr.Replace("$content$", "");
            }
            else
            {
                contentstr = contentstr.Replace("$content$", md.content);
            }
            return Content(contentstr);
        }
	}
}