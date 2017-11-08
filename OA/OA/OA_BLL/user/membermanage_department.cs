using OA_IBLL.user;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.user
{
    public class membermanage_department : BaseUserService, Imembermanage_department
    {
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="dt">部门表实体</param>
        /// <returns>布尔值</returns>
        public returnresult AddDepartment(oa_member_departments dt)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_departments>(a => a.departmentname == dt.departmentname && a.companyid == dt.companyid))
            {
                rr.status = false;
                rr.msg = "部门名称已存在";
            }
            else
            {
                rr.status = Add<oa_member_departments>(dt);
                if (rr.status)
                {
                    rr.msg = "新增部门成功";
                }
                else
                {
                    rr.msg = "新增部门失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 新增部门并修改操作者部门信息
        /// </summary>
        /// <param name="dt">部门表实体</param>
        /// <returns>布尔值</returns>
        public returnresult AddDepartmentAndUpdate(oa_member_departments dt,long staffid)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_departments>(a => a.departmentname == dt.departmentname && a.companyid == dt.companyid))
            {
                rr.status = false;
                rr.msg = "部门名称已存在";
            }
            else
            {
                rr.status = Add<oa_member_departments>(dt);
                if (rr.status)
                {
                    rr.msg = "新增部门成功";
                    var staff = Query<oa_member_staffs>(a=>a.id==staffid);
                    if (staff.departmentid == 0)
                    {
                        var department = Query<oa_member_departments>(a => a.departmentname == dt.departmentname && a.companyid == staff.companyid);
                        staff.departmentid = department.id;
                        var temp = Update<oa_member_staffs>(staff);
                        if (temp)
                        {
                            rr.msg += "修改成功";
                        }
                        else
                        {
                            rr.msg += "修改失败";
                        }
                    }
                }
                else
                {
                    rr.msg = "新增部门失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="dt">部门表实体</param>
        /// <returns>布尔值</returns>
        public returnresult UpdateDepartment(oa_member_departments dt)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_departments>(a => a.departmentname == dt.departmentname && a.id != dt.id))
            {
                rr.status = false;
                rr.msg = "部门名称已存在";
            }
            else
            {
                rr.status = Update<oa_member_departments>(dt);
                if (rr.status)
                {
                    rr.msg = "修改部门成功";
                }
                else
                {
                    rr.msg = "修改部门失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="dt">部门表实体</param>
        /// <returns>布尔值</returns>
        public returnresult DeleteDepartment(oa_member_departments dt)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_staffs>(a => a.departmentid == dt.id))
            {
                rr.status = false;
                rr.msg = "该部门下还有员工";
            }
            else
            {
                rr.status = Delete<oa_member_departments>(dt);
                if (rr.status)
                {
                    rr.msg = "删除部门成功";
                }
                else
                {
                    rr.msg = "删除部门失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 根据部门id查询部门信息
        /// </summary>
        /// <param name="departmentid">部门id</param>
        /// <returns></returns>
        public oa_member_departments QueryDepartmentByDepartmentid(long departmentid,long companyid)
        {
            return Query<oa_member_departments>(a=>a.id==departmentid && a.companyid==companyid);
        }

        /// <summary>
        /// 根据部门名称和公司id查询部门信息
        /// </summary>
        /// <param name="departmentname">部门名称</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_member_departments QueryDepartmentByDepartmentname(string departmentname, long companyid)
        {
            return Query<oa_member_departments>(a=>a.departmentname==departmentname && a.companyid == companyid);
        }

        /// <summary>
        /// 根据部门id查询员工列表
        /// </summary>
        /// <param name="departmentid">部门id</param>
        /// <returns></returns>
        public List<oa_member_staffs> QueryStaffListByDepartmentid(long departmentid)
        {
            return QueryList<oa_member_staffs>(a=>a.departmentid==departmentid);
        }
    }
}
