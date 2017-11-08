using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_BLL.user;
using OA_Models;
using System.Web.UI.WebControls;
using System.IO;

namespace OA_Web.Controllers.user
{
    public class ApprovalprocessNoViewController : BaseController
    {
        /// <summary>
        /// 审批项目列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryProjectList(DataTableParameter param)
        {
            var me = getme();
            approvalprocessmanage_project ap = new approvalprocessmanage_project();
            var lstap = ap.QueryProjectList(me.companyid);
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
            var me = getme();
            approvalprocessmanage_project ap = new approvalprocessmanage_project();
            var lstap = ap.QueryProjectList(me.companyid);
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
            var me = getme();
            approvalprocessmanage_project ap = new approvalprocessmanage_project();
            var pc = ap.QueryProjectByIdCompanyid(id, me.companyid);
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
                            pc.companyid = me.companyid;
                            pc.creator = me.id;
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
            var me = getme();
            approvalprocessmanage_project ap = new approvalprocessmanage_project();
            var pc = ap.QueryProjectByIdCompanyid(id, me.companyid);
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
            var me = getme();
            approvalprocessmanage_process ap = new approvalprocessmanage_process();
            var lstap = ap.QueryProcessList(me.companyid);
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
            var me = getme();
            approvalprocessmanage_process ap = new approvalprocessmanage_process();
            var lstap = ap.QueryProcessList(me.companyid);
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
            var me = getme();
            approvalprocessmanage_process ap = new approvalprocessmanage_process();
            var pc = ap.QueryProcessByIdCompanyid(id, me.companyid);
            //新增
            if (pc == null)
            {
                pc = new oa_approvalprocess_process();
                pc.processname = processname;
                pc.processinfo = processinfo;
                pc.addtime = DateTime.Now;
                pc.companyid = me.companyid;
                pc.creator = me.id;
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
            var me = getme();
            approvalprocessmanage_process ap = new approvalprocessmanage_process();
            var pc = ap.QueryProcessByIdCompanyid(id, me.companyid);
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
            var me = getme();
            approvalprocessmanage_project ap = new approvalprocessmanage_project();
            var lstap = ap.QueryProjectProcessList(me.companyid);
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
            var me = getme();
            approvalprocessmanage_project ap = new approvalprocessmanage_project();
            var lstap = ap.QueryProjectProcessList_Select(me.companyid);
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
            var me = getme();
            approvalprocessmanage_project ap = new approvalprocessmanage_project();
            var pp = ap.QueryProjectProcessByIdCompanyid(id, me.companyid);
            //新增
            if (pp == null)
            {
                pp = new oa_approvalprocess_projectprocess();
                pp.processid = processid;
                pp.projectid = projectid;
                pp.companyid = me.companyid;
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
            var me = getme();
            approvalprocessmanage_project ap = new approvalprocessmanage_project();
            var pc = ap.QueryProjectProcessByIdCompanyid(id, me.companyid);
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
            var me = getme();
            long processid = Request.QueryString["processid"].ToLong();
            approvalprocessmanage_process ap = new approvalprocessmanage_process();
            var lstap = ap.QueryProcessPersonList(processid, me.companyid);
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
            var me = getme();
            approvalprocessmanage_process ap = new approvalprocessmanage_process();
            var pp = ap.QueryProcessPerson(id, me.companyid);
            var pc = ap.QueryProcessByIdCompanyid(processid, me.companyid);
            int sortcount = ap.QueryProcessPersonList(processid, me.companyid).Count;
            if (pc == null)
            {
                rr.status = false;
                rr.msg = "未找到该流程";
            }
            else
            {
                membermanage_staff ms = new membermanage_staff();
                if (ms.QueryStaffByIdCompanyid(staffid, me.companyid) == null)
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
                            var changepp = ap.QueryProcessPerson(sort, pp.processid, me.companyid);
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
            var me = getme();
            approvalprocessmanage_process ap = new approvalprocessmanage_process();
            var pp = ap.QueryProcessPerson(id, me.companyid);
            if (pp == null)
            {
                rr.status = false;
                rr.msg = "未找到该流程审批人";
            }
            else
            {
                rr.status = ap.DeleteApprovalperson(pp, me.companyid);
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
            var me = getme();
            approvalprocessmanage_project ap = new approvalprocessmanage_project();
            var lstap = ap.QueryStaffApplyList(me.companyid);
            if (me.isall == 0)
            {
                lstap = lstap.Where(a => a.approvalstaffid == me.id).ToList();
            }
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
            var me = getme();
            approvalprocessmanage_project ap = new approvalprocessmanage_project();
            var sa = ap.QueryStaffApplyAll(id, me.companyid,me.id);
            if (sa == null)
            {
                rr.status = false;
                rr.msg = "未找到该申请相关信息";
            }
            else
            {
                var saa = ap.QueryStaffApplyApprovalByApprovalpersonid(sa.approvalpersonid, id, me.companyid);
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
                            var lstsaa = ap.QueryStaffApplyApprovalList(me.companyid, id);
                            if (lstsaa != null)
                            {
                                var sasa = ap.QueryStaffApply(me.companyid, id);
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
        /// 申请审批流程
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplyApprovalprocess()
        {
            returnresult rr = new returnresult();
            string applyname = Request.Form["applyname"];
            string remark = Request.Form["remark"];
            long projectprocessid = Request.Form["projectprocessid"].ToLong();
            long id = Request.Form["id"].ToLong();
            var me = getme();
            approvalprocessmanage_project ap = new approvalprocessmanage_project();
            approvalprocessmanage_process app = new approvalprocessmanage_process();
            var sa = ap.QueryStaffApply(me.companyid, id);
            var pp = ap.QueryProjectProcessByIdCompanyid(projectprocessid, me.companyid);
            if (pp == null)
            {
                rr.status = false;
                rr.msg = "未找到该项目流程";
            }
            else
            {
                //新增
                if (sa == null)
                {
                    sa = new oa_approvalprocess_staffapplys();
                    sa.addtime = DateTime.Now;
                    sa.applyname = applyname;
                    sa.companyid = me.companyid;
                    sa.projectprocessid = projectprocessid;
                    sa.remark = remark;
                    sa.staffid = me.id;
                    sa.status = 0;
                    rr.status = ap.AddStaffapply(sa);
                    if (rr.status)
                    {
                        var lstap = app.QueryProcessPersonById(pp.processid);
                        List<oa_approvalprocess_staffapplyapprovals> lstsaa = new List<oa_approvalprocess_staffapplyapprovals>();
                        foreach (var item in lstap)
                        {
                            oa_approvalprocess_staffapplyapprovals saa = new oa_approvalprocess_staffapplyapprovals();
                            saa.approvalpersonid = item.id;
                            saa.approvaltime = "".ToDatetime();
                            saa.companyid = me.companyid;
                            saa.remark = "";
                            saa.staffapplyid = sa.id;
                            saa.status = 0;
                            lstsaa.Add(saa);
                        }
                        bool temp = ap.AddStaffApplyApproval(lstsaa);
                        if (temp)
                        {
                            rr.msg = "新增成功";
                        }
                        else
                        {
                            ap.DeleteStaffapply(sa);
                            rr.status = false;
                            rr.msg = "新增审批信息失败";
                        }
                    }
                    else
                    {
                        rr.msg = "新增失败";
                    }
                }
                else//修改
                {
                    if (sa.status == 0)
                    {
                        sa.applyname = applyname;
                        sa.remark = remark;
                        bool boo = false;
                        if (sa.projectprocessid != projectprocessid)
                        {
                            var lstsa = ap.QueryStaffApplyApprovalByStaffApplyid(me.companyid, sa.id);
                            bool temp = ap.DeleteStaffApplyApproval(lstsa);
                            var lstap = app.QueryProcessPersonById(pp.processid);
                            List<oa_approvalprocess_staffapplyapprovals> lstsaa = new List<oa_approvalprocess_staffapplyapprovals>();
                            foreach (var item in lstap)
                            {
                                oa_approvalprocess_staffapplyapprovals saa = new oa_approvalprocess_staffapplyapprovals();
                                saa.approvalpersonid = item.id;
                                saa.approvaltime = "".ToDatetime();
                                saa.companyid = me.companyid;
                                saa.remark = "";
                                saa.staffapplyid = sa.id;
                                saa.status = 0;
                                lstsaa.Add(saa);
                            }
                            bool temp1 = ap.AddStaffApplyApproval(lstsaa);
                            if (temp1 && temp)
                            {
                                sa.projectprocessid = projectprocessid;
                                boo = true;
                            }
                            else
                            {
                                ap.AddStaffApplyApproval(lstsa);
                                ap.DeleteStaffApplyApproval(lstsaa);
                            }
                        }
                        rr.status = ap.UpdateStaffapply(sa);
                        if (rr.status)
                        {

                            if (boo)
                            {
                                rr.msg = "修改成功";
                            }
                            else
                            {
                                rr.msg = "修改成功,项目流程修改失败";
                            }
                        }
                        else
                        {
                            rr.msg = "修改失败";
                        }
                    }
                    else
                    {
                        rr.status = false;
                        rr.msg = "该申请已审核，不能修改";
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
            var me = getme();
            approvalprocessmanage_project ap = new approvalprocessmanage_project();
            var lstap = ap.QueryStaffApplyApprovalList(me.companyid, staffapplyid);
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