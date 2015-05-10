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

        #region 查询所有活动
        public IQueryable<activity> SelectAllAct()
        {
            return actDAL.SelectAllAct();
        }
        #endregion 查询所有活动

        #region 判断活动名称是否存在
        public bool IsActNameExist(string actname)
        {
            if (actDAL.SelectActByName(actname) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion 判断活动名称是否存在

        #region 获取图片路径
        public string SelectPathByActID(Guid actid)
        {
            return actDAL.SelectActByID(actid).ActPic;
        }
        #endregion 获取图片路径

        #region 创建活动
        /// <summary>
        /// 创建一个活动
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public bool AddActivity(activity act)
        {
            act.ActID = Guid.NewGuid();
            act.State = 0;
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
