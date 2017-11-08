using OA_IDAL;
using OA_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OA_DAL
{
    /// <summary>
    /// 数据操作仓储类
    /// </summary>
    public class BaseRepository : IBaseRepository
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>布尔值</returns>
        public bool Add<T>(T entity) where T : class
        {
            bool result = false;
            using (oadbEntities db = new oadbEntities())
            {
                db.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
                try
                {
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                return result;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>实体</returns>
        public T AddReturnEntity<T>(T entity) where T : class
        {
            using (oadbEntities db = new oadbEntities())
            {
                db.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return entity;
            }
        }
        /// <summary>
        /// 添加集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddIQueryable<T>(List<T> lstentity) where T : class
        {
            bool result = false;
            using (oadbEntities db = new oadbEntities())
            {
                foreach (var item in lstentity)
                {
                    db.Entry<T>(item).State = System.Data.Entity.EntityState.Added;
                }
                try
                {
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                return result;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns>布尔值（修改结果）</returns>
        public bool Update<T>(T entity) where T : class
        {
            bool result = false;
            using (oadbEntities db = new oadbEntities())
            {
                db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }
        /// <summary>
        /// 修改集合
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns>布尔值（修改结果）</returns>
        public bool UpdateIQueryable<T>(List<T> entity) where T : class
        {
            bool result = false;
            using (oadbEntities db = new oadbEntities())
            {
                foreach (var item in entity)
                {
                    db.Entry<T>(item).State = System.Data.Entity.EntityState.Modified;
                }
                try
                {
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>布尔值（删除结果）</returns>
        public bool Delete<T>(T entity) where T : class
        {
            bool result = false;
            using (oadbEntities db = new oadbEntities())
            {
                db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
                try
                {
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                return result;
            }
        }
        /// <summary>
        /// 删除集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteIQueryable<T>(List<T> entity) where T : class
        {
            bool result = false;
            using (oadbEntities db = new oadbEntities())
            {
                foreach (var item in entity)
                {
                    db.Entry<T>(item).State = System.Data.Entity.EntityState.Deleted;
                }
                try
                {
                    db.SaveChanges();
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                return result;
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="Lambda">表达式</param>
        /// <returns>数据实体</returns>
        public T Query<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            using (oadbEntities db = new oadbEntities())
            {
                return db.Set<T>().FirstOrDefault(whereLambda);
            }
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="whereLambda">查询表达式</param>
        /// <returns>数据列表</returns>
        public List<T> QueryList<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            using (oadbEntities db = new oadbEntities())
            {
                return db.Set<T>().Where(whereLambda).ToList();
            }
        }

        /// <summary>
        /// 查询sql语句(集合)
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="entity">数据实体</param>
        /// <returns>数据列表</returns>
        public List<T> QueryListSql<T>(string sql) where T : class
        {
            using (oadbEntities db = new oadbEntities())
            {
                return db.Database.SqlQuery<T>(sql).ToList();
            }
        }

        /// <summary>
        /// 查询sql语句（单条）
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="entity">数据实体</param>
        /// <returns>数据</returns>
        public T QueryFirstSql<T>(string sql) where T : class
        {
            using (oadbEntities db = new oadbEntities())
            {
                return db.Database.SqlQuery<T>(sql).FirstOrDefault();
            }
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="anyLambda">查询表达式</param>
        /// <returns>布尔值</returns>
        public bool Exist<T>(Expression<Func<T, bool>> anyLambda) where T : class
        {
            using (oadbEntities db = new oadbEntities())
            {
                return db.Set<T>().Any(anyLambda);
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="name">存储过程名称</param>
        /// <param name="para">参数</param>
        public int execproc(string name,string word, SqlParameter[] para)
        {
            using (oadbEntities db = new oadbEntities())
            {
                try
                {
                    return db.Database.ExecuteSqlCommand(name + " " + word, para);
                }
                catch (Exception)
                {
                    return -1024;
                }
            }
        }
    }
}
