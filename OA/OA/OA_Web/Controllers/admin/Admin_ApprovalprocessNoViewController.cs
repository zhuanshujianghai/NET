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
    public class Admin_ApprovalprocessNoViewController : Admin_BaseController
    {
        /// <summary>
        /// 审批项目列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryProjectList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var lstap = ap.QueryProjectList(companyid);
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
        /// 查询项目列表-下拉列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryProjectList_Select()
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var lstap = ap.QueryProjectList(companyid);
            return Json(lstap, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 修改审批项目
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProject()
        {
            returnresult rr = new returnresult();
            string projectname = Request.Form["projectname"];
            string projectinfo = Request.Form["projectinfo"];
            HttpPostedFileBase pathfile = Request.Files["filepath"];
            string filepath = Request.Form["pathfile"];
            long id = Request.Form["id"].ToLong();


            //上传文件指定路径
            string path = "/UF/Uploads/ApprovalProjectFile/";
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var pc = ap.QueryProjectByIdCompanyid(id, companyid);
            bool boo = true;
            //新增
            if (pc == null)
            {
                if (pathfile.ContentLength == 0)
                {
                    rr.status = false;
                    rr.msg = "请选择文件";
                }
                else
                {
                    if (pathfile.FileName.Split('.')[1] != "doc" || pathfile.FileName.Split('.')[1] != "docx")
                    {
                        rr.status = false;
                        rr.msg = "暂不支持" + pathfile.FileName.Split('.')[1] + "格式文件上传";
                    }
                    else
                    {
                        boo = UploadFile(path, pathfile);
                        if (boo)
                        {
                            pc = new oa_approvalprocess_projects();
                            pc.projectname = projectname;
                            pc.projectinfo = projectinfo;
                            pc.pathfile = path + pathfile.FileName;
                            pc.addtime = DateTime.Now;
                            pc.companyid = companyid;
                            pc.creator = 0;
                            pc.status = 0;
                            rr = ap.AddProject(pc);
                            //如果新增失败，则删除已上传的图片
                            if (rr.status == false)
                            {
                                System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(path + pathfile.FileName));
                                if (file.Exists)
                                {
                                    file.Delete();//删除
                                }
                            }
                        }
                        {
                            rr.status = false;
                            rr.msg = "文件上传失败";
                        }
                    }
                }
            }
            //修改
            else
            {
                if (pathfile.ContentLength == 0 || pc.pathfile == path + pathfile.FileName)
                {
                    //文件名称相同则不再上传，防止重复上传
                }
                else
                {
                    if (pathfile.FileName.Split('.')[1] != "doc" || pathfile.FileName.Split('.')[1] != "docx")
                    {
                        rr.status = false;
                        rr.msg = "暂不支持【" + pathfile.FileName.Split('.')[1] + "】格式文件上传";
                        return Json(rr, JsonRequestBehavior.DenyGet);
                    }
                    else
                    {
                        boo = UploadFile(path, pathfile);
                        if (boo)
                        {
                            pc.pathfile = path + pathfile.FileName;
                        }
                    }
                }
                if (boo)
                {
                    pc.projectname = projectname;
                    pc.projectinfo = projectinfo;
                    rr = ap.UpdateProject(pc);
                    //如果修改失败，则删除已上传的图片
                    if (rr.status == false)
                    {
                        System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(path + pathfile.FileName));
                        if (file.Exists)
                        {
                            file.Delete();//删除
                        }
                    }
                }
                else
                {
                    rr.status = false;
                    rr.msg = "文件上传失败";
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 删除审批项目
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteProject()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var pc = ap.QueryProjectByIdCompanyid(id, companyid);
            if (pc == null)
            {
                rr.status = false;
                rr.msg = "未找到该审批项目";
            }
            else
            {
                rr.status = ap.DeleteProject(pc);
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
        /// 审批流程列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryProcessList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_process ap = new admin_approvalprocessmanage_process();
            var lstap = ap.QueryProcessList(companyid);
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
        /// 查询流程列表-下拉列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryProcessList_Select()
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_process ap = new admin_approvalprocessmanage_process();
            var lstap = ap.QueryProcessList(companyid);
            return Json(lstap, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 编辑审批流程
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProcess()
        {
            returnresult rr = new returnresult();
            string processname = Request.Form["processname"];
            string processinfo = Request.Form["processinfo"];
            long id = Request.Form["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_process ap = new admin_approvalprocessmanage_process();
            var pc = ap.QueryProcessByIdCompanyid(id, companyid);
            //新增
            if (pc == null)
            {
                pc = new oa_approvalprocess_process();
                pc.processname = processname;
                pc.processinfo = processinfo;
                pc.addtime = DateTime.Now;
                pc.companyid = companyid;
                pc.creator = 0;
                pc.status = 0;
                rr = ap.AddProcess(pc);
            }
            //修改
            else
            {
                pc.processname = processname;
                pc.processinfo = processinfo;
                rr = ap.UpdateProcess(pc);
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 删除审批流程
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteProcess()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_process ap = new admin_approvalprocessmanage_process();
            var pc = ap.QueryProcessByIdCompanyid(id, companyid);
            if (pc == null)
            {
                rr.status = false;
                rr.msg = "未找到该审批流程";
            }
            else
            {
                rr.status = ap.DeleteProcess(pc);
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
        /// 项目流程列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryProjectProcessList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var lstap = ap.QueryProjectProcessList(companyid);
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
        /// 查询项目流程列表-用于下拉列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryProjectProcessList_Select()
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var lstap = ap.QueryProjectProcessList_Select(companyid);
            return Json(lstap);
        }

        /// <summary>
        /// 编辑项目流程
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProjectProcess()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long projectid = Request.Form["projectid"].ToLong();
            long processid = Request.Form["processid"].ToLong();
            string projectprocessname = Request.Form["projectprocessname"];
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var pp = ap.QueryProjectProcessByIdCompanyid(id, companyid);
            //新增
            if (pp == null)
            {
                pp = new oa_approvalprocess_projectprocess();
                pp.processid = processid;
                pp.projectid = projectid;
                pp.companyid = companyid;
                pp.projectprocessname = projectprocessname;
                pp.status = 0;
                rr.status = ap.AddProjectProcess(pp);
                if (rr.status)
                {
                    rr.msg = "新增成功";
                }
                else
                {
                    rr.msg = "新增失败";
                }
            }
            //修改
            else
            {
                pp.projectid = projectid;
                pp.processid = processid;
                pp.projectprocessname = projectprocessname;
                rr.status = ap.UpdateProjectProcess(pp);
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
        /// 删除项目流程
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteProjectProcess()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var pc = ap.QueryProjectProcessByIdCompanyid(id, companyid);
            if (pc == null)
            {
                rr.status = false;
                rr.msg = "未找到该项目流程";
            }
            else
            {
                rr.status = ap.DeleteProjectProcess(pc);
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
        /// 流程审批人列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryProcessPersonList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            long processid = Request.QueryString["processid"].ToLong();
            admin_approvalprocessmanage_process ap = new admin_approvalprocessmanage_process();
            var lstap = ap.QueryProcessPersonList(processid, companyid);
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
        /// 编辑流程审批人
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProcessPerson()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            int sort = Request.Form["sort"].ToInt();
            long staffid = Request.Form["staffid"].ToLong();
            long processid = Request.Form["processid"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_process ap = new admin_approvalprocessmanage_process();
            var pp = ap.QueryProcessPerson(id, companyid);
            var pc = ap.QueryProcessByIdCompanyid(processid, companyid);
            int sortcount = ap.QueryProcessPersonList(processid, companyid).Count;
            if (pc == null)
            {
                rr.status = false;
                rr.msg = "未找到该流程";
            }
            else
            {
                admin_membermanage_staff ms = new admin_membermanage_staff();
                if (ms.QueryStaffByIdCompanyid(staffid, companyid) == null)
                {
                    rr.status = false;
                    rr.msg = "未找到该审批人";
                }
                else
                {
                    //新增
                    if (pp == null)
                    {
                        pp = new oa_approvalprocess_approvalpersons();
                        pp.processid = processid;
                        pp.sort = sortcount + 1;
                        pp.staffid = staffid;
                        pp.status = 0;
                        rr = ap.AddApprovalperson(pp);
                    }
                    //修改
                    else
                    {

                        //修改顺序，将两个审批人顺序交换
                        if (sort >= 1 && sort <= sortcount)
                        {
                            var changepp = ap.QueryProcessPerson(sort, pp.processid, companyid);
                            if (changepp == null)
                            {
                                //未找到该排序的审批人，不进行处理
                            }
                            else
                            {
                                changepp.sort = pp.sort;
                                rr = ap.UpdateApprovalperson(changepp);
                            }
                            if (rr.status)
                            {
                                pp.sort = sort;
                            }
                            else
                            {
                                pp.sort = sortcount + 1;
                            }
                            pp.sort = sort;
                        }
                        else
                        {
                            pp.sort = pp.sort;
                        }
                        pp.staffid = staffid;
                        rr = ap.UpdateApprovalperson(pp);
                    }
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除流程审批人
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteProcessPerson()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_process ap = new admin_approvalprocessmanage_process();
            var pp = ap.QueryProcessPerson(id, companyid);
            if (pp == null)
            {
                rr.status = false;
                rr.msg = "未找到该流程审批人";
            }
            else
            {
                rr.status = ap.DeleteApprovalperson(pp, companyid);
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
        /// 员工申请列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryStaffApplyList(DataTableParameter param)
        {
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var lstap = ap.QueryStaffApplyList(companyid);
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
        /// 员工申请审批
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffApplyApproval()
        {
            returnresult rr = new returnresult();
            int status = Request.Form["status"].ToInt();
            string remark = Request.Form["remark"];
            long id = Request.Form["id"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var sa = ap.QueryStaffApplyAll(id, companyid, 0);
            if (sa == null)
            {
                rr.status = false;
                rr.msg = "未找到该申请相关信息";
            }
            else
            {
                var saa = ap.QueryStaffApplyApprovalByApprovalpersonid(sa.approvalpersonid, id, companyid);
                if (saa == null)
                {
                    rr.status = false;
                    rr.msg = "未找到审批信息";
                }
                else
                {
                    if (saa.status != 0)
                    {
                        saa.status = status;
                        saa.remark = remark;
                        saa.approvaltime = DateTime.Now;
                        rr.status = ap.UpdateStaffApplyApproval(saa);
                        if (rr.status)
                        {
                            var lstsaa = ap.QueryStaffApplyApprovalList(companyid, id);
                            if (lstsaa != null)
                            {
                                var sasa = ap.QueryStaffApply(companyid, id);
                                //最后一个审批
                                if (lstsaa.Count == sa.sort)
                                {
                                    sasa.status = status == 1 ? 2 : 3;
                                }
                                else
                                {
                                    sasa.status = 1;
                                }
                                bool temp = ap.UpdateStaffapply(sasa);
                                if (temp)
                                {
                                    rr.msg = "审批成功";
                                }
                                else
                                {
                                    rr.msg = "审批成功,修改申请状态失败";
                                }
                            }
                            else
                            {
                                rr.msg = "审批成功,未找到审批信息";
                            }
                        }
                        else
                        {
                            rr.msg = "审批失败";
                        }
                    }
                    else
                    {
                        rr.status = false;
                        rr.msg = "该申请已经审批";
                    }
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 员工申请审批列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryStaffApplyApprovalList(DataTableParameter param)
        {
            long staffapplyid = Request.QueryString["staffapplyid"].ToLong();
            long companyid = Request.QueryString["companyid"].ToLong();
            admin_approvalprocessmanage_project ap = new admin_approvalprocessmanage_project();
            var lstap = ap.QueryStaffApplyApprovalList(companyid, staffapplyid);
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
	}
}