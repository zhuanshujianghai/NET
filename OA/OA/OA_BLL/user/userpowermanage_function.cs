using OA_IBLL.user;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.user
{
    public class userpowermanage_function : BaseUserService, Iuserpowermanage_function
    {
        /// <summary>
        /// 修改功能
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public returnresult UpdateFunction(oa_userpower_functions pm)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_userpower_functions>(a => a.functionname == pm.functionname && a.id!=pm.id))
            {
                rr.status = false;
                rr.msg = "功能名称已存在";
            }
            else
            {
                rr.status = Update<oa_userpower_functions>(pm);
                if (rr.status)
                {
                    rr.msg = "修改成功";
                }
                else
                {
                    rr.msg = "修改失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 根据功能id查询功能信息
        /// </summary>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        public oa_userpower_functions QueryFunctionById(long functionid)
        {
            return Query<oa_userpower_functions>(a=>a.id==functionid);
        }

        /// <summary>
        /// 根据父级id查询功能信息列表
        /// </summary>
        /// <param name="pid">父级id</param>
        /// <returns></returns>
        public List<oa_userpower_functions> QueryFunctionByPid(long pid)
        {
            return QueryList<oa_userpower_functions>(a => a.parentid == pid);
        }

        /// <summary>
        /// 根据功能id和公司id查询功能信息
        /// </summary>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        public oa_userpower_functions QueryFunctionByIdCompanyid(long functionid)
        {
            return Query<oa_userpower_functions>(a=>a.id==functionid);
        }

        /// <summary>
        /// 根据公司id查询功能列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<oa_userpower_functions> QueryFunctionList()
        {
            return QueryList<oa_userpower_functions>(a=>a.id>0);
        }

        /// <summary>
        /// 修改ACTION
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public returnresult UpdateAction(oa_userpower_actions at)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_userpower_actions>(a => a.actionname == at.actionname && a.id != at.id))
            {
                rr.status = false;
                rr.msg = "ACTION名称已存在";
            }
            else
            {
                rr.status = Update<oa_userpower_actions>(at);
                if (rr.status)
                {
                    rr.msg = "修改成功";
                }
                else
                {
                    rr.msg = "修改失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 根据actionid查询action信息
        /// </summary>
        /// <param name="functionid">actionid</param>
        /// <returns></returns>
        public oa_userpower_actions QueryActionById(long actionid)
        {
            return Query<oa_userpower_actions>(a => a.id == actionid);
        }
        /// <summary>
        /// 根据actionid和公司id查询action信息
        /// </summary>
        /// <param name="actionid">actionid</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_userpower_actions QueryActionByIdCompanyid(long actionid)
        {
            return Query<oa_userpower_actions>(a=>a.id==actionid);
        }

        /// <summary>
        /// 根据公司id查询action列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<oa_userpower_actions> QueryActionList(long companyid)
        {
            return QueryList<oa_userpower_actions>(a => a.id>0);
        }

        /// <summary>
        /// 新增职位功能
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public bool AddPositionFunction(oa_userpower_positionsfunctions pf)
        {
            return Add<oa_userpower_positionsfunctions>(pf);
        }

        /// <summary>
        /// 修改职位功能
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public bool UpdatePositionFunction(oa_userpower_positionsfunctions at)
        {
            return Update<oa_userpower_positionsfunctions>(at);
        }

        /// <summary>
        /// 批量修改职位功能
        /// </summary>
        /// <param name="lstrf"></param>
        /// <returns></returns>
        public bool UpdatePositionFunctionList(List<oa_userpower_positionsfunctions> lstrf)
        {
            return UpdateIQueryable<oa_userpower_positionsfunctions>(lstrf);
        }

        /// <summary>
        /// 根据id查询职位功能信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public oa_userpower_positionsfunctions QueryPositionFunction(long id)
        {
            return Query<oa_userpower_positionsfunctions>(a=>a.id==id);
        }
        /// <summary>
        /// 根据职位id查询职位功能视图列表
        /// </summary>
        /// <param name="positionid">职位id</param>
        /// <returns></returns>
        public List<view_oa_member_positionfunctionlist> QueryViewPositionFunctionListByPositionid(long positionid)
        {
            return QueryList<view_oa_member_positionfunctionlist>(a => a.positionid == positionid);
        }

        /// <summary>
        /// 根据职位id查询职位功能列表
        /// </summary>
        /// <param name="positionid">职位id</param>
        /// <returns></returns>
        public List<oa_userpower_positionsfunctions> QueryPositionFunctionListByPositionid(long positionid)
        {
            return QueryList<oa_userpower_positionsfunctions>(a => a.positionid == positionid);
        }

        /// <summary>
        /// 根据角色id和功能id查询角色功能信息
        /// </summary>
        /// <param name="positionid">职位id</param>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        public oa_userpower_positionsfunctions QueryPositionFunctionByPositionIdFunctionId(long positionid, long functionid)
        {
            return Query<oa_userpower_positionsfunctions>(a => a.positionid == positionid && a.functionid == functionid);
        }

        /// <summary>
        /// 根据功能id查询拥有该功能的职位列表
        /// </summary>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        public List<oa_member_positions> QueryPositionListByFunctionid(long functionid)
        {
            var lstpf = QueryList<oa_userpower_positionsfunctions>(a=>a.functionid==functionid);
            List<oa_member_positions> lstpt = new List<oa_member_positions>();
            foreach (var item in lstpf)
            {
                lstpt.Add(Query<oa_member_positions>(a=>a.id==item.positionid));
            }
            return lstpt;
        }
        /// <summary>
        /// 根据职位id查询该职位的功能列表
        /// </summary>
        /// <param name="positionid"></param>
        /// <returns></returns>
        public List<oa_userpower_functions> QueryFunctionListByPositionid(long positionid)
        {
            var lstpf = QueryList<oa_userpower_positionsfunctions>(a => a.positionid == positionid);
            List<oa_userpower_functions> lstft = new List<oa_userpower_functions>();
            foreach (var item in lstpf)
            {
                lstft.Add(Query<oa_userpower_functions>(a => a.id == item.functionid));
            }
            return lstft;
        }


        /// <summary>
        /// 新增功能ACTION
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public bool AddFunctionAction(oa_userpower_functionsactions fa)
        {
            return Add<oa_userpower_functionsactions>(fa);
        }

        /// <summary>
        /// 修改功能ACTION
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public bool UpdateFunctionAction(oa_userpower_functionsactions fa)
        {
            return Update<oa_userpower_functionsactions>(fa);
        }

        /// <summary>
        /// 根据id查询功能ACTION信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public oa_userpower_functionsactions QueryFunctionAction(long id)
        {
            return Query<oa_userpower_functionsactions>(a => a.id == id);
        }
        /// <summary>
        /// 根据功能id查询该功能的Action
        /// </summary>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        public List<oa_userpower_actions> QueryActionListByFunctionid(long functionid)
        {
            var lstfa = QueryList<oa_userpower_functionsactions>(a => a.functionid == functionid);
            List<oa_userpower_actions> lstpt = new List<oa_userpower_actions>();
            foreach (var item in lstfa)
            {
                lstpt.Add(Query<oa_userpower_actions>(a => a.id == item.actionid));
            }
            return lstpt;
        }
        /// <summary>
        /// 根据actiond查询拥有该action的功能列表
        /// </summary>
        /// <param name="positionid">actionid</param>
        /// <returns></returns>
        public List<oa_userpower_functions> QueryFunctionListByActionid(long actionid)
        {
            var lstfa = QueryList<oa_userpower_functionsactions>(a => a.actionid == actionid);
            List<oa_userpower_functions> lstft = new List<oa_userpower_functions>();
            foreach (var item in lstfa)
            {
                lstft.Add(Query<oa_userpower_functions>(a => a.id == item.functionid));
            }
            return lstft;
        }
    }
}
