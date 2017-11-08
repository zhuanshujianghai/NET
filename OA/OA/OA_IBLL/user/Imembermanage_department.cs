using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_Models;

namespace OA_IBLL.user
{
    /// <summary>
    /// 部门接口
    /// </summary>
    public interface Imembermanage_department
    {
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="dt">部门表实体</param>
        /// <returns>布尔值</returns>
        returnresult AddDepartment(oa_member_departments dt);

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="dt">部门表实体</param>
        /// <returns>布尔值</returns>
        returnresult UpdateDepartment(oa_member_departments dt);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="dt">部门表实体</param>
        /// <returns>布尔值</returns>
        returnresult DeleteDepartment(oa_member_departments dt);

        /// <summary>
        /// 根据部门id查询部门信息
        /// </summary>
        /// <param name="departmentid">部门id</param>
        /// <returns></returns>
        oa_member_departments QueryDepartmentByDepartmentid(long departmentid,long companyid);

        /// <summary>
        /// 根据部门名称和公司id查询部门信息
        /// </summary>
        /// <param name="departmentname">部门名称</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_member_departments QueryDepartmentByDepartmentname(string departmentname, long companyid);

        /// <summary>
        /// 根据部门id查询员工信息
        /// </summary>
        /// <param name="departmentid">部门id</param>
        /// <returns></returns>
        List<oa_member_staffs> QueryStaffListByDepartmentid(long departmentid);
    }
}
