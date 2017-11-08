using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA_Models;

namespace OA_IBLL.user
{
    /// <summary>
    /// 公司接口
    /// </summary>
    public interface Imembermanage_company
    {
        /// <summary>
        /// 新增公司信息
        /// </summary>
        /// <param name="cp">公司表实体</param>
        /// <param name="staffid">员工id（将当前登陆操作员工的公司id改为新增的公司id）</param>
        /// <returns></returns>
        returnresult Addcompany(oa_member_companys cp, long staffid);
        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="cp">公司表实体</param>
        /// <returns>布尔值</returns>
        returnresult Updatecompany(oa_member_companys cp);
         /// <summary>
        /// 根据公司id查询公司信息
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_member_companys Querycompanybycompanyid(long companyid);

        /// <summary>
        /// 根据部门id查询公司信息
        /// </summary>
        /// <param name="departmentid">部门id</param>
        /// <returns></returns>
        oa_member_companys Querycompanybydepartmentid(long departmentid);

        /// <summary>
        /// 根据员工id查询公司信息
        /// </summary>
        /// <param name="departmentid">员工id</param>
        /// <returns></returns>
        oa_member_companys Querycompanybystaffid(long staffid);

        /// <summary>
        /// 根据公司id查询部门列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<oa_member_departments> QueryDepartmentByCompanyid(long companyid);

        /// <summary>
        /// 根据公司id查询员工列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<oa_member_staffs> QueryStaffByCompanyid(long companyid);
    }
}
