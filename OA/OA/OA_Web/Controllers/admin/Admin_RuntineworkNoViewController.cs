using OA_BLL.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_Models;

namespace OA_Web.Controllers.admin
{
    /// <summary>
    /// 后台-日常工作
    /// </summary>
    public class Admin_RuntineworkNoViewController : Admin_BaseController
    {
        /// <summary>
        /// 备忘录列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryMemorandumList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            long staffid = Request.QueryString["staffid"].ToLong();
            admin_runtineworkmanage_memorandum rm = new admin_runtineworkmanage_memorandum();
            var lstpj = rm.QueryMemorandumList(staffid, companyid).Where(a => a.status == 1).ToList();
            int total = lstpj.Count;
            lstpj = lstpj.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstpj
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑备忘录
        /// </summary>
        /// <returns></returns>
        public ActionResult EditMemorandum()
        {
            returnresult rr = new returnresult();
            long companyid = Request.Form["companyid"].ToLong();
            long staffid = Request.Form["staffid"].ToLong();
            long id = Request.Form["id"].ToLong();
            string content = Request.Form["content"];
            admin_runtineworkmanage_memorandum rm = new admin_runtineworkmanage_memorandum();
            var md = rm.QueryMemorandum(id, companyid);
            DateTime dt = DateTime.Now;
            if (md == null)
            {
                md = new oa_runtinework_memorandums();
                md.addtime = dt;
                md.companyid = companyid;
                md.content = content;
                md.deletetime = "".ToDatetime();
                md.remindtime = dt;
                md.staffid = staffid;
                md.status = 1;
                rr.status = rm.Addmemoreandum(md);
                if (rr.status)
                {
                    rr.msg = "新增成功";
                }
                else
                {
                    rr.msg = "新增失败";
                }
            }
            else
            {
                md.content = content;
                md.addtime = dt;
                rr.status = rm.Updatememoreandum(md);
                if (rr.status)
                {
                    rr.msg = "修改成功";
                }
                else
                {
                    rr.msg = "修改失败";
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除备忘录
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteMemorandum()
        {
            returnresult rr = new returnresult();
            long companyid = Request.Form["companyid"].ToLong();
            long id = Request.Form["id"].ToLong();
            admin_runtineworkmanage_memorandum rm = new admin_runtineworkmanage_memorandum();
            var md = rm.QueryMemorandum(id, companyid);
            rr.status = rm.Deletememoreandum(md);
            if (rr.status)
            {
                rr.msg = "删除成功";
            }
            else
            {
                rr.msg = "删除失败";
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }
	}
}