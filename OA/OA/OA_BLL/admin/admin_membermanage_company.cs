using OA_IBLL.admin;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.admin
{
    /// <summary>
    /// 公司类
    /// </summary>
    public class admin_membermanage_company : BaseAdminService, Iadmin_membermanage_company
    {
        /// <summary>
        /// 新增公司信息
        /// </summary>
        /// <param name="cp">公司表实体</param>
        /// <param name="staffid">员工id（将当前登陆操作员工的公司id改为新增的公司id）</param>
        /// <returns></returns>
        public returnresult Addcompany(oa_member_companys cp,long staffid)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_companys>(a => a.companyname == cp.companyname))
            {
                rr.status = false;
                rr.msg = "公司名称已存在";
            }
            else
            {
                rr.status = Add<oa_member_companys>(cp);
                if (rr.status)
                {
                    rr.msg = "新增成功";
                    var sf = Query<oa_member_staffs>(a=>a.id==staffid);
                    var com = Query<oa_member_companys>(a=>a.companyname==cp.companyname);
                    sf.companyid = com.id;
                    var temp = Update<oa_member_staffs>(sf);
                    if (temp)
                    {
                        rr.msg += "修改成功";
                    }
                    else
                    {
                        rr.msg += "修改失败";
                    }
                }
                else
                {
                    rr.msg = "新增失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 新增公司信息
        /// </summary>
        /// <param name="cp">公司表实体</param>
        /// <returns></returns>
        public returnresult Addcompany(oa_member_companys cp)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_companys>(a => a.companyname == cp.companyname))
            {
                rr.status = false;
                rr.msg = "公司名称已存在";
            }
            else
            {
                rr.status = Add<oa_member_companys>(cp);
                if (rr.status)
                {
                    rr.msg = "新增成功";
                }
                else
                {
                    rr.msg = "新增失败";
                }
            }
            return rr;
        }


        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="cp">公司表实体</param>
        /// <returns>布尔值</returns>
        public returnresult Updatecompany(oa_member_companys cp)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_member_companys>(a => a.id == cp.id))
            {
                if (Exist<oa_member_companys>(a => a.companyname == cp.companyname && a.id != cp.id))
                {
                    rr.status = false;
                    rr.msg = "公司名称已存在";
                }
                else
                {
                    rr.status = Update<oa_member_companys>(cp);
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
            else
            {
                rr.status = false;
                rr.msg = "公司不存在";
            }
            return rr;
        }

        /// <summary>
        /// 根据公司id查询公司信息
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_member_companys Querycompanybycompanyid(long companyid)
        {
            return Query<oa_member_companys>(a => a.id == companyid);
        }

        /// <summary>
        /// 查询公司列表
        /// </summary>
        /// <returns></returns>
        public List<oa_member_companys> QueryCompanyList()
        {
            return QueryList<oa_member_companys>(a=>a.status!=-1).OrderByDescending(a=>a.addtime).ToList();
        }

        /// <summary>
        /// 查询视图-公司列表
        /// </summary>
        /// <returns></returns>
        public List<view_oa_member_companylist> QueryViewCompanyList()
        {
            return QueryList<view_oa_member_companylist>(a => a.id > 0).OrderByDescending(a => a.addtime).ToList();
        }

        /// <summary>
        /// 根据部门id查询公司信息
        /// </summary>
        /// <param name="departmentid">部门id</param>
        /// <returns></returns>
        public oa_member_companys Querycompanybydepartmentid(long departmentid)
        {
            oa_member_departments dm = Query<oa_member_departments>(a => a.id == departmentid);
            return Query<oa_member_companys>(a => a.id == dm.companyid);
        }

        /// <summary>
        /// 根据员工id查询公司信息
        /// </summary>
        /// <param name="departmentid">员工id</param>
        /// <returns></returns>
        public oa_member_companys Querycompanybystaffid(long staffid)
        {
            oa_member_staffs sf = Query<oa_member_staffs>(a => a.id == staffid);
            return Query<oa_member_companys>(a => a.id == sf.companyid);
        }

        /// <summary>
        /// 根据公司id查询部门列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<oa_member_departments> QueryDepartmentByCompanyid(long companyid)
        {
            return QueryList<oa_member_departments>(a => a.companyid == companyid);
        }

        /// <summary>
        /// 根据公司id查询视图部门列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_member_departmentstaffnumber> QueryViewDepartmentByCompanyid(long companyid)
        {
            return QueryList<view_oa_member_departmentstaffnumber>(a => a.companyid == companyid);
        }

        /// <summary>
        /// 根据公司id查询员工列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<oa_member_staffs> QueryStaffByCompanyid(long companyid)
        {
            var dp = QueryList<oa_member_departments>(a => a.companyid == companyid).ToList();
            List<oa_member_staffs> lstsf = new List<oa_member_staffs>();
            foreach (var item in dp)
            {
                var sf = QueryList<oa_member_staffs>(a => a.departmentid == item.id);
                lstsf.AddRange(sf);
            }
            return lstsf;
        }
    }
}
