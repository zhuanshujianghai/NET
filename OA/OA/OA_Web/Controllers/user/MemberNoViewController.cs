using OA_BLL.user;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA_Common;

namespace OA_Web.Controllers.user
{
    public class MemberNoViewController : BaseController
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            returnresult rr = new returnresult();
            //用户名
            string username = Request.Form["username"];
            //密码
            string password = Request.Form["password"];
            //验证码
            string captcha = Request.Form["captcha"];
            if (CheckVerificationCode(captcha))
            {
                logintype lt = new logintype();
                lt.username = username;
                lt.password = password;
                membermanage_login lg = new membermanage_login();
                oa_member_staffs sf = lg.loginusername(lt);
                if (sf == null)
                {
                    rr.status = false;
                    rr.msg = "账户名和密码不匹配！";
                }
                else
                {
                    rr = lg.logincookie(sf);
                    if (rr.status)
                    {
                        HttpCookie hc = new HttpCookie("logincookie");
                        hc.Value = rr.msg;
                        hc.Expires = DateTime.Now.AddDays(30);
                        Response.AppendCookie(hc);

                        rr.status = true;
                        rr.msg = "登陆成功！";
                    }
                }
            }
            else
            {
                rr.status = false;
                rr.msg = "验证码有误！";
            }
            return Json(rr, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            var au = getme();
            HttpCookie cookie1 = Request.Cookies.Get("logincookie");
            string cookievalue = cookie1.Value;
            membermanage_login ml = new membermanage_login();
            int result = ml.adminoutlogin(cookievalue);

            HttpCookie cookie = new HttpCookie("logincookie");
            cookie.Value = "";
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.AppendCookie(cookie);
            return Redirect("/member/login");
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
            oa_member_staffs sf = getme();
            returnresult rr = new returnresult();
            string companyname = Request.Form["companyname"];
            string companyinfo = Request.Form["companyinfo"];

            if (checknullorkong(companyname) || !checkhanzi(companyname) || !checklength(companyname,1,50))
            {
                rr.status = false;
                rr.msg = "公司名称有误";
            }
            else
            {
                if (checknull(companyinfo) || !checklength(companyinfo,1,100))
                {
                    rr.status = false;
                    rr.msg = "公司介绍有误";
                }
                else
                {
                    membermanage_company mc = new membermanage_company();
                    var cp = mc.Querycompanybycompanyid(sf.companyid);
                    if (cp == null)
                    {
                        cp = new oa_member_companys();
                        cp.companyname = companyname;
                        cp.companyinfo = companyinfo;
                        cp.status = 1;
                        cp.addtime = DateTime.Now;
                        rr = mc.Addcompany(cp,getmeto().id);
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
            var me = getme();
            string departmentname = Request.QueryString["departmentname"];
            membermanage_company cp = new membermanage_company();
            var lstdt = cp.QueryViewDepartmentByCompanyid(me.companyid);
            if (departmentname != null)
            {
                lstdt = lstdt.Where(a => a.departmentname.Contains(departmentname)).ToList();
            }
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
            var me = getme();
            membermanage_company cp = new membermanage_company();
            var lstdt = cp.QueryDepartmentByCompanyid(me.companyid);
            return Json(lstdt,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <returns></returns>
        public ActionResult EditDepartment()
        {
            var me = getmeto();
            returnresult rr = new returnresult();
            string departmentname = Request.Form["departmentname"];
            string departmentinfo = Request.Form["departmentinfo"];
            long departmentid = Request.Form["departmentid"].ToLong();

            if (checknullorkong(departmentname) || !checklength(departmentname,1,50))
            {
                rr.status = false;
                rr.msg = "部门名称有误";
            }
            else
            {
                if (checknull(departmentinfo) || !checklength(departmentinfo,1,100))
                {
                    rr.status = false;
                    rr.msg = "部门介绍有误";
                }
                else
                {
                    membermanage_department md = new membermanage_department();
                    var dt = md.QueryDepartmentByDepartmentid(departmentid,me.companyid);
                    if (dt == null)
                    {
                        dt = new oa_member_departments();
                        dt.departmentname = departmentname;
                        dt.departmentinfo = departmentinfo;
                        dt.companyid = me.companyid;
                        dt.status = 1;
                        dt.addtime = DateTime.Now;
                        rr = md.AddDepartmentAndUpdate(dt,me.id);
                    }
                    else
                    {
                        dt.departmentname = departmentname;
                        dt.departmentinfo = departmentinfo;
                        rr = md.UpdateDepartment(dt);
                    }
                }
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 员工列表
        /// </summary>
        /// <param name="param">datatables接收参数实体</param>
        /// <returns></returns>
        public ActionResult QueryViewStaffInfoList(DataTableParameter param)
        {
            var me = getmeto();
            long departmentid = Request.QueryString["departmentid"].ToLong();
            string name = Request.QueryString["name"];
            List<view_oa_member_staffinfolist> lstsf = new List<view_oa_member_staffinfolist>();
            membermanage_staff ms = new membermanage_staff();
            if (departmentid == 0)
            {
                lstsf = ms.QueryViewStaffInfoList().Where(a => a.companyid == me.companyid).ToList();
            }
            else
            {
                //此处不要写死，还有需求，是否能不限制看其他部门信息
                lstsf = ms.QueryViewStaffInfoList().Where(a => a.departmentid == departmentid).ToList();
            }
            if (name != null)
            {
                lstsf = lstsf.Where(a => a.username.Contains(name) || a.realname.Contains(name)).ToList();
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
            long departmentid = Request.Form["departmentid"].ToLong();
            var me = getme();
            string dt = "-"+DateTime.Now.ToString("yyyyMMddHHmmss");
            membermanage_staff ms = new membermanage_staff();
            oa_member_staffs sf = new oa_member_staffs();
            sf.username = username;
            sf.password = password;
            sf.companyid = me.companyid;
            sf.email = dt;
            sf.addtime = DateTime.Now;
            sf.phone = dt;
            sf.status = 0;
            sf.userword = "";
            sf.departmentid = departmentid;
            sf.isachievement = 1;
            sf.isall = 0;
            rr = ms.AddStaff(sf);
            return Json(rr,JsonRequestBehavior.DenyGet);
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
            long departmentid = Request.Form["departmentid"].ToLong();
            membermanage_staff ms = new membermanage_staff();
            var me = getme();
            if (staffid == 0)
            {
                rr.status = false;
                rr.msg = "人员ID异常";
            }
            else
            {
                //修改所在部门


                var vsf = ms.QueryStaffView(staffid,me.companyid);
                if (vsf == null)
                {
                    rr.status = false;
                    rr.msg = "未找到该人员";
                }
                vsf.username = username;
                vsf.email = email;
                vsf.phone = phone;
                vsf.realname = realname;
                vsf.sex = sex.ToInt();
                vsf.age = age.ToInt();
                vsf.idcard = idcard;
                vsf.address = address;
                vsf.departmentid = departmentid;
                rr = ms.UpdateViewStaffInfo(vsf);
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
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
            var me = getme();
            membermanage_staff ms = new membermanage_staff();
            var si = ms.QueryStaffInfoByStaffid(me.id);
            si.realname = realname;
            si.sex = sex;
            si.age = age;
            si.idcard = idcard;
            si.address = address;
            rr = ms.UpdateStaffInfo(si);
            return Json(rr,JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 查询员工列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryStaffsOnlyIdUsername()
        {
            var me = getme();
            membermanage_staff ms = new membermanage_staff();
            var lstsf = ms.QueryStaffOnlyIdUsername(me.companyid);
            return Json(lstsf,JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 职位列表
        /// </summary>
        /// <param name="param">datatables接收参数实体</param>
        /// <returns></returns>
        public ActionResult QueryPositionList(DataTableParameter param)
        {
            var me = getme();
            membermanage_staff ms = new membermanage_staff();
            var lstpt = ms.QueryViewPositionList(me.companyid);
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
            var me = getme();
            membermanage_staff ms = new membermanage_staff();
            var lstpt = ms.QueryPositionList(me.companyid).OrderBy(a=>a.level);
            return Json(lstpt,JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 编辑职位
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPosition()
        {
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
                membermanage_staff ms = new membermanage_staff();
                //新增
                if (positionid == 0)
                {
                    pt.positionname = positionname;
                    pt.positioninfo = positioninfo;
                    pt.departmentid = departmentid;
                    pt.parentid = parentid;
                    pt.level = level;
                    pt.sort = sort;
                    pt.companyid = getme().companyid;
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
                    pt.companyid = getme().companyid;
                    rr = ms.UpdatePosition(pt);
                }
            }
            return Json(rr,JsonRequestBehavior.DenyGet);
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
                membermanage_staff ms = new membermanage_staff();
                var pt = ms.QueryPositionByPositionid(positionid);
                rr = ms.DeletePosition(pt);
            }
            else
            {
                rr.status = false;
                rr.msg = "职位ID异常";
            }
            return Json(rr,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 员工职位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryStaffPositionList(DataTableParameter param)
        {
            var me = getme();
            membermanage_staff ms = new membermanage_staff();
            var lstspv = ms.QueryStaffPositionListView(me.companyid);
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
            var me = getme();
            membermanage_staff ms = new membermanage_staff();
            oa_member_staffspositions sp = new oa_member_staffspositions();
            var sf = ms.QueryStaffByUsername(username,me.companyid);
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
                    sp.companyid = me.companyid;
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
            return Json(rr,JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 删除员工职位
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteStaffPosition()
        {
            long id = Request.Form["id"].ToLong();
            returnresult rr = new returnresult();
            membermanage_staff ms = new membermanage_staff();
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
            return Json(rr,JsonRequestBehavior.DenyGet);
        }
    }
}