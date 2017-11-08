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
    public class Admin_MemberNoViewController : Admin_BaseController
    {
        /// <summary>
        /// 查询公司列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryCompanyList(DataTableParameter param)
        {
            admin_membermanage_company cp = new admin_membermanage_company();
            var lstdt = cp.QueryViewCompanyList();
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
        /// 查询公司列表-下拉列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryCompanyList_Select()
        {
            admin_membermanage_company cp = new admin_membermanage_company();
            var lstdt = cp.QueryCompanyList();
            return Json(lstdt, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 检测验证码是否正确
        /// </summary>
        /// <param name="code">用户输入</param>
        /// <returns>布尔值</returns>
        public bool CheckVerificationCode(string captcha)
        {
            if (TempData["captcha"] == null || TempData["captcha"].ToString() != captcha.ToLower())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 编辑公司信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditCompany()
        {
            long staffid = Request.Form["staffid"].ToLong();
            long companyid = Request.Form["companyid"].ToLong();
            returnresult rr = new returnresult();
            string companyname = Request.Form["companyname"];
            string companyinfo = Request.Form["companyinfo"];

            if (checknullorkong(companyname) || !checkhanzi(companyname) || !checklength(companyname, 1, 50))
            {
                rr.status = false;
                rr.msg = "公司名称有误";
            }
            else
            {
                if (checknull(companyinfo) || !checklength(companyinfo, 1, 100))
                {
                    rr.status = false;
                    rr.msg = "公司介绍有误";
                }
                else
                {
                    admin_membermanage_company mc = new admin_membermanage_company();
                    var cp = mc.Querycompanybycompanyid(companyid);
                    if (cp == null)
                    {
                        cp = new oa_member_companys();
                        cp.companyname = companyname;
                        cp.companyinfo = companyinfo;
                        cp.status = 1;
                        cp.addtime = DateTime.Now;
                        rr = mc.Addcompany(cp);
                    }
                    else
                    {
                        cp.companyname = companyname;
                        cp.companyinfo = companyinfo;
                        rr = mc.Updatecompany(cp);
                    }
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 查询部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryDepartmentList(DataTableParameter param)
        {
            long companyid = Request.Form["companyid"].ToLong();
            admin_membermanage_company cp = new admin_membermanage_company();
            var lstdt = cp.QueryViewDepartmentByCompanyid(companyid);
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
        /// 查询所有部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryAllDepartmentList()
        {
            long companyid = Request.Form["companyid"].ToLong();
            admin_membermanage_company cp = new admin_membermanage_company();
            var lstdt = cp.QueryDepartmentByCompanyid(companyid);
            return Json(lstdt, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <returns></returns>
        public ActionResult EditDepartment()
        {
            long staffid = Request.Form["staffid"].ToLong();
            long companyid = Request.Form["companyid"].ToLong();
            long departmentid = Request.Form["departmentid"].ToLong();
            returnresult rr = new returnresult();
            string departmentname = Request.Form["departmentname"];
            string departmentinfo = Request.Form["departmentinfo"];

            if (checknullorkong(departmentname) || !checklength(departmentname, 1, 50))
            {
                rr.status = false;
                rr.msg = "部门名称有误";
            }
            else
            {
                if (checknull(departmentinfo) || !checklength(departmentinfo, 1, 100))
                {
                    rr.status = false;
                    rr.msg = "部门介绍有误";
                }
                else
                {
                    admin_membermanage_department md = new admin_membermanage_department();
                    var dt = md.QueryDepartmentByDepartmentid(departmentid);
                    if (dt == null)
                    {
                        dt = new oa_member_departments();
                        dt.departmentname = departmentname;
                        dt.departmentinfo = departmentinfo;
                        dt.companyid = companyid;
                        dt.status = 1;
                        dt.addtime = DateTime.Now;
                        rr = md.AddDepartmentAndUpdate(dt, staffid);
                    }
                    else
                    {
                        dt.departmentname = departmentname;
                        dt.departmentinfo = departmentinfo;
                        rr = md.UpdateDepartment(dt);
                    }
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 员工列表
        /// </summary>
        /// <param name="param">datatables接收参数实体</param>
        /// <returns></returns>
        public ActionResult QueryViewStaffInfoList(DataTableParameter param)
        {
            long companyid = Request.Form["companyid"].ToLong();
            long departmentid = Request.QueryString["departmentid"].ToLong();
            List<view_oa_member_staffinfolist> lstsf = new List<view_oa_member_staffinfolist>();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            if (departmentid == 0)
            {
                lstsf = ms.QueryViewStaffInfoList().Where(a => a.companyid == companyid).ToList();
            }
            else
            {
                //此处不要写死，还有需求，是否能不限制看其他部门信息
                lstsf = ms.QueryViewStaffInfoList().Where(a => a.departmentid == departmentid).ToList();
            }
            int total = lstsf.Count;
            lstsf = lstsf.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstsf
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <returns></returns>
        public ActionResult AddStaff()
        {
            returnresult rr = new returnresult();
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            long companyid = Request.Form["companyid"].ToLong();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            oa_member_staffs sf = new oa_member_staffs();
            sf.username = username;
            sf.password = password;
            sf.companyid = companyid;
            sf.email = "";
            sf.addtime = DateTime.Now;
            sf.phone = "";
            sf.status = 0;
            sf.userword = "";
            rr = ms.AddStaff(sf);
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑员工视图信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditStaffView()
        {
            returnresult rr = new returnresult();
            long staffid = Request.Form["staffid"].ToLong();
            string username = Request.Form["username"];
            string email = Request.Form["email"];
            string phone = Request.Form["phone"];
            string realname = Request.Form["realname"];
            string sex = Request.Form["sex"];
            string age = Request.Form["age"];
            string idcard = Request.Form["idcard"];
            string address = Request.Form["address"];
            view_oa_member_staffs_staffsinfos vsf = new view_oa_member_staffs_staffsinfos();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            if (staffid == 0)
            {
                rr.status = false;
                rr.msg = "员工ID异常";
            }
            else
            {
                vsf = ms.QueryStaffView(staffid);
                vsf.username = username;
                vsf.email = email;
                vsf.phone = phone;
                vsf.realname = realname;
                vsf.sex = sex.ToInt();
                vsf.age = age.ToInt();
                vsf.idcard = idcard;
                vsf.address = address;
                rr = ms.UpdateViewStaffInfo(vsf);
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult EditStaffInfo(FormCollection collection)
        {
            returnresult rr = new returnresult();
            string realname = collection["realname"];
            int sex = collection["sex"].ToInt();
            int age = collection["age"].ToInt(18);
            string idcard = collection["idcard"];
            string address = collection["address"];
            long staffid = Request.Form["staffid"].ToLong();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            var si = ms.QueryStaffInfoByStaffid(staffid);
            si.realname = realname;
            si.sex = sex;
            si.age = age;
            si.idcard = idcard;
            si.address = address;
            rr = ms.UpdateStaffInfo(si);
            return Json(rr, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 查询员工列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryStaffsOnlyIdUsername()
        {
            long companyid = Request.Form["companyid"].ToLong();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            var lstsf = ms.QueryStaffOnlyIdUsername(companyid);
            return Json(lstsf, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 职位列表
        /// </summary>
        /// <param name="param">datatables接收参数实体</param>
        /// <returns></returns>
        public ActionResult QueryPositionList(DataTableParameter param)
        {
            long companyid = Request.Form["companyid"].ToLong();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            var lstpt = ms.QueryViewPositionList(companyid);
            int total = lstpt.Count;
            lstpt = lstpt.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstpt
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询所有职位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryAllPositionList()
        {
            long companyid = Request.Form["companyid"].ToLong();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            var lstpt = ms.QueryPositionList(companyid).OrderBy(a => a.level);
            return Json(lstpt, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 编辑职位
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPosition()
        {
            long staffid = Request.Form["staffid"].ToLong();
            long companyid = Request.Form["companyid"].ToLong();
            long positionid = Request.Form["positionid"].ToLong();
            string positionname = Request.Form["positionname"];
            string positioninfo = Request.Form["positioninfo"];
            long departmentid = Request.Form["departmentid"].ToLong();
            long parentid = Request.Form["parentid"].ToLong();
            int level = Request.Form["level"].ToInt();
            int sort = Request.Form["sort"].ToInt();
            returnresult rr = new returnresult();
            if (checknullorkong(positionname))
            {
                rr.status = false;
                rr.msg = "职位名称不能为空";
            }
            else
            {
                oa_member_positions pt = new oa_member_positions();
                admin_membermanage_staff ms = new admin_membermanage_staff();
                //新增
                if (positionid == 0)
                {
                    pt.positionname = positionname;
                    pt.positioninfo = positioninfo;
                    pt.departmentid = departmentid;
                    pt.parentid = parentid;
                    pt.level = level;
                    pt.sort = sort;
                    pt.companyid = companyid;
                    rr = ms.AddPosition(pt);
                }
                //修改
                else
                {
                    pt = ms.QueryPositionByPositionid(positionid);
                    pt.positionname = positionname;
                    pt.positioninfo = positioninfo;
                    pt.departmentid = departmentid;
                    pt.parentid = parentid;
                    pt.level = level;
                    pt.sort = sort;
                    pt.companyid = companyid;
                    rr = ms.UpdatePosition(pt);
                }
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <returns></returns>
        public ActionResult DeletePosition()
        {
            returnresult rr = new returnresult();
            long positionid = Request.Form["positionid"].ToLong();
            if (positionid > 0)
            {
                admin_membermanage_staff ms = new admin_membermanage_staff();
                var pt = ms.QueryPositionByPositionid(positionid);
                rr = ms.DeletePosition(pt);
            }
            else
            {
                rr.status = false;
                rr.msg = "职位ID异常";
            }
            return Json(rr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 员工职位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryStaffPositionList(DataTableParameter param)
        {
            long companyid = Request.Form["companyid"].ToLong();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            var lstspv = ms.QueryStaffPositionListView(companyid);
            int total = lstspv.Count;
            lstspv = lstspv.Take(param.iDisplayStart + param.iDisplayLength).Skip(param.iDisplayStart).ToList();
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = total,
                iTotalDisplayRecords = total,
                aaData = lstspv
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 编辑员工职位
        /// </summary>
        /// <returns></returns>
        public ActionResult EditStaffPosition()
        {
            returnresult rr = new returnresult();
            string username = Request.Form["username"];
            long positionid = Request.Form["positionid"].ToLong();
            long id = Request.Form["id"].ToLong();
            long companyid = Request.Form["companyid"].ToLong();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            oa_member_staffspositions sp = new oa_member_staffspositions();
            var sf = ms.QueryStaffByUsername(username, companyid);
            //修改
            if (id > 0)
            {
                sp = ms.QueryStaffPosition(id);
                if (sp == null)
                {
                    rr.status = false;
                    rr.msg = "未找到修改信息";
                }
                else
                {
                    if (sf == null)
                    {
                        rr.status = false;
                        rr.msg = "该员工不存在";
                    }
                    else
                    {
                        sp.staffid = sf.id;
                        sp.positionid = positionid;
                        rr.status = ms.UpdateStaffPosition(sp);
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
            }
            //新增
            else if (id == 0)
            {
                if (sf == null)
                {
                    rr.status = false;
                    rr.msg = "该员工不存在";
                }
                else
                {
                    sp.staffid = sf.id;
                    sp.positionid = positionid;
                    sp.companyid = companyid;
                    rr.status = ms.AddStaffPosition(sp);
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
            else
            {
                rr.status = false;
                rr.msg = "操作失败";
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 删除员工职位
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteStaffPosition()
        {
            long id = Request.Form["id"].ToLong();
            returnresult rr = new returnresult();
            admin_membermanage_staff ms = new admin_membermanage_staff();
            if (id > 0)
            {
                var sp = ms.QueryStaffPosition(id);
                if (sp == null)
                {
                    rr.status = false;
                    rr.msg = "未找到修改信息";
                }
                else
                {
                    rr.status = ms.DeleteStaffPositon(sp);
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
            else
            {
                rr.status = false;
                rr.msg = "删除失败";
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }
	}
}