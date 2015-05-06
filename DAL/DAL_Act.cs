using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLinq;
using System.Data.Linq;

namespace DAL
{
    public class DAL_Act
    {
        private FirewoodDataContext fwDataContext = new FirewoodDataContext();

        #region 公共方法
        /// <summary>
        /// 判断查询到的是否为空,为空返回null,不为空返回activity
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataLinq.activity ifQuery(IQueryable<activity> query)
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
        /// 判断查询到的是否为空,为空返回null,不为空返回IQueryable<activity>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<activity> ifQueryAll(IQueryable<activity> query)
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
        /// 通过ActID获得act实体
        /// </summary>
        /// <param name="actid"></param>
        /// <returns></returns>
        public activity SelectActByID(Guid actid)
        {
            var query = fwDataContext.activity.Where(a => a.ActID == actid && a.State == 0);
            return ifQuery(query);
        }
        /// <summary>
        /// 根据活动名称模糊查询activity
        /// </summary>
        /// <param name="actname"></param>
        /// <returns></returns>
        public activity SelectActByName(string actname)
        {
            var query = fwDataContext.activity.Where(a => a.ActName.Contains(actname) && a.State == 0);
            return ifQuery(query);
        }
        /// <summary>
        /// 根据OrgID查询所有activity
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public IQueryable<activity> SelectActByOrgID(Guid orgid)
        {
            var query = fwDataContext.activity.Where(a => a.OrgID == orgid && a.State == 0);
            return ifQueryAll(query);
        }
        /// <summary>
        /// 根据Class1查询所有activity
        /// </summary>
        /// <param name="class2"></param>
        /// <returns></returns>
        public IQueryable<activity> SelectActByClass1(string class1)
        {
            var query = fwDataContext.activity.Where(a => a.Class1 == class1 && a.State == 0);
            return ifQueryAll(query);
        }
        /// <summary>
        /// 根据Class2查询所有activity
        /// </summary>
        /// <param name="class2"></param>
        /// <returns></returns>
        public IQueryable<activity> SelectActByClass2(string class2)
        {
            var query = fwDataContext.activity.Where(a => a.Class2 == class2 && a.State == 0);
            return ifQueryAll(query);
        }
        /// <summary>
        /// 根据Place查询所有activity
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public IQueryable<activity> SelectActByPlace(string place)
        {
            var query = fwDataContext.activity.Where(a => a.Place == place && a.State == 0);
            return ifQueryAll(query);
        }
        /// <summary>
        /// 查询所有还没开始的activity
        /// </summary>
        /// <returns></returns>
        public IQueryable<activity> SelectActByBeginTime()
        {
            var query = fwDataContext.activity.Where(a => DateTime.Compare(DateTime.Now, a.BeginTime) <= 0 && a.State == 0);
            return ifQueryAll(query);
        }
        /// <summary>
        /// 查询所有还没结束的activity(首页显示的那些活动)
        /// </summary>
        /// <returns></returns>
        public IQueryable<activity> SelectActByEndTime()
        {
            var query = fwDataContext.activity.Where(a => DateTime.Compare(DateTime.Now, a.EndTime) <= 0 && a.State == 0);
            return ifQueryAll(query);
        }
        /// <summary>
        /// 查询所有正在进行中的activity
        /// </summary>
        /// <returns></returns>
        public IQueryable<activity> SelectActByTime()
        {
            DateTime now = DateTime.Now;
            var query = fwDataContext.activity.Where(a => DateTime.Compare(now, a.BeginTime) >= 0 && DateTime.Compare(now, a.EndTime) <= 0 && a.State == 0);
            return ifQueryAll(query);
        }
        /// <summary>
        /// 查询所有有奖金的activity
        /// </summary>
        /// <returns></returns>
        public IQueryable<activity> SelectActByMoney()
        {
            var query = fwDataContext.activity.Where(a => a.MoneyState == 1 && a.State == 0);
            return ifQueryAll(query);
        }
        /// <summary>
        /// 查询所有有第二课堂学分认定的activity
        /// </summary>
        /// <returns></returns>
        public IQueryable<activity> SelectActByScore()
        {
            var query = fwDataContext.activity.Where(a => a.ScoreState == 1 && a.State == 0);
            return ifQueryAll(query);
        }
        /// <summary>
        /// 查询所有有加分的activity
        /// </summary>
        /// <returns></returns>
        public IQueryable<activity> SelectActByAward()
        {
            var query = fwDataContext.activity.Where(a => a.AwardState == 1 && a.State == 0);
            return ifQueryAll(query);
        }
        /// <summary>
        /// 查询所有需要投票的activity
        /// </summary>
        /// <returns></returns>
        public IQueryable<activity> SelectActByVote()
        {
            var query = fwDataContext.activity.Where(a => a.VoteState == 1 && a.State == 0);
            return ifQueryAll(query);
        }
        #endregion 查询

        #region 增
        /// <summary>
        /// 增加活动
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public bool AddActivity(activity act)
        {
            fwDataContext.activity.InsertOnSubmit(act);
            return SubmitChangesWithReturnValue(fwDataContext);
        }
        #endregion 增

        #region 改
        #endregion 改

        #region 删
        /// <summary>
        /// 根据ActID删除某个活动
        /// </summary>
        /// <param name="actid"></param>
        /// <returns></returns>
        public bool DelActivity(Guid actid)
        {
            var query = fwDataContext.activity.Where(a => a.ActID == actid && a.State == 0);
            if (ifQuery(query) != null)
            {
                query.Single().State = 1;
                return SubmitChangesWithReturnValue(fwDataContext);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据OrgID删除某些活动
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public bool DelActByOrgID(Guid orgid)
        {
            var query = fwDataContext.activity.Where(a => a.OrgID == orgid && a.State == 0);
            IQueryable<activity> actQuery = ifQueryAll(query);
            if (actQuery != null)
            {
                foreach (var act in actQuery)
                {
                    act.State = 1;
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
