using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLinq;
using System.Data.Linq;

namespace DAL
{
    public class DAL_Join
    {
        private FirewoodDataContext fwDataContext = new FirewoodDataContext();

        #region 公共方法
        /// <summary>
        /// 判断查询到的是否为空,为空返回null,不为空返回join
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public join ifQuery(IQueryable<join> query)
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
        /// 判断查询到的是否为空,为空返回null,不为空返回IQueryable<join>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<join> ifQueryAll(IQueryable<join> query)
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
        public bool SubmitChangesWithReturnValue(DataLinq.FirewoodDataContext fwDataContext)
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
        /// 通过UserID查询用户想参加的所有活动，为空返回null
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IQueryable<join> SelectJoinByUserID(Guid userid)
        {
            var query = fwDataContext.join.Where(j => j.UserID == userid && j.State == 0);
            return ifQueryAll(query);
        }
        /// <summary>
        /// 通过ActID查询所有想参加该活动的用户，为空返回null
        /// </summary>
        /// <param name="actid"></param>
        /// <returns></returns>
        public IQueryable<join> SelectJoinByActID(Guid actid)
        {
            var query = fwDataContext.join.Where(j => j.ActID == actid && j.State == 0);
            return ifQueryAll(query);
        }
        #endregion 查询

        #region 增
        /// <summary>
        /// 我想参加
        /// </summary>
        /// <param name="join"></param>
        /// <returns></returns>
        public bool AddJoin(join join)
        {
            fwDataContext.join.InsertOnSubmit(join);
            return SubmitChangesWithReturnValue(fwDataContext);
        }
        #endregion 增

        #region 删
        /// <summary>
        /// 删除该活动所有想参加的人
        /// </summary>
        /// <param name="actid"></param>
        /// <returns></returns>
        public bool DelJoinByActID(Guid actid)
        {
            var query = fwDataContext.join.Where(j => j.ActID == actid && j.State == 0);
            IQueryable<join> joinQuery= ifQueryAll(query);
            if (joinQuery != null)
            {
                foreach (var join in joinQuery)
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
        /// 如果注销了某个用户，把他的所有想参加的活动删除
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool DelJoinByUserID(Guid userid)
        {
            var query = fwDataContext.join.Where(j => j.UserID == userid && j.State == 0);
            IQueryable<join> joinQuery = ifQueryAll(query);
            if (joinQuery != null)
            {
                foreach (var join in joinQuery)
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
