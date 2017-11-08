using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_IBLL.user
{
    public interface Iuserpowermanage_function
    {
        /// <summary>
        /// 修改功能
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        returnresult UpdateFunction(oa_userpower_functions pm);

        /// <summary>
        /// 根据功能id查询功能信息
        /// </summary>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        oa_userpower_functions QueryFunctionById(long functionid);

        /// <summary>
        /// 根据公司id查询功能列表
        /// </summary>
        /// <returns></returns>
        List<oa_userpower_functions> QueryFunctionList();

        /// <summary>
        /// 修改ACTION
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        returnresult UpdateAction(oa_userpower_actions at);

        /// <summary>
        /// 根据actionid查询功能信息
        /// </summary>
        /// <param name="functionid">actionid</param>
        /// <returns></returns>
        oa_userpower_actions QueryActionById(long actionid);

        /// <summary>
        /// 根据公司id查询action列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<oa_userpower_actions> QueryActionList(long companyid);


        /// <summary>
        /// 新增职位功能
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        bool AddPositionFunction(oa_userpower_positionsfunctions pf);

        /// <summary>
        /// 修改职位功能
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        bool UpdatePositionFunction(oa_userpower_positionsfunctions at);

        /// <summary>
        /// 根据id查询职位功能信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        oa_userpower_positionsfunctions QueryPositionFunction(long id);
        /// <summary>
        /// 根据功能id查询拥有该功能的职位列表
        /// </summary>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        List<oa_member_positions> QueryPositionListByFunctionid(long functionid);
        /// <summary>
        /// 根据职位id查询该职位的功能列表
        /// </summary>
        /// <param name="positionid"></param>
        /// <returns></returns>
        List<oa_userpower_functions> QueryFunctionListByPositionid(long positionid);

        /// <summary>
        /// 新增功能ACTION
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        bool AddFunctionAction(oa_userpower_functionsactions fa);

        /// <summary>
        /// 修改功能ACTION
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        bool UpdateFunctionAction(oa_userpower_functionsactions fa);

        /// <summary>
        /// 根据id查询功能ACTION信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        oa_userpower_functionsactions QueryFunctionAction(long id);

        /// <summary>
        /// 根据功能id查询该功能的Action
        /// </summary>
        /// <param name="functionid">功能id</param>
        /// <returns></returns>
        List<oa_userpower_actions> QueryActionListByFunctionid(long functionid);

        /// <summary>
        /// 根据actiond查询拥有该action的功能列表
        /// </summary>
        /// <param name="positionid">actionid</param>
        /// <returns></returns>
        List<oa_userpower_functions> QueryFunctionListByActionid(long actionid);
    }
}
