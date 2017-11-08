using OA_IBLL.user;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_BLL.user
{
    /// <summary>
    /// 论坛
    /// </summary>
    public class forummanage_forum : BaseUserService,Iforummanage_forum
    {
        /// <summary>
        /// 新增版块
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public returnresult AddSection(oa_forum_section st)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_forum_section>(a => a.sectionname == st.sectionname && a.companyid == st.companyid))
            {
                rr.status = false;
                rr.msg = "版块已存在";
            }
            else
            {
                rr.status = Add<oa_forum_section>(st);
                if (rr.status)
                {
                    rr.msg = "新增版块成功";
                }
                else
                {
                    rr.msg = "新增版块失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 修改版块
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public returnresult UpdateSection(oa_forum_section st)
        {
            returnresult rr = new returnresult();
            if (Exist<oa_forum_section>(a => a.sectionname == st.sectionname && a.companyid == st.companyid))
            {
                rr.status = false;
                rr.msg = "版块已存在";
            }
            else
            {
                rr.status = Update<oa_forum_section>(st);
                if (rr.status)
                {
                    rr.msg = "修改版块成功";
                }
                else
                {
                    rr.msg = "修改版块失败";
                }
            }
            return rr;
        }

        /// <summary>
        /// 删除版块
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool DeleteSection(oa_forum_section st)
        {
            st.status = -1;
            return Update<oa_forum_section>(st);
        }

        /// <summary>
        /// 根据版块id和公司id查询版块信息
        /// </summary>
        /// <param name="id">版块id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_forum_section QuerySectionByIdcCompanyid(long id, long companyid)
        {
            return Query<oa_forum_section>(a=>a.id==id && a.companyid==companyid);
        }

        /// <summary>
        /// 根据公司id查询版块列表
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_forum_section> QuerySectionListByCompanyid(long companyid)
        {
            return QueryList<view_oa_forum_section>(a=>a.companyid==companyid).Where(a=>a.status!=-1).ToList();
        }

        /// <summary>
        /// 新增主贴
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public bool AddTopic(oa_forum_topic tp)
        {
            return Add<oa_forum_topic>(tp);
        }

        /// <summary>
        /// 修改主贴
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public bool UpdateTopic(oa_forum_topic tp)
        {
            return Update<oa_forum_topic>(tp);
        }

        /// <summary>
        /// 删除主贴
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public bool DeleteTopic(oa_forum_topic tp)
        {
            returnresult rr = new returnresult();
            List<oa_forum_reply> lstrp = QueryList<oa_forum_reply>(a => a.topicid == tp.id);
            if (lstrp.ToList().Count > 0)
            {
                DeleteIQueryable<oa_forum_reply>(lstrp);
            }
            return Delete<oa_forum_topic>(tp);
        }

        /// <summary>
        /// 根据主贴id和公司id查询主贴信息
        /// </summary>
        /// <param name="id">主贴id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_forum_topic QueryTopicByIdCompanyid(long id, long companyid)
        {
            return Query<oa_forum_topic>(a=>a.id==id && a.companyid==companyid);
        }

        /// <summary>
        /// 根据公司id查询主贴信息
        /// </summary>
        /// <param name="sectionid">版块id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_forum_topic> QueryTopicByCompanyId(long sectionid,long companyid)
        {
            return QueryList<view_oa_forum_topic>(a=>a.sectionid==sectionid && a.companyid==companyid).Where(a=>a.status!=-1).ToList();
        }

        /// <summary>
        /// 新增跟帖
        /// </summary>
        /// <param name="rp"></param>
        /// <returns></returns>
        public bool AddReply(oa_forum_reply rp)
        {
            return Add<oa_forum_reply>(rp);
        }

        /// <summary>
        /// 修改跟帖
        /// </summary>
        /// <param name="rp"></param>
        /// <returns></returns>
        public bool UpdateReply(oa_forum_reply rp)
        {
            return Update<oa_forum_reply>(rp);
        }

        /// <summary>
        /// 删除跟帖
        /// </summary>
        /// <param name="rp"></param>
        /// <returns></returns>
        public bool DeleteReply(oa_forum_reply rp)
        {
            return Delete<oa_forum_reply>(rp);
        }

        /// <summary>
        /// 根据跟帖id和公司id查询跟帖信息
        /// </summary>
        /// <param name="id">跟帖id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public oa_forum_reply QueryReplyByIdCompanyid(long id, long companyid)
        {
            return Query<oa_forum_reply>(a=>a.id==id && a.companyid==companyid);
        }

        /// <summary>
        /// 根据主贴id和公司id查询跟帖列表
        /// </summary>
        /// <param name="topicid">主贴id</param>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        public List<view_oa_forum_reply> QueryReplyListByTopicidCompanyid(long topicid, long companyid)
        {
            return QueryList<view_oa_forum_reply>(a=>a.topicid==topicid && a.companyid==companyid);
        }
    }
}
