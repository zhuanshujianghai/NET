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
    /// 后台-审批流程
    /// </summary>
    public class Admin_ApprovalprocessController : Admin_BaseController
    {
        /// <summary>
        /// 项目流程列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectProcessList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_ApprovalProcess_ProjectProcessList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑项目流程信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProjectProcess()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_ApprovalProcess_EditProjectProcess.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 项目流程信息
            long id = Request.QueryString["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var pc = ap.QueryProjectProcessByIdCompanyid(id, companyid);
            contentstr = contentstr.Replace("$id$", id + "");
            if (pc == null)
            {
                contentstr = contentstr.Replace("$projectid$", "1");
                contentstr = contentstr.Replace("$processid$", "1");
            }
            else
            {
                contentstr = contentstr.Replace("$projectid$", pc.projectid + "");
                contentstr = contentstr.Replace("$processid$", pc.processid + "");
            }
            #endregion
            return Content(contentstr);
        }
        /// <summary>
        /// 审批项目列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_ApprovalProcess_ProjectList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }
        /// <summary>
        /// 编辑审批项目
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProject()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_ApprovalProcess_EditProject.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 审批项目信息
            long id = Request.QueryString["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var pc = ap.QueryProjectByIdCompanyid(id, companyid);
            contentstr = contentstr.Replace("$id$", id + "");
            if (pc == null)
            {
                contentstr = contentstr.Replace("$projectname$", "");
                contentstr = contentstr.Replace("$projectinfo$", "");
                contentstr = contentstr.Replace("$filepath$", "");
            }
            else
            {
                contentstr = contentstr.Replace("$projectname$", pc.projectname);
                contentstr = contentstr.Replace("$projectinfo$", pc.projectinfo);
                contentstr = contentstr.Replace("$filepath$", pc.pathfile);
            }
            #endregion
            return Content(contentstr);
        }
        /// <summary>
        /// 审批流程列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_ApprovalProcess_ProcessList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }
        /// <summary>
        /// 编辑审批流程
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProcess()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_ApprovalProcess_EditProcess.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 审批流程信息
            long id = Request.QueryString["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_process ap = new admin_approvalprocessmanage_process();
            var pc = ap.QueryProcessByIdCompanyid(id, companyid);
            contentstr = contentstr.Replace("$id$", id + "");
            if (pc == null)
            {
                contentstr = contentstr.Replace("$processname$", "");
                contentstr = contentstr.Replace("$processinfo$", "");
            }
            else
            {
                contentstr = contentstr.Replace("$processname$", pc.processname);
                contentstr = contentstr.Replace("$processinfo$", pc.processinfo);
            }


            #endregion
            return Content(contentstr);
        }

        /// <summary>
        /// 流程审批人列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ApprovalPersonList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_ApprovalProcess_ApprovalPersonList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long processid = Request.QueryString["processid"].ToLong();
            contentstr = contentstr.Replace("$processid$", processid + "");
            return Content(contentstr);
        }
        /// <summary>
        /// 编辑审批人
        /// </summary>
        /// <returns></returns>
        public ActionResult EditApprovalPerson()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_ApprovalProcess_EditApprovalPerson.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["id"].ToLong();
            long processid = Request.QueryString["processid"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_process ap = new admin_approvalprocessmanage_process();
            var pp = ap.QueryProcessPerson(id, companyid);
            var pc = ap.QueryProcessByIdCompanyid(processid, companyid);
            if (pc == null && id == 0)
            {
                return Content("<h1>未找到该流程</h1>");
            }
            else
            {
                contentstr = contentstr.Replace("$id$", id + "");
                //新增
                if (pp == null)
                {
                    contentstr = contentstr.Replace("$processid$", pc.id + "");
                    contentstr = contentstr.Replace("$processname$", pc.processname);
                    contentstr = contentstr.Replace("$sort$", ap.QueryProcessPersonList(processid, companyid).Count + 1 + "");
                    contentstr = contentstr.Replace("$staffid$", "0");
                }
                //修改
                else
                {
                    contentstr = contentstr.Replace("$processid$", pc.id + "");
                    contentstr = contentstr.Replace("$processname$", pc.processname);
                    contentstr = contentstr.Replace("$sort$", pp.sort + "");
                    contentstr = contentstr.Replace("$staffid$", pp.staffid + "");
                    contentstr = contentstr.Replace("readonly=\"readonly\"", "");
                }
            }
            return Content(contentstr);
        }
        /// <summary>
        /// 员工申请列表
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffApplyList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_ApprovalProcess_StaffApplyList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }
        /// <summary>
        /// 员工申请审批
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffApplyApproval()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_ApprovalProcess_StaffApplyApproval.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["id"].ToLong();
            string type = Request.QueryString["type"];
            contentstr = contentstr.Replace("$type$", type == "see" ? "display:none;" : "");
            contentstr = contentstr.Replace("$id$", id + "");
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var sa = ap.QueryStaffApplyAll(id, companyid, id);
            if (sa == null)
            {
                contentstr = "未找到申请信息";
            }
            else
            {
                var saa = ap.QueryStaffApplyApprovalByApprovalpersonid(sa.approvalpersonid, id, companyid);
                if (sa == null)
                {
                    contentstr = "申请信息异常";
                }
                else
                {
                    admin_membermanage_staff ms = new admin_membermanage_staff();
                    var sf = ms.QueryStaffByIdCompanyid(sa.staffid, companyid);
                    if (sf == null)
                    {
                        contentstr = "申请人信息异常";
                    }
                    else
                    {
                        contentstr = contentstr.Replace("$username$", sf.username);
                        contentstr = contentstr.Replace("$applyname$", sa.applyname);
                    }
                }
            }
            return Content(contentstr);
        }
	}
}