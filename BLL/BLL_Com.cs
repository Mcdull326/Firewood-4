using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLinq;
using DAL;

namespace BLL
{
    public class BLL_Com
    {
        private DAL_Com comDAL = new DAL_Com();

        #region 显示某个活动的所有留言
        /// <summary>
        /// 显示某个活动的所有留言
        /// </summary>
        /// <param name="actid"></param>
        /// <returns></returns>
        public IQueryable<comment> SelectComByActID(Guid actid)
        {
            return comDAL.SelectComByActID(actid);
        }
        #endregion 显示某个活动的所有留言

        #region 添加留言
        /// <summary>
        /// 添加留言
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public bool AddCom(comment com)
        {
            return comDAL.AddCom(com);
        }
        #endregion 添加留言

        #region 删除
        /// <summary>
        /// 删除某条留言
        /// </summary>
        /// <param name="comid"></param>
        /// <returns></returns>
        public bool DelComByComID(Guid comid)
        {
            return comDAL.DelComByComID(comid);
        }
        #endregion 删除
    }
}
