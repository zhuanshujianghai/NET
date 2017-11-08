using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_IBLL.admin
{
    /// <summary>
    /// 员工接口
    /// </summary>
    public interface Iadmin_membermanage_staff
    {
        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="sf"></param>
        /// <returns></returns>
        returnresult AddStaff(oa_member_staffs sf);
        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="sf">员工表实体</param>
        /// <returns>结果实体</returns>
        returnresult UpdateStaff(oa_member_staffs sf);

        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="sf">员工表实体</param>
        /// <returns>布尔值</returns>
        bool DeleteStaff(oa_member_staffs sf);

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="vsf">员工信息视图实体</param>
        /// <returns></returns>
        returnresult UpdateViewStaffInfo(view_oa_member_staffs_staffsinfos vsf);

        /// <summary>
        /// 根据员工id查询员工信息视图
        /// </summary>
        /// <param name="staffid">员工id</param>
        /// <returns></returns>
        view_oa_member_staffs_staffsinfos QueryStaffView(long staffid);

        /// <summary>
        /// 根据员工id和公司id查询员工信息
        /// </summary>
        /// <param name="staffid"></param>
        /// <returns></returns>
        oa_member_staffs QueryStaffByIdCompanyid(long staffid,long companyid);

        /// <summary>
        /// 根据公司id查询员工列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_member_staffsidusername> QueryStaffOnlyIdUsername(long companyid);

        /// <summary>
        /// 根据员工id查询员工信息
        /// </summary>
        /// <param name="staffid"></param>
        /// <returns></returns>
        oa_member_staffs QueryStaff(long staffid);

        /// <summary>
        /// 查询员工展示视图列表
        /// </summary>
        /// <returns></returns>
        List<view_oa_member_staffinfolist> QueryViewStaffInfoList();

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="si"></param>
        /// <returns></returns>
        returnresult UpdateStaffInfo(oa_member_staffsinfos si);

        /// <summary>
        /// 根据员工id查询员工信息
        /// </summary>
        /// <param name="staffid">员工id</param>
        /// <returns></returns>
        oa_member_staffsinfos QueryStaffInfoByStaffid(long staffid);

        /// <summary>
        /// 新增职位
        /// </summary>
        /// <param name="pt">员工表实体</param>
        /// <returns></returns>
        returnresult AddPosition(oa_member_positions pt);

        /// <summary>
        /// 修改职位
        /// </summary>
        /// <param name="pt">员工表实体</param>
        /// <returns></returns>
        returnresult UpdatePosition(oa_member_positions pt);

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="pt">职位表实体</param>
        /// <returns></returns>
        returnresult DeletePosition(oa_member_positions pt);

        /// <summary>
        /// 根据公司id查询职位列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<oa_member_positions> QueryPositionList(long companyid);

        /// <summary>
        /// 根据公司id查询职位视图列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_member_position> QueryViewPositionList(long companyid);

        /// <summary>
        /// 新增员工职位
        /// </summary>
        /// <param name="sp">员工职位表实体</param>
        /// <returns></returns>
        bool AddStaffPosition(oa_member_staffspositions sp);

        /// <summary>
        /// 修改员工职位
        /// </summary>
        /// <param name="sp">员工职位表实体</param>
        /// <returns></returns>
        bool UpdateStaffPosition(oa_member_staffspositions sp);

        /// <summary>
        /// 删除员工职位
        /// </summary>
        /// <param name="sp">员工职位表实体</param>
        /// <returns></returns>
        bool DeleteStaffPositon(oa_member_staffspositions sp);

        /// <summary>
        /// 根据公司id查询员工职位列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_member_staffposition> QueryStaffPositionListView(long companyid);
    }
}
