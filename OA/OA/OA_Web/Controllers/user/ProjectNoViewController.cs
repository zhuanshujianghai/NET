using OA_BLL.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_Models;

namespace OA_Web.Controllers.user
{
    public class ProjectNoViewController : BaseController
    {
        /// <summary>
        /// 项目列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryProjectList(DataTableParameter param)
        {
            var me = getme();
            projectmanage_project pp = new projectmanage_project();
            var lstpj = pp.QueryProjectList(me.companyid);
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
        /// 修改项目信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProject()
        {
            returnresult rr = new returnresult();
            string projectname = Request.Form["projectname"];
            string projectinfo = Request.Form["projectinfo"];
            decimal progress = Request.Form["progress"].ToDecimal();
            int status = Request.Form["status"].ToInt();
            string person_responsiblename = Request.Form["person_responsiblename"];
            long id = Request.Form["id"].ToLong();
            DateTime expectbegintime = Request.Form["expectbegintime"].ToDatetime();
            DateTime expectfinishtime = Request.Form["expectfinishtime"].ToDatetime();
            var me = getme();
            projectmanage_project pp = new projectmanage_project();
            var pt = pp.QueryProjectById(id, me.companyid);
            DateTime dt = DateTime.Now;
            //新增
            if (pt == null)
            {
                pt = new oa_project_projects();
                pt.addtime = dt;
                pt.companyid = me.companyid;
                pt.expectbegintime = expectbegintime;
                pt.expectfinishtime = expectfinishtime;
                pt.person_responsibleid = me.id;
                pt.progress = 0;
                pt.projectinfo = projectinfo;
                pt.projectname = projectname;
                pt.realitybegintime = "".ToDatetime();
                pt.realityfinishtime = "".ToDatetime();
                pt.staffid = me.id;
                pt.status = 0;
                pt.updatetime = dt;
                rr = pp.AddProject(pt);
            }
            //修改
            else
            {
                membermanage_staff ms = new membermanage_staff();
                var pr = ms.QueryStaffByUsername(person_responsiblename, me.companyid);
                if (pr == null)
                {
                    rr.status = false;
                    rr.msg = "未找到负责人";
                }
                else
                {
                    pt.person_responsibleid = pr.id;
                    pt.progress = progress;
                    pt.projectinfo = projectinfo;
                    pt.projectname = projectname;
                    pt.status = status;
                    rr = pp.UpdateProject(pt);
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 修改项目部分时间
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProjectTime()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            string timetype = Request.Form["timetype"];
            DateTime realitybegintime = Request.Form["realitybegintime"].ToDatetime();
            DateTime expectfinishtime = Request.Form["expectfinishtime"].ToDatetime();
            DateTime updatetime = Request.Form["updatetime"].ToDatetime();
            var me = getme();
            projectmanage_project pp = new projectmanage_project();
            var pt = pp.QueryProjectById(id, me.companyid);
            DateTime dt = DateTime.Now;
            if (pt == null)
            {
                rr.status = false;
                rr.msg = "未找到该项目";
            }
            else
            {
                switch (timetype)
                {
                    case "realitybegintime":
                        pt.realitybegintime = dt;
                        break;
                    case "realityfinishtime":
                        pt.realityfinishtime = dt;
                        break;
                    case "updatetime":
                        pt.updatetime = dt;
                        break;
                    default:
                        break;
                }
                rr = pp.UpdateProject(pt);
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除项目（修改状态为删除状态）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteProject()
        {
            returnresult rr = new returnresult();
            long pid = Request.Form["pid"].ToLong();
            var me = getme();
            projectmanage_project pp = new projectmanage_project();
            var pt = pp.QueryProjectById(pid, me.companyid);
            if (pt == null)
            {
                rr.status = false;
                rr.msg = "未找到该项目";
            }
            else
            {
                pt.status = -1;
                rr = pp.UpdateProject(pt);
                rr.msg = rr.msg.Replace("修改", "删除");
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 编辑项目人员
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProjectStaff()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long pid = Request.Form["pid"].ToLong();
            string username = Request.Form["username"];
            string rolename = Request.Form["rolename"];
            var me = getme();
            projectmanage_project pp = new projectmanage_project();
            membermanage_staff ms = new membermanage_staff();
            var sf = ms.QueryStaffByUsername(username, me.companyid);
            var ps = pp.QueryProjectStaffByIdCompanyid(id, me.companyid);

            if (sf == null)
            {
                rr.status = false;
                rr.msg = "该用户名不存在";
            }
            else
            {
                //新增
                if (ps == null)
                {
                    var pt = pp.QueryProjectById(pid, me.companyid);
                    if (pt == null)
                    {
                        rr.status = false;
                        rr.msg = "该项目不存在";
                    }
                    else
                    {
                        ps = new oa_project_projectsstaffs();
                        ps.projectid = pid;
                        ps.rolename = rolename;
                        ps.staffid = sf.id;
                        ps.companyid = me.companyid;
                        ps.status = 0;
                        rr.status = pp.AddProjectstaff(ps);
                        if (rr.status)
                        {
                            rr.msg = "新增成功";
                        }
                        else
                        {
                            rr.msg = "新增失败";
                        }
                    }
                }
                //修改
                else
                {
                    ps.rolename = rolename;
                    ps.staffid = sf.id;
                    rr.status = pp.UpdateProjectstaff(ps);
                    if (rr.status)
                    {
                        rr.msg = "修改成功";
                    }
                    else
                    {
                        rr.msg = "修改失败";
                    }
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除项目人员（修改状态为删除状态）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteProjectStaff()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            var me = getme();
            projectmanage_project pp = new projectmanage_project();
            var ps = pp.QueryProjectStaffByIdCompanyid(id, me.companyid);
            if (ps == null)
            {
                rr.status = false;
                rr.msg = "未找到该项目人员";
            }
            else
            {
                ps.status = -1;
                rr.status = pp.UpdateProjectstaff(ps);
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
        /// 项目人员列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryProjectStaffList(DataTableParameter param)
        {
            var me = getme();
            long pid = Request.QueryString["pid"].ToLong();
            projectmanage_project pp = new projectmanage_project();
            var lstvps = pp.QueryViewProjectStaffList(pid, me.companyid);
            int total = lstvps.Count;
            lstvps = lstvps.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstvps
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 项目bug列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult QueryProjectBugList(DataTableParameter param)
        {
            var me = getme();
            long pid = Request.QueryString["pid"].ToLong();
            projectmanage_bug pb = new projectmanage_bug();
            var lstvpb = pb.QueryViewProjectBugList(pid, me.companyid);
            int total = lstvpb.Count;
            lstvpb = lstvpb.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstvpb
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据bugid和公司id查询bug信息
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryProjectBug()
        {
            var me = getme();
            long id = Request.Form["id"].ToLong();
            projectmanage_bug pb = new projectmanage_bug();
            var vpb = pb.QueryViewProjectBug(id, me.companyid);
            return Json(vpb, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑项目bug
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProjectBug()
        {
            int degree = Request.Form["degree"].ToInt();
            string editor = Request.Form["editor"];
            long pid = Request.Form["pid"].ToLong();
            long id = Request.Form["id"].ToLong();
            string reproducestep = Request.Form["reproducestep"];
            string title = Request.Form["title"];
            int type = Request.Form["type"].ToInt();
            returnresult rr = new returnresult();
            var me = getme();
            membermanage_staff ms = new membermanage_staff();
            var sf = ms.QueryStaffByUsername(editor, me.companyid);
            if (sf == null)
            {
                rr.status = false;
                rr.msg = "未找到指派人";
            }
            else
            {
                projectmanage_bug pb = new projectmanage_bug();
                DateTime dt = DateTime.Now;
                //新增项目bug
                if (pid > 0)
                {
                    var pj = pb.QueryProject(pid, me.companyid);
                    if (pj == null)
                    {
                        rr.status = false;
                        rr.msg = "未找到该项目";
                    }
                    else
                    {
                        oa_project_projectsbugs pbs = new oa_project_projectsbugs();
                        pbs.addtime = dt;
                        pbs.confirmtime = "".ToDatetime();//确认时间
                        pbs.degree = degree;
                        pbs.dostep = dt + ",由" + me.username + "创建。\r\n";
                        pbs.editor = sf.id;
                        pbs.closetime = "".ToDatetime();//关闭时间
                        pbs.projectid = pid;
                        pbs.reproducestep = reproducestep;
                        pbs.solvetime = "".ToDatetime();//解决时间
                        pbs.status = 0;
                        pbs.title = title;
                        pbs.type = type;
                        pbs.writer = me.id;
                        rr = pb.AddProjectBug(pbs);
                    }
                }
                //修改项目bug
                else if (id > 0)
                {
                    var pbs = pb.QueryProjectBug(id, me.companyid);
                    if (pbs == null)
                    {
                        rr.status = false;
                        rr.msg = "未找到该bug";
                    }
                    else
                    {
                        pbs.degree = degree;
                        pbs.editor = sf.id;
                        pbs.reproducestep = reproducestep;
                        pbs.title = title;
                        pbs.type = type;
                        string dostep = dt + ",由" + me.username + "修改。\r";
                        if (pbs.degree != degree)
                        {
                            dostep += string.Format("修改了 {0} ，旧值为 \"{1}\"，新值为 \"{2}\"。", "级别", pbs.degree, degree);
                        }
                        if (pbs.editor != sf.id)
                        {
                            dostep += string.Format("修改了 {0} ，旧值为 \"{1}\"，新值为 \"{2}\"。", "指派人", pbs.editor, sf.id);
                        } 
                        if (pbs.reproducestep != reproducestep)
                        {
                            dostep += string.Format("修改了 {0} ，旧值为 \"{1}\"，新值为 \"{2}\"。", "重现步骤", "*", "*");
                        } 
                        if (pbs.title != title)
                        {
                            dostep += string.Format("修改了 {0} ，旧值为 \"{1}\"，新值为 \"{2}\"。", "标题", pbs.title, title);
                        } 
                        if (pbs.type != type)
                        {
                            dostep += string.Format("修改了 {0} ，旧值为 \"{1}\"，新值为 \"{2}\"。", "类型", pbs.type, type);
                        }
                        pbs.dostep += dostep+"\n";
                        rr = pb.UpdateProjectBug(pbs);
                    }
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 修改项目bug状态
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateProjectBugStatus()
        {
            string type = Request.Form["type"];
            long bugid = Request.Form["bid"].ToLong();
            returnresult rr = new returnresult();
            var me = getme();
            projectmanage_bug pb = new projectmanage_bug();
            var bug = pb.QueryProjectBug(bugid, me.companyid);
            if (bug == null)
            {
                rr.status = false;
                rr.msg = "未找到该BUG";
            }
            else
            {
                switch (type)
                {
                    case "confirm":
                        bug.status = 1;
                        break;
                    case "resolve":
                        bug.status = 2;
                        break;
                    case "close":
                        bug.status = 3;
                        break;
                    default:
                        bug.status = -1;
                        break;
                }
                if (bug.status == -1)
                {
                    rr.status = false;
                    rr.msg = "修改状态有误";
                }
                else
                {
                    rr = pb.UpdateProjectBug(bug);
                }
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 指派bug修改人
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProjectBugEditor()
        {
            returnresult rr = new returnresult();
            long id = Request.Form["id"].ToLong();
            long editor = Request.Form["editor"].ToLong();
            string remark = Request.Form["remark"];
            var me = getme();
            projectmanage_bug pb = new projectmanage_bug();
            var bug = pb.QueryProjectBug(id,me.companyid);
            if (bug == null)
            {
                rr.status = false;
                rr.msg = "未找到该BUG";
            }
            else
            {
                membermanage_staff ms = new membermanage_staff();
                var sf = ms.QueryStaffByIdCompanyid(editor, me.companyid);
                if (sf == null)
                {
                    rr.status = false;
                    rr.msg = "未找到该员工";
                }
                else
                {
                    bug.editor = editor;
                    bug.dostep += DateTime.Now+"，由"+me.username+"指派给"+sf.username+"\n";
                    bug.dostep += remark + "\n";
                    rr = pb.UpdateProjectBug(bug);
                }
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }
    }
}