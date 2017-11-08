using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_Models;
using OA_BLL.admin;

namespace OA_Web.Controllers.admin
{
    public class Admin_NoticeNoViewController : Admin_BaseController
    {
        /// <summary>
        /// 公告类型列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryNoticetypeList(DataTableParameter param)
        {
            long companyid=Request.QueryString["companyid"].ToLong();
            admin_noticemanage_notice nn = new admin_noticemanage_notice();
            var lstdt = nn.QueryNoticetypeList(companyid);
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
        /// 公告类型列表-下拉列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryNoticeTypeList_Select()
        {
            long companyid=Request.Form["companyid"].ToLong();
            admin_noticemanage_notice nn = new admin_noticemanage_notice();
            var lstdt = nn.QueryNoticetypeList(companyid);
            return Json(lstdt, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑公告类型
        /// </summary>
        /// <returns></returns>
        public ActionResult EditNoticetype()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            string name = Request.Form["name"];
            int sort = Request.Form["sort"].ToInt();
            long staffid = Request.Form["staffid"].ToLong();
            long companyid=Request.Form["companyid"].ToLong();
            admin_noticemanage_notice nn = new admin_noticemanage_notice();
            var nt = nn.QueryNoticetype(id, companyid);
            //新增
            if (nt == null)
            {
                nt = new oa_notice_noticetype();
                nt.addtime = DateTime.Now;
                nt.companyid = companyid;
                nt.name = name;
                nt.sort = sort;
                nt.staffid = staffid;
                nt.status = 0;
                rr = nn.AddNoticetype(nt);
            }
            else//修改
            {
                nt.name = name;
                nt.sort = sort;
                rr = nn.UpdateNoticetype(nt);
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除公告类型
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteNoticetype()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long companyid=Request.Form["companyid"].ToLong();
            admin_noticemanage_notice nn = new admin_noticemanage_notice();
            var nt = nn.QueryNoticetype(id, companyid);
            if (nt == null)
            {
                rr.status = false;
                rr.msg = "未找到该公告类型";
            }
            else
            {
                rr.status = nn.DeleteNoticetype(nt);
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
        /// 公告列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryNoticeList(DataTableParameter param)
        {
            long companyid=Request.QueryString["companyid"].ToLong();
            admin_noticemanage_notice nn = new admin_noticemanage_notice();
            long typeid = Request.QueryString["typeid"].ToLong();
            var lstdt = nn.QueryNoticeListByTypeidCompanyid(typeid, companyid).Where(a => a.status != -1).ToList();
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
        /// 编辑公告
        /// </summary>
        /// <returns></returns>
        public ActionResult EditNotice()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long typeid = Request.Form["typeid"].ToLong();
            string title = Request.Form["title"];
            string content = Request.Form["content"];
            int sort = Request.Form["sort"].ToInt();
            long staffid = Request.Form["staffid"].ToLong();
            long companyid = Request.Form["companyid"].ToLong();
            admin_noticemanage_notice nn = new admin_noticemanage_notice();
            var type = nn.QueryNoticetype(typeid, companyid);
            if (type == null)
            {
                rr.status = false;
                rr.msg = "未找到该公告类型";
            }
            else
            {
                var nt = nn.QueryNoticeByIdCompanyid(id, companyid);
                //新增
                if (nt == null)
                {
                    nt = new oa_notice_notice();
                    nt.clickcount = 0;
                    nt.commentcount = 0;
                    nt.companyid = companyid;
                    nt.content = content;
                    nt.edittime = DateTime.Now;
                    nt.sort = sort;
                    nt.staffid = staffid;
                    nt.status = 0;
                    nt.title = title;
                    nt.typeid = typeid;
                    rr.status = nn.AddNoticecontent(nt);
                    if (rr.status)
                    {
                        rr.msg = "新增公告成功";
                    }
                    else
                    {
                        rr.msg = "新增公告失败";
                    }
                }
                else
                {
                    nt.title = title;
                    nt.content = content;
                    nt.sort = sort;
                    rr.status = nn.UpdateNoticecontent(nt);
                    if (rr.status)
                    {
                        rr.msg = "修改公告成功";
                    }
                    else
                    {
                        rr.msg = "修改公告失败";
                    }
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteNotice()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long companyid=Request.Form["companyid"].ToLong();
            admin_noticemanage_notice nn = new admin_noticemanage_notice();
            var nt = nn.QueryNoticeByIdCompanyid(id, companyid);
            if (nt == null)
            {
                rr.status = false;
                rr.msg = "未找到该公告";
            }
            else
            {
                rr.status = nn.DeleteNoticecontent(nt);
                if (rr.status)
                {
                    rr.msg = "删除公告成功";
                }
                else
                {
                    rr.msg = "删除公告失败";
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 公告评论列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryNoticeCommentList()
        {
            long companyid=Request.Form["companyid"].ToLong();
            admin_noticemanage_notice nn = new admin_noticemanage_notice();
            long noticeid = Request.Form["noticeid"].ToLong();
            int thispage = Request.Form["thispage"].ToInt();
            int pagesize = Request.Form["pagesize"].ToInt();
            var lstdt = nn.QueryNoticeCommentListByIdCompanyid(noticeid, companyid).Where(a => a.status != -1).ToList();
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
        /// 新增公告评论
        /// </summary>
        /// <returns></returns>
        public ActionResult AddNoticeComment()
        {
            returnresult rr = new returnresult();
            long noticeid = Request.Form["noticeid"].ToLong();
            string comment = Request.Form["comment"];
            long staffid = Request.Form["staffid"].ToLong();
            long companyid = Request.Form["companyid"].ToLong();
            admin_noticemanage_notice nn = new admin_noticemanage_notice();
            var nt = nn.QueryNoticeByIdCompanyid(noticeid, companyid);
            if (nt == null)
            {
                rr.status = false;
                rr.msg = "未找到该公告";
            }
            else
            {
                oa_notice_noticecomment nc = new oa_notice_noticecomment();
                nc.addtime = DateTime.Now;
                nc.comment = comment;
                nc.companyid = companyid;
                nc.noticeid = noticeid;
                nc.staffid = staffid;
                nc.status = 0;
                rr.status = nn.AddNoticecomment(nc);
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
	}
}