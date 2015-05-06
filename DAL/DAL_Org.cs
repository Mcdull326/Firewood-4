using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLinq;
using System.Data.Linq;

namespace DAL
{
    public class DAL_Org
    {
        private FirewoodDataContext fwDataContext = new FirewoodDataContext();

        #region 公共方法
        /// <summary>
        /// 判断查询到的是否为空
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public org ifQuery(IQueryable<org> query)
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
        /// 通过OrgID获得org实体
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public org SelectOrgByID(Guid orgid)
        {
            var query = fwDataContext.org.Where(o => o.OrgID == orgid && o.State == 0);
            return ifQuery(query);
        }
        /// <summary>
        /// 通过OrgName获得org实体
        /// </summary>
        /// <param name="orgname"></param>
        /// <returns></returns>
        public org SelectOrgByName(string orgname)
        {
            var query = fwDataContext.org.Where(o => o.OrgName == orgname && o.State == 0);
            return ifQuery(query);
        }
        /// <summary>
        /// 社团名称和密码判断登录
        /// </summary>
        /// <param name="orgname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public org LoginByName(string orgname, string password)
        {
            var query = fwDataContext.org.Where(o => o.OrgName == orgname && o.OrgPwd == password && o.State == 0);
            return ifQuery(query);
        }
        #endregion 查询

        #region 增
        /// <summary>
        /// 社团组织注册
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public bool Register(org org)
        {
            fwDataContext.org.InsertOnSubmit(org);
            return SubmitChangesWithReturnValue(fwDataContext);
        }
        #endregion 增

        #region 改
        /// <summary>
        /// 更新社团信息（logo、负责人、负责人电话、所属部门、介绍、联系方式）
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public bool UpdateOrgInfo(org org)
        {
            var query = fwDataContext.org.Where(o => o.OrgName == org.OrgName && o.State == 0);
            if (ifQuery(query) != null)
            {
                query.Single().OrgPic = org.OrgPic;
                query.Single().OrgPrincipal = org.OrgPrincipal;
                query.Single().OrgTel = org.OrgTel;
                query.Single().OrgDepartment = org.OrgDepartment;
                query.Single().OrgIntroduction = org.OrgIntroduction;
                query.Single().OrgContact = org.OrgContact;
                return SubmitChangesWithReturnValue(fwDataContext);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据社团名称 更新IP和登录时间
        /// </summary>
        /// <param name="orgname"></param>
        /// <returns></returns>
        public bool UpdateIPTime(string orgname)
        {
            var query = fwDataContext.org.Where(o => o.OrgName == orgname && o.State == 0);
            if (query != null)
            {
                query.Single().OrgIP = GetClientIP.GetIP();
                query.Single().LastLogin = DateTime.Now;
                return SubmitChangesWithReturnValue(fwDataContext);
            }
            else
            {
                return false;
            }
        }
        #endregion 改

        #region 删
        /// <summary>
        /// 根据OrgID注销社团
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public bool DelOrg(Guid orgid)
        {
            var query = fwDataContext.org.Where(o => o.OrgID == orgid && o.State == 0);
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
        #endregion 删
    }
}
