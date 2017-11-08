using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;
using OA_BLL.admin;
using OA_Models;

namespace OA_Web.Controllers.admin
{
    public class Admin_MemberController : Admin_BaseController
    {
        /// <summary>
        /// 公司列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CompanyList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_CompanyList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }
        /// <summary>
        /// 编辑公司
        /// </summary>
        /// <returns></returns>
        public ActionResult EditCompany()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_EditCompany.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 公司信息
            long companyid = Request.QueryString["id"].ToLong();
            admin_membermanage_company cp = new admin_membermanage_company();
            var company = cp.Querycompanybycompanyid(companyid);
            if (company == null)
            {
                company = new oa_member_companys();
                company.companyname = "";
                company.companyinfo = "";
                company.id = 0;
            }
            contentstr = contentstr.Replace("$companyid$",companyid+"");
            contentstr = contentstr.Replace("$companyname$", company.companyname);
            contentstr = contentstr.Replace("$companyinfo$", company.companyinfo);
            contentstr = contentstr.Replace("$departmentnumber$", cp.QueryDepartmentByCompanyid(company.id).Count + "");
            contentstr = contentstr.Replace("$staffnumber$", cp.QueryStaffByCompanyid(company.id).Count + "");
            #endregion
            return Content(contentstr);
        }
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_DepartmentList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }
        /// <summary>
        /// 部门详情
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentInfo()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_DepartmentInfo.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 部门信息
            long departmentid = Request.QueryString["departmentid"].ToLong();
            admin_membermanage_department cp = new admin_membermanage_department();
            oa_member_departments department = new oa_member_departments();
            long companyid = Request.QueryString["companyid"].ToLong();
            if (departmentid == 0)
            {
                department = cp.QueryDepartmentByDepartmentid(departmentid);
            }
            else
            {
                department = cp.QueryDepartmentByDepartmentid(departmentid);
            }
            if (department == null)
            {
                department = new oa_member_departments();
                department.departmentname = "";
                department.departmentinfo = "";
                department.companyid = companyid;
                department.status = 1;
                department.id = -1;
            }
            contentstr = contentstr.Replace("$departmentname$", department.departmentname);
            contentstr = contentstr.Replace("$departmentinfo$", department.departmentinfo);
            contentstr = contentstr.Replace("$staffnumber$", cp.QueryStaffListByDepartmentid(department.id).Count + "");
            #endregion
            return Content(contentstr);
        }
        /// <summary>
        /// 员工列表
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_StaffList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 员工列表
            long departmentid = Request.QueryString["departmentid"].ToLong();
            contentstr = contentstr.Replace("$departmentid$", departmentid + "");
            #endregion
            return Content(contentstr);
        }
        /// <summary>
        /// 员工信息
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffInfo()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_Staff.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 员工信息
            long staffid = Request.QueryString["staffid"].ToLong();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            var vsf = ms.QueryStaffView(staffid);
            contentstr = contentstr.Replace("$staffid$", staffid + "");
            contentstr = contentstr.Replace("$username$", vsf.username);
            contentstr = contentstr.Replace("$email$", vsf.email);
            contentstr = contentstr.Replace("$phone$", vsf.phone);
            contentstr = contentstr.Replace("$realname$", vsf.realname);
            contentstr = contentstr.Replace("$sex$", vsf.sex + "");
            contentstr = contentstr.Replace("$age$", vsf.age + "");
            contentstr = contentstr.Replace("$idcard$", vsf.idcard);
            contentstr = contentstr.Replace("$address$", vsf.address);
            #endregion
            return Content(contentstr);
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <returns></returns>
        public ActionResult AddStaff()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_AddStaff.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑员工信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditStaffInfo()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_EditStaffInfo.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 员工信息
            long staffid = Request.QueryString["staffid"].ToLong();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            var si = ms.QueryStaffInfoByStaffid(staffid);
            if (si == null)
            {
                contentstr = contentstr.Replace("$realname$", "");
                contentstr = contentstr.Replace("$sex$", "");
                contentstr = contentstr.Replace("$age$", "");
                contentstr = contentstr.Replace("$idcard$", "");
                contentstr = contentstr.Replace("$address$", "");
            }
            else
            {
                contentstr = contentstr.Replace("$realname$", si.realname);
                contentstr = contentstr.Replace("$sex$", si.sex + "");
                contentstr = contentstr.Replace("$age$", si.age + "");
                contentstr = contentstr.Replace("$idcard$", si.idcard);
                contentstr = contentstr.Replace("$address$", si.address);
            }
            #endregion
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑员工
        /// </summary>
        /// <returns></returns>
        public ActionResult EditStaff()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_EditStaff.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 员工信息
            long staffid = Request.QueryString["staffid"].ToLong();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            var dt = ms.QueryDepartmentByStaffid(staffid);
            contentstr = contentstr.Replace("$departmentname$", dt.departmentname);
            #endregion
            return Content(contentstr);
        }

        /// <summary>
        /// 职位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult PositionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_PositionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑职位
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPosition()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_EditPosition.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 职位信息
            long positionid = Request.QueryString["positionid"].ToLong();
            oa_member_positions pt = new oa_member_positions();
            if (positionid == 0)
            {
                pt.positionname = "";
                pt.positioninfo = "";
                pt.level = 0;
                pt.sort = 0;
            }
            else
            {
                admin_membermanage_staff ms = new admin_membermanage_staff();
                pt = ms.QueryPositionByPositionid(positionid);
                if (pt == null)
                {
                    pt.positionname = "";
                    pt.positioninfo = "";
                    pt.level = 0;
                    pt.sort = 0;
                }
            }
            contentstr = contentstr.Replace("$positionid$", positionid + "");
            contentstr = contentstr.Replace("$positionname$", pt.positionname);
            contentstr = contentstr.Replace("$positioninfo$", pt.positionname);
            contentstr = contentstr.Replace("$level$", pt.level + "");
            contentstr = contentstr.Replace("$sort$", pt.sort + "");
            #endregion
            return Content(contentstr);
        }

        /// <summary>
        /// 员工职位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffPositionList()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_StaffPositionList.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            return Content(contentstr);
        }

        /// <summary>
        /// 编辑员工职位
        /// </summary>
        /// <returns></returns>
        public ActionResult EditStaffPosition()
        {
            string contentstr = "";
            var str = Server.MapPath("/Template/admin/Admin_Member_EditStaffPosition.html");
            contentstr = System.IO.File.ReadAllText(str, System.Text.Encoding.UTF8);
            string url = Request.Url.LocalPath.ToString();
            contentstr = contentstr.Replace("$topdiv$", gettopdiv()).Replace("$leftdiv$", getleftdiv(url));
            #region 员工职位信息
            long id = Request.QueryString["id"].ToLong();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            //修改
            if (id > 0)
            {
                var spv = ms.QueryStaffPositionView(id);
                contentstr = contentstr.Replace("$username$", spv.username);
                contentstr = contentstr.Replace("$id$", spv.id + "");
            }
            //新增
            else
            {
                contentstr = contentstr.Replace("$username$", "");
                contentstr = contentstr.Replace("$id$", "0");
            }
            #endregion
            return Content(contentstr);
        }
	}
}