using OA_BLL.admin;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;

namespace OA_Web.Controllers.admin
{
    /// <summary>
    /// 后台-论坛
    /// </summary>
    public class Admin_ForumNoViewController : Admin_BaseController
    {
        /// <summary>
        /// 版块列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QuerySectionList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_forummanage_forum ff = new admin_forummanage_forum();
            var lstap = ff.QuerySectionListByCompanyid(companyid);
            int total = lstap.Count;
            lstap = lstap.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstap
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑版块
        /// </summary>
        /// <returns></returns>
        public ActionResult EditSection()
        {
            returnresult rr = new returnresult();
            string sectionname = Request.Form["sectionname"];
            string sectioninfo = Request.Form["sectioninfo"];
            long id = Request.Form["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_forummanage_forum ff = new admin_forummanage_forum();
            var se = ff.QuerySectionByIdcCompanyid(id, companyid);
            //新增
            if (se == null)
            {
                se = new oa_forum_section();
                se.addtime = DateTime.Now;
                se.companyid = companyid;
                se.sectioninfo = sectioninfo;
                se.sectionname = sectionname;
                se.staffid = 0;
                se.status = 0;
                se.topiccount = 0;
                rr = ff.AddSection(se);

            }
            else//修改
            {
                se.sectionname = sectionname;
                se.sectioninfo = sectioninfo;
                rr = ff.UpdateSection(se);
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除版块
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSection()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_forummanage_forum ff = new admin_forummanage_forum();
            var se = ff.QuerySectionByIdcCompanyid(id, companyid);
            if (se == null)
            {
                rr.status = false;
                rr.msg = "未找到该版块";
            }
            else
            {
                rr.status = ff.DeleteSection(se);
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
        /// 主贴列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryTopicList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            long sectionid = Request.QueryString["sectionid"].ToLong();
            admin_forummanage_forum ff = new admin_forummanage_forum();
            var lstap = ff.QueryTopicByCompanyId(sectionid, companyid);
            int total = lstap.Count;
            lstap = lstap.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstap
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增主贴（主贴不能修改，只能新增和删除）
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTopic()
        {
            returnresult rr = new returnresult();
            long sectionid = Request.Form["sectionid"].ToLong();
            string title = Request.Form["title"];
            string content = Request.Form["content"];
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_forummanage_forum ff = new admin_forummanage_forum();
            var se = ff.QuerySectionByIdcCompanyid(sectionid, companyid);
            if (se == null)
            {
                rr.status = false;
                rr.msg = "未找到该版块";
            }
            else
            {
                oa_forum_topic tp = new oa_forum_topic();
                tp.addtime = DateTime.Now;
                tp.clickcount = 0;
                tp.companyid = companyid;
                tp.content = content;
                tp.lastclicktime = DateTime.Now;
                tp.sectionid = sectionid;
                tp.staffid = 0;
                tp.status = 0;
                tp.title = title;
                rr.status = ff.AddTopic(tp);
                if (rr.status)
                {
                    se.topiccount += 1;
                    var re = ff.UpdateSection(se);
                    if (re.status == false)
                    {
                        //记录此异常更新
                    }
                    rr.msg = "新增成功";
                }
                else
                {
                    rr.msg = "新增失败";
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除主贴
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteTopic()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_forummanage_forum ff = new admin_forummanage_forum();
            var tp = ff.QueryTopicByIdCompanyid(id, companyid);
            if (tp == null)
            {
                rr.status = false;
                rr.msg = "未找到该帖子";
            }
            else
            {
                if (tp.staffid != 0)
                {
                    rr.status = false;
                    rr.msg = "不能删除别人的帖子";
                }
                else
                {
                    rr.status = ff.DeleteTopic(tp);
                    if (rr.status)
                    {
                        rr.msg = "删除成功";
                    }
                    else
                    {
                        rr.msg = "删除失败";
                    }
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 跟帖列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryReplyList()
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_forummanage_forum ff = new admin_forummanage_forum();
            long topicid = Request.Form["topicid"].ToLong();
            int thispage = Request.Form["thispage"].ToInt();
            int pagesize = Request.Form["pagesize"].ToInt();
            var lstdt = ff.QueryReplyListByTopicidCompanyid(topicid, companyid).Where(a => a.status != -1).ToList();
            int total = lstdt.Count;
            lstdt = lstdt.Take(pagesize * (thispage - 1) + pagesize).Skip(pagesize * (thispage - 1)).ToList();
            return Json(new
            {
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstdt
            }, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 新增跟帖
        /// </summary>
        /// <returns></returns>
        public ActionResult AddReply()
        {
            returnresult rr = new returnresult();
            long topicid = Request.Form["topicid"].ToLong();
            string content = Request.Form["content"];
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_forummanage_forum ff = new admin_forummanage_forum();
            var tp = ff.QueryTopicByIdCompanyid(topicid, companyid);
            if (tp == null)
            {
                rr.status = false;
                rr.msg = "未找到该帖子";
            }
            else
            {
                oa_forum_reply rp = new oa_forum_reply();
                rp.addtime = DateTime.Now;
                rp.companyid = companyid;
                rp.content = content;
                rp.staffid = 0;
                rp.status = 0;
                rp.topicid = topicid;
                rr.status = ff.AddReply(rp);
                if (rr.status)
                {
                    rr.msg = "新增成功";
                }
                else
                {
                    rr.msg = "新增失败";
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除跟帖
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteReply()
        {
            returnresult rr = new returnresult();
            long replyid = Request.Form["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_forummanage_forum ff = new admin_forummanage_forum();
            var rp = ff.QueryReplyByIdCompanyid(replyid, companyid);
            if (rp == null)
            {
                rr.status = false;
                rr.msg = "未找到该评论";
            }
            else
            {
                rr.status = ff.DeleteReply(rp);
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
	}
}