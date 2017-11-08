using OA_BLL.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;

namespace OA_Web.Controllers.admin
{
    /// <summary>
    /// 后天-论坛
    /// </summary>
    public class Admin_ForumController : Admin_BaseController
    {
        /// <summary>
        /// 版块列表
        /// </summary>
        /// <returns></returns>
        public ActionResult SectionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Forum_SectionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑版块
        /// </summary>
        /// <returns></returns>
        public ActionResult EditSection()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Forum_EditSection.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["id"].ToLong();
            contentstr = contentstr.Replace("$id$", id + "");
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_forummanage_forum ff = new admin_forummanage_forum();
            var se = ff.QuerySectionByIdcCompanyid(id, companyid);
            //新增
            if (se == null)
            {
                contentstr = contentstr.Replace("$sectionname$", "");
                contentstr = contentstr.Replace("$sectioninfo$", "");
            }
            else//修改
            {
                contentstr = contentstr.Replace("$sectionname$", se.sectionname);
                contentstr = contentstr.Replace("$sectioninfo$", se.sectioninfo);
            }
            return Content(contentstr);
        }

        /// <summary>
        /// 主贴列表
        /// </summary>
        /// <returns></returns>
        public ActionResult TopicList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Forum_TopicList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long sectionid = Request.QueryString["sectionid"].ToLong();
            contentstr = contentstr.Replace("$sectionid$", sectionid + "");
            return Content(contentstr);
        }

        /// <summary>
        /// 新增主贴（主贴不能修改，只能新增和删除）
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTopic()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Forum_AddTopic.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long sectionid = Request.QueryString["sectionid"].ToLong();
            contentstr = contentstr.Replace("$sectionid$", sectionid + "");
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_forummanage_forum ff = new admin_forummanage_forum();
            var se = ff.QuerySectionByIdcCompanyid(sectionid, companyid);
            //新增
            if (se == null)
            {
                return Content("未找到版块");
            }
            else
            {
                contentstr = contentstr.Replace("$title$", "");
                contentstr = contentstr.Replace("$content$", "");
            }
            return Content(contentstr);
        }
        /// <summary>
        /// 跟帖列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ReplyList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Forum_ReplyList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long topicid = Request.QueryString["topicid"].ToLong();
            long sectionid = Request.QueryString["sectionid"].ToLong();
            contentstr = contentstr.Replace("$topicid$", topicid + "");
            contentstr = contentstr.Replace("$sectionid$", sectionid + "");
            return Content(contentstr);
        }
	}
}