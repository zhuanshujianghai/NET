using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_IBLL.user
{
    public interface Iforummanage_forum
    {
        /// <summary>
        /// 新增版块
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        returnresult AddSection(oa_forum_section st);

        /// <summary>
        /// 修改版块
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        returnresult UpdateSection(oa_forum_section st);

        /// <summary>
        /// 删除版块
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        bool DeleteSection(oa_forum_section st);

        /// <summary>
        /// 根据版块id和公司id查询版块信息
        /// </summary>
        /// <param name="id">版块id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_forum_section QuerySectionByIdcCompanyid(long id, long companyid);

        /// <summary>
        /// 根据公司id查询版块列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_forum_section> QuerySectionListByCompanyid(long companyid);

        /// <summary>
        /// 新增主贴
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        bool AddTopic(oa_forum_topic tp);

        /// <summary>
        /// 修改主贴
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        bool UpdateTopic(oa_forum_topic tp);

        /// <summary>
        /// 删除主贴
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        bool DeleteTopic(oa_forum_topic tp);

        /// <summary>
        /// 根据主贴id和公司id查询主贴信息
        /// </summary>
        /// <param name="id">主贴id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_forum_topic QueryTopicByIdCompanyid(long id, long companyid);

        /// <summary>
        /// 根据公司id查询主贴信息
        /// </summary>
        /// <param name="sectionid">版块id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_forum_topic> QueryTopicByCompanyId(long sectionid,long companyid);

        /// <summary>
        /// 新增跟帖
        /// </summary>
        /// <param name="rp"></param>
        /// <returns></returns>
        bool AddReply(oa_forum_reply rp);

        /// <summary>
        /// 修改跟帖
        /// </summary>
        /// <param name="rp"></param>
        /// <returns></returns>
        bool UpdateReply(oa_forum_reply rp);

        /// <summary>
        /// 删除跟帖
        /// </summary>
        /// <param name="rp"></param>
        /// <returns></returns>
        bool DeleteReply(oa_forum_reply rp);

        /// <summary>
        /// 根据跟帖id和公司id查询跟帖信息
        /// </summary>
        /// <param name="id">跟帖id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        oa_forum_reply QueryReplyByIdCompanyid(long id, long companyid);

        /// <summary>
        /// 根据主贴id和公司id查询跟帖列表
        /// </summary>
        /// <param name="topicid">主贴id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        List<view_oa_forum_reply> QueryReplyListByTopicidCompanyid(long topicid, long companyid);
    }
}
