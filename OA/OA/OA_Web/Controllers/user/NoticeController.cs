﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_BLL.user;
using OA_Models;

namespace OA_Web.Controllers.user
{
    /// <summary>
    /// 公告
    /// </summary>
    public class NoticeController : BaseController
    {
        /// <summary>
        /// 公告类型列表
        /// </summary>
        /// <returns></returns>
        public ActionResult NoticetypeList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Notice_NoticetypeList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑公告类型
        /// </summary>
        /// <returns></returns>
        public ActionResult EditNoticetype()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Notice_EditNoticetype.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["id"].ToLong();
            contentstr = contentstr.Replace("$id$",id+"");
            var me = getme();
            noticemanage_notice nn = new noticemanage_notice();
            var nt = nn.QueryNoticetype(id,me.companyid);
            //新增
            if (nt == null)
            {
                contentstr = contentstr.Replace("$name$","");
                contentstr = contentstr.Replace("$sort$","0");
            }
            else//修改
            {
                contentstr = contentstr.Replace("$name$", nt.name);
                contentstr = contentstr.Replace("$sort$", nt.sort+"");
            }
            return Content(contentstr);
        }

        /// <summary>
        /// 公告列表
        /// </summary>
        /// <returns></returns>
        public ActionResult NoticeList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Notice_NoticeList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long typeid = Request.QueryString["typeid"].ToLong();
            contentstr = contentstr.Replace("$typeid$",typeid+"");
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑公告
        /// </summary>
        /// <returns></returns>
        public ActionResult EditNotice()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Notice_EditNotice.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["id"].ToLong();
            long typeid = Request.QueryString["typeid"].ToLong();
            contentstr = contentstr.Replace("$id$", id + "");
            contentstr = contentstr.Replace("$typeid$",typeid+"");
            var me = getme();
            noticemanage_notice nn = new noticemanage_notice();
            var nt = nn.QueryNoticeByIdCompanyid(id, me.companyid);
            //新增
            if (nt == null)
            {
                contentstr = contentstr.Replace("$title$", "");
                contentstr = contentstr.Replace("$content$", "");
                contentstr = contentstr.Replace("$sort$", "0");
            }
            else//修改
            {
                contentstr = contentstr.Replace("$title$", nt.title);
                contentstr = contentstr.Replace("$content$", nt.content);
                contentstr = contentstr.Replace("$sort$", nt.sort+"");
            }
            return Content(contentstr);
        }

        /// <summary>
        /// 公告评论列表
        /// </summary>
        /// <returns></returns>
        public ActionResult NoticeCommentList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Notice_NoticeCommentList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long noticeid = Request.QueryString["noticeid"].ToLong();
            contentstr = contentstr.Replace("$noticeid$", noticeid + "");
            return Content(contentstr);
        }

        /// <summary>
        /// 新增公告评论
        /// </summary>
        /// <returns></returns>
        public ActionResult AddNoticeComment()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Notice_AddNoticeComment.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long noticeid = Request.QueryString["noticeid"].ToLong();
            var me = getme();
            noticemanage_notice nn = new noticemanage_notice();
            var nt = nn.QueryNoticeByIdCompanyid(noticeid,me.companyid);
            return Content(contentstr);
        }
	}
}