using OA_IBLL.admin;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OA_BLL.admin
{
    /// <summary>
    /// 员工类
    /// </summary>
    public class admin_membermanage_staff : BaseAdminService, Iadmin_membermanage_staff
    {
        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="sf"></param>
        /// <returns></returns>
        public returnresult AddStaff(oa_member_staffs sf)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_staffs>(a => a.username == sf.username))
            {
                rr.status = false;
                rr.msg = "用户名已存在";
            }
            else
            {
                var rsf = AddReturnEntity<oa_member_staffs>(sf);
                if (rsf.id>0)
                {
                    oa_member_staffsinfos si = new oa_member_staffsinfos();
                    si.staffid = rsf.id;
                    si.address = "";
                    si.idcard = "";
                    si.realname = "";
                    rr.status = Add<oa_member_staffsinfos>(si);
                    if (rr.status)
                    {
                        rr.msg = "新增员工成功";
                    }
                    else
                    {
                        Delete<oa_member_staffs>(rsf);
                        rr.msg = "新增员工信息失败";
                    }
                }
                else
                {
                    rr.msg = "新增员工失败";
                }
            }
            return rr;
        }
        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="sf">员工表实体</param>
        /// <returns>结果实体</returns>
        public returnresult UpdateStaff(oa_member_staffs sf)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_staffs>(a => a.username == sf.username && a.id != sf.id))
            {
                rr.status = false;
                rr.msg = "用户名已存在";
            }
            else
            {
                if (Exist<oa_member_staffs>(a => a.email == sf.email && a.id != sf.id))
                {
                    rr.status = false;
                    rr.msg = "邮箱已存在";
                }
                else
                {
                    if (Exist<oa_member_staffs>(a => a.phone == sf.phone && a.id != sf.id))
                    {
                        rr.status = false;
                        rr.msg = "手机已存在";
                    }
                    else
                    {
                        if (Exist<oa_member_departments>(a => a.id == sf.departmentid))
                        {
                            oa_member_staffs sfs = Query<oa_member_staffs>(a=>a.id==sf.id);
                            oa_member_departments dt = Query<oa_member_departments>(a=>a.id==sfs.departmentid);
                            oa_member_departments dtt = Query<oa_member_departments>(a=>a.id == sf.departmentid);
                            if (dt.companyid == dtt.companyid)
                            {
                                rr.status = Update<oa_member_staffs>(sf);
                                if (rr.status)
                                {
                                    rr.msg = "修改成功";
                                }
                                else
                                {
                                    rr.msg = "修改失败";
                                }
                            }
                            else
                            {
                                rr.status = false;
                                rr.msg = "警告！部门不能跳出公司！";
                            }
                        }
                        else
                        {
                            rr.status = false;
                            rr.msg = "该部门不存在";
                        }
                    }
                }
            }
            return rr;
        }

        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="sf">员工表实体</param>
        /// <returns>布尔值</returns>
        public bool DeleteStaff(oa_member_staffs sf)
        {
            return Delete<oa_member_staffs>(sf);
        }

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="vsf"></param>
        /// <returns></returns>
        public returnresult UpdateViewStaffInfo(view_oa_member_staffs_staffsinfos vsf)
        { 
            returnresult rr = new returnresult();
            if (Exist<view_oa_member_staffs_staffsinfos>(a => a.username == vsf.username && a.id != vsf.id))
            {
                rr.status = false;
                rr.msg = "用户名已存在";
            }
            else
            {
                if (Exist<view_oa_member_staffs_staffsinfos>(a => a.email == vsf.email && a.id != vsf.id))
                {
                    rr.status = false;
                    rr.msg = "邮箱已存在";
                }
                else
                {
                    if (Exist<view_oa_member_staffs_staffsinfos>(a => a.phone == vsf.phone && a.id != vsf.id))
                    {
                        rr.status = false;
                        rr.msg = "手机已存在";
                    }
                    else
                    {
                        //修改oa_member_staffs
                        bool temp1 = false;
                        var sf = Query<oa_member_staffs>(a=>a.id==vsf.id);
                        sf.username = vsf.username;
                        sf.email = vsf.email;
                        sf.phone = vsf.phone;
                        temp1 = Update<oa_member_staffs>(sf);
                        //修改oa_member_staffsinfos
                        bool temp2 = false;
                        var si = Query<oa_member_staffsinfos>(a=>a.staffid==vsf.id);
                        if (si == null)
                        {
                            si = new oa_member_staffsinfos();
                        }
                        si.realname = vsf.realname;
                        si.sex = vsf.sex;
                        si.age = vsf.age;
                        si.idcard = vsf.idcard;
                        si.address = vsf.address;
                        if (si.id == 0)
                        {
                            si.staffid = vsf.id;
                            temp2 = Add<oa_member_staffsinfos>(si);
                        }
                        else
                        {
                            temp2 = Update<oa_member_staffsinfos>(si);
                        }
                        if (temp1 && temp2)
                        {
                            rr.status = true;
                            rr.msg = "修改成功";
                        }
                        else if (temp1 || temp2)
                        {
                            rr.status = false;
                            rr.msg = "修改半成功";
                        }
                        else
                        {
                            rr.status = false;
                            rr.msg = "修改失败";
                        }
                    }
                }
            }
            return rr;
        }

        /// <summary>
        /// 根据员工id查询员工信息视图
        /// </summary>
        /// <param name="staffid">员工id</param>
        /// <returns></returns>
        public view_oa_member_staffs_staffsinfos QueryStaffView(long staffid)
        {
            return Query<view_oa_member_staffs_staffsinfos>(a=>a.id==staffid);
        }

        /// <summary>
        /// 根据员工id查询员工信息
        /// </summary>
        /// <param name="staffid"></param>
        /// <returns></returns>
        public oa_member_staffs QueryStaffByIdCompanyid(long staffid,long companyid)
        {
            return Query<oa_member_staffs>(a=>a.id==staffid && a.companyid == companyid);
        }

        /// <summary>
        /// 根据员工id查询员工信息
        /// </summary>
        /// <param name="staffid"></param>
        /// <returns></returns>
        public oa_member_staffs QueryStaff(long staffid)
        {
            return Query<oa_member_staffs>(a => a.id == staffid);
        }

        /// <summary>
        /// 根据用户名查询员工信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public oa_member_staffs QueryStaffByUsername(string username,long companyid)
        {
            return Query<oa_member_staffs>(a => a.username == username && a.companyid == companyid);
        }

        /// <summary>
        /// 查询员工展示视图列表
        /// </summary>
        /// <returns></returns>
        public List<view_oa_member_staffinfolist> QueryViewStaffInfoList()
        {
            return QueryList<view_oa_member_staffinfolist>(a=>a.id>0).OrderByDescending(a=>a.id).ToList();
        }

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="si"></param>
        /// <returns></returns>
        public returnresult UpdateStaffInfo(oa_member_staffsinfos si)
        {
            returnresult rr = new returnresult();
            if (si.realname.Length > 4)
            {
                rr.status = false;
                rr.msg = "真实姓名太长了";
            }
            else
            {
                if (si.sex != 0 && si.sex != 1 && si.sex != -1)
                {
                    rr.status = false;
                    rr.msg = "性别有误";
                }
                else
                {
                    if (si.age > 100 || si.age < 0)
                    {
                        rr.status = false;
                        rr.msg = "年龄有误";
                    }
                    else
                    {
                        if ((!Regex.IsMatch(si.idcard, @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase)))
                        {
                            rr.status = false;
                            rr.msg = "身份证号码有误";
                        }
                        else
                        {
                            rr.status = Update<oa_member_staffsinfos>(si);
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
            }
            return rr;
        }

        /// <summary>
        /// 根据员工id查询员工信息
        /// </summary>
        /// <param name="staffid">员工id</param>
        /// <returns></returns>
        public oa_member_staffsinfos QueryStaffInfoByStaffid(long staffid)
        {
            return Query<oa_member_staffsinfos>(a=>a.staffid == staffid);
        }

        /// <summary>
        /// 根据员工id查询部门信息
        /// </summary>
        /// <param name="staffid">员工id</param>
        /// <returns></returns>
        public oa_member_departments QueryDepartmentByStaffid(long staffid)
        {
            var sf = Query<oa_member_staffs>(a=>a.id==staffid);
            return Query<oa_member_departments>(a=>a.id==sf.departmentid);
        }

        /// <summary>
        /// 根据公司id查询员工列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_member_staffsidusername> QueryStaffOnlyIdUsername(long companyid)
        {
            return QueryList<view_oa_member_staffsidusername>(a=>a.companyid==companyid);
        }

        /// <summary>
        /// 新增职位
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public returnresult AddPosition(oa_member_positions pt)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_positions>(a => a.positionname == pt.positionname && a.companyid == pt.companyid))
            {
                rr.status = false;
                rr.msg = "职位已存在";
            }
            else
            {
                rr.status = Add<oa_member_positions>(pt);
                if (rr.status)
                {
                    rr.msg = "新增职位成功";
                }
                else
                {
                    rr.msg = "新增职位失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 修改职位
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public returnresult UpdatePosition(oa_member_positions pt)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_positions>(a => a.positionname == pt.positionname && a.companyid == pt.companyid && a.id!=pt.id))
            {
                rr.status = false;
                rr.msg = "职位已存在";
            }
            else
            {
                rr.status = Update<oa_member_positions>(pt);
                if (rr.status)
                {
                    rr.msg = "修改职位成功";
                }
                else
                {
                    rr.msg = "修改职位失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public returnresult DeletePosition(oa_member_positions pt)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_positions>(a => a.parentid == pt.id))
            {
                rr.status = false;
                rr.msg = "该职位下还有职位";
            }
            else
            {
                if (Exist<oa_member_staffspositions>(a => a.positionid == pt.id))
                {
                    rr.status = false;
                    rr.msg = "该职位下还有员工";
                }
                else
                {
                    rr.status = Delete<oa_member_positions>(pt);
                    if (rr.status)
                    {
                        rr.msg = "删除职位成功";
                    }
                    else
                    {
                        rr.msg = "删除职位失败";
                    }
                }
            }
            return rr;
        }

        /// <summary>
        /// 根据公司id查询职位列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<oa_member_positions> QueryPositionList(long companyid)
        {
            return QueryList<oa_member_positions>(a=>a.companyid==companyid);
        }
        /// <summary>
        /// 根据职位id查询职位信息
        /// </summary>
        /// <param name="positionid">职位id</param>
        /// <returns></returns>
        public oa_member_positions QueryPositionByPositionid(long positionid)
        {
            return Query<oa_member_positions>(a=>a.id==positionid);
        }

        /// <summary>
        /// 根据公司id查询职位视图列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_member_position> QueryViewPositionList(long companyid)
        {
            return QueryList<view_oa_member_position>(a=>a.companyid == companyid);
        }

        /// <summary>
        /// 新增员工职位
        /// </summary>
        /// <param name="sp">员工职位表实体</param>
        /// <returns></returns>
        public bool AddStaffPosition(oa_member_staffspositions sp)
        {
            return Add<oa_member_staffspositions>(sp);
        }

        /// <summary>
        /// 修改员工职位
        /// </summary>
        /// <param name="sp">员工职位表实体</param>
        /// <returns></returns>
        public bool UpdateStaffPosition(oa_member_staffspositions sp)
        {
            return Update<oa_member_staffspositions>(sp);
        }

        /// <summary>
        /// 删除员工职位
        /// </summary>
        /// <param name="sp">员工职位表实体</param>
        /// <returns></returns>
        public bool DeleteStaffPositon(oa_member_staffspositions sp)
        {
            return Delete<oa_member_staffspositions>(sp);
        }

        /// <summary>
        /// 根据id查询员工职位信息
        /// </summary>
        /// <param name="id">员工职位id</param>
        /// <returns></returns>
        public oa_member_staffspositions QueryStaffPosition(long id)
        {
            return Query<oa_member_staffspositions>(a=>a.id==id);
        }

        /// <summary>
        /// 根据员工职位id查询员工职位信息
        /// </summary>
        /// <param name="id">员工职位id</param>
        /// <returns></returns>
        public view_oa_member_staffposition QueryStaffPositionView(long id)
        {
            return Query<view_oa_member_staffposition>(a=>a.id==id);
        }

        /// <summary>
        /// 根据公司id查询员工职位列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_member_staffposition> QueryStaffPositionListView(long companyid)
        {
            return QueryList<view_oa_member_staffposition>(a=>a.companyid==companyid).OrderBy(a=>a.level).ToList();
        }
    }
}
