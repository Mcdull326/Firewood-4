using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLinq;
using System.Data.Linq;

namespace DAL
{
    public class DAL_Com
    {
        private FirewoodDataContext fwDataContext = new FirewoodDataContext();

        #region 公共方法
        /// <summary>
        /// 判断查询到的是否为空
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public comment ifQuery(IQueryable<comment> query)
        {
            try
            {
                if (query.Count() > 0)
                {
                    return query.Single();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 判断查询到的是否为空,为空返回null,不为空返回IQueryable<comment>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<comment> ifQueryAll(IQueryable<comment> query)
        {
            try
            {
                if (query.Count() > 0)
                {
                    return query;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 判断SubmitChanges是否成功
        /// </summary>
        /// <param name="fwDataContext"></param>
        /// <returns></returns>
        public bool SubmitChangesWithReturnValue(FirewoodDataContext fwDataContext)
        {
            try
            {
                fwDataContext.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion 公共方法

        #region 查询
        /// <summary>
        /// 通过ActID查询所有留言
        /// </summary>
        /// <param name="actid"></param>
        /// <returns></returns>
        public IQueryable<comment> SelectComByActID(Guid actid)
        {
            var query = fwDataContext.comment.Where(c => c.ActID == actid && c.State == 0);
            return ifQueryAll(query);
        }
        #endregion 查询

        #region 增
        /// <summary>
        /// 新增一条留言
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public bool AddCom(comment com)
        {
            fwDataContext.comment.InsertOnSubmit(com);
            return SubmitChangesWithReturnValue(fwDataContext);
        }
        #endregion 增

        #region 删
        /// <summary>
        /// 通过ComID删除留言
        /// </summary>
        /// <param name="comid"></param>
        /// <returns></returns>
        public bool DelComByComID(Guid comid)
        {
            var query = fwDataContext.comment.Where(c => c.ComID == comid && c.State == 0);
            IQueryable<comment> comQuery = ifQueryAll(query);
            if (comQuery != null)
            {
                foreach (var com in comQuery)
                {
                    com.State = 1;
                }
                return SubmitChangesWithReturnValue(fwDataContext);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 如果删除了某个活动，把所有的留言都删除
        /// </summary>
        /// <param name="actid"></param>
        /// <returns></returns>
        public bool DelComByActID(Guid actid)
        {
            var query = fwDataContext.comment.Where(c => c.ActID == actid && c.State == 0);
            IQueryable<comment> comQuery = ifQueryAll(query);
            if (comQuery != null)
            {
                foreach (var join in comQuery)
                {
                    join.State = 1;
                }
                return SubmitChangesWithReturnValue(fwDataContext);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 如果删除了某个用户，把他所有的留言都删除
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool DelComByUserID(Guid userid)
        {
            var query = fwDataContext.comment.Where(c => c.UserID == userid && c.State == 0);
            IQueryable<comment> comQuery = ifQueryAll(query);
            if (comQuery != null)
            {
                foreach (var join in comQuery)
                {
                    join.State = 1;
                }
                return SubmitChangesWithReturnValue(fwDataContext);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 如果删除了某个社团，把他所有的留言都删除
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public bool DelComByOrgID(Guid orgid)
        {
            var query = fwDataContext.comment.Where(c => c.OrgID == orgid && c.State == 0);
            IQueryable<comment> comQuery = ifQueryAll(query);
            if (comQuery != null)
            {
                foreach (var join in comQuery)
                {
                    join.State = 1;
                }
                return SubmitChangesWithReturnValue(fwDataContext);
            }
            else
            {
                return false;
            }
        }
        #endregion 删
    }
}
