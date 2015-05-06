using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLinq;
using DAL;

namespace BLL
{
    public class BLL_Act
    {
        private DAL_Act actDAL = new DAL_Act();

        #region 创建活动
        /// <summary>
        /// 创建一个活动
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public bool AddActivity(activity act)
        {
            return actDAL.AddActivity(act);
        }
        #endregion 创建活动

        #region 删除活动
        /// <summary>
        /// 根据ActID删除某个活动
        /// </summary>
        /// <param name="actid"></param>
        /// <returns></returns>
        public bool DelActivity(Guid actid)
        {
            return actDAL.DelActivity(actid);
        }
        #endregion 删除活动
    }
}
