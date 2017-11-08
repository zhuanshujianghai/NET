using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_BLL.user;

namespace OA_Web.Controllers.user
{
    //项目
    public class ProjectController : BaseController
    {
        /// <summary>
        /// 项目列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Project_ProjectList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑项目信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProject()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Project_EditProject.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 项目信息
            long pid = Request.QueryString["pid"].ToLong();
            var me = getme();
            projectmanage_project pp = new projectmanage_project();
            var pt = pp.QueryProjectById(pid,me.companyid);
            contentstr = contentstr.Replace("$pid$",pid+"");
            if (pt == null)
            {
                contentstr = contentstr.Replace("$projectname$", "");
                contentstr = contentstr.Replace("$projectinfo$", "");
                contentstr = contentstr.Replace("$staff$", "");
                contentstr = contentstr.Replace("$person_responsiblename$", "");
                contentstr = contentstr.Replace("$progress$", "");
                contentstr = contentstr.Replace("$status$", "0");
                contentstr = contentstr.Replace("$addtime$", "");
                contentstr = contentstr.Replace("$expectbegintime$", "");
                contentstr = contentstr.Replace("$expectfinishtime$", "");
            }
            else
            {
                contentstr = contentstr.Replace("$projectname$", pt.projectname);
                contentstr = contentstr.Replace("$projectinfo$", pt.projectinfo);
                membermanage_staff ms = new membermanage_staff();
                var staff = ms.QueryStaffByIdCompanyid(pt.staffid,me.companyid);
                contentstr = contentstr.Replace("$staff$", staff==null?"":staff.username+"");
                contentstr = contentstr.Replace("$progress$", pt.progress+"");
                contentstr = contentstr.Replace("$status$", pt.status+"");
                contentstr = contentstr.Replace("$addtime$", pt.addtime.ToShortDateString());
                contentstr = contentstr.Replace("$expectbegintime$", pt.expectbegintime.ToShortDateString());
                contentstr = contentstr.Replace("$expectfinishtime$", pt.expectfinishtime.ToShortDateString());
            }
            #endregion
            return Content(contentstr);
        }

        /// <summary>
        /// 项目人员列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectStaffList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Project_ProjectStaffList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long pid = Request.QueryString["pid"].ToLong();
            contentstr = contentstr.Replace("$pid$",pid+"");
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑项目人员
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProjectStaff()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Project_EditProjectStaff.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["id"].ToLong();
            long pid = Request.QueryString["pid"].ToLong();
            contentstr = contentstr.Replace("$id$",id+"");
            contentstr = contentstr.Replace("$pid$",pid+"");
            var me = getme();
            projectmanage_project pp = new projectmanage_project();
            var ps = pp.QueryProjectStaffByIdCompanyid(id,me.companyid);
            if (ps == null)
            {
                contentstr = contentstr.Replace("$rolename$", "");
                contentstr = contentstr.Replace("$username$", "");
            }
            else
            {
                membermanage_staff ms = new membermanage_staff();
                var sf = ms.QueryStaff(ps.staffid);
                contentstr = contentstr.Replace("$rolename$", ps.rolename);
                contentstr = contentstr.Replace("$username$", sf.username);
            }
            return Content(contentstr);
        }

        /// <summary>
        /// 项目bug列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectBugList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Project_ProjectBugList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long pid = Request.QueryString["pid"].ToLong();
            contentstr = contentstr.Replace("$pid$", pid + "");
            return Content(contentstr);
        }
        /// <summary>
        /// 查看项目bug信息
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryProjectBug()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Project_ProjectBug.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["id"].ToLong();
            contentstr = contentstr.Replace("$id$", id + "");
            return Content(contentstr);
        }
        /// <summary>
        /// 编辑项目bug
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProjectBug()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Project_EditProjectBug.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long pid = Request.QueryString["pid"].ToLong();
            long id = Request.QueryString["id"].ToLong();
            contentstr = contentstr.Replace("$pid$", pid + "");
            contentstr = contentstr.Replace("$id$", id + "");
            var me = getme();
            projectmanage_bug pb = new projectmanage_bug();
            var pbs = pb.QueryProjectBug(id,me.companyid);
            //新增
            if (pbs == null)
            {
                contentstr = contentstr.Replace("$title$","");
                contentstr = contentstr.Replace("$reproducestep$","");
                contentstr = contentstr.Replace("$type$", getoption<enum_projectsbugs_type>(0));
                contentstr = contentstr.Replace("$degree$", getoption<enum_projectsbugs_degree>(0));
                contentstr = contentstr.Replace("$editor$","");
                contentstr = contentstr.Replace("$status$", "新建");
                contentstr = contentstr.Replace("$addtime$","");
                contentstr = contentstr.Replace("$confirmtime$","");
                contentstr = contentstr.Replace("$solvetime$","");
                contentstr = contentstr.Replace("$closetime$","");
                contentstr = contentstr.Replace("$dostep$","");
            }
            //修改
            else
            {
                contentstr = contentstr.Replace("$title$", pbs.title);
                contentstr = contentstr.Replace("$reproducestep$", pbs.reproducestep);
                contentstr = contentstr.Replace("$type$", getoption<enum_projectsbugs_type>(pbs.type));
                contentstr = contentstr.Replace("$degree$", getoption<enum_projectsbugs_degree>(pbs.degree));
                contentstr = contentstr.Replace("$status$", getnamebyvalue<enum_projecstbugs_status>(pbs.status));
                membermanage_staff ms = new membermanage_staff();
                var sf = ms.QueryStaffByIdCompanyid(pbs.editor,me.companyid);
                contentstr = contentstr.Replace("$editor$", sf==null?"":sf.username);
                contentstr = contentstr.Replace("$dostep$", pbs.dostep);
            }            
            return Content(contentstr);
        }
        /// <summary>
        /// 指派bug修改人
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProjectBugEditor()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/user/Project_EditProjectBugEditor.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            long id = Request.QueryString["id"].ToLong();
            contentstr = contentstr.Replace("$id$", id + "");
            return Content(contentstr);
        }
	}
}