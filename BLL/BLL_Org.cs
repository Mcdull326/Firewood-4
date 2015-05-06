using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLinq;
using DAL;

namespace BLL
{
    public class BLL_Org
    {
        private DAL_Org orgDAL = new DAL_Org();
        private DAL_Act actDAL = new DAL_Act();
        private DAL_Com comDAL = new DAL_Com();

        #region 社团组织登录
        /// <summary>
        /// 社团组织用名称和密码登录
        /// </summary>
        /// <param name="orgname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public org Login(string orgname, string password)
        {
            orgname = orgname != null ? orgname.Trim() : "";
            password = password != null ? Md5.MD5_encrypt(password.Trim()) : "";
            if (orgDAL.UpdateIPTime(orgname))//更新IP和登录时间
            {
                return orgDAL.LoginByName(orgname, password);
            }
            else
            {
                return null;
            }
        }
        #endregion 社团组织登录

        #region 社团组织注册
        /// <summary>
        /// 判断注册字段长度（社团名称、密码、负责人姓名、负责人电话、所属部门、简介、联系方式）
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public string IsBlankReg(org org)
        {
            if (org.OrgName.Equals("") || org.OrgName.Length > 20)
            {
                return "orgname";
            }
            else if (org.OrgPwd.Equals("") || org.OrgPwd.Length > 50)
            {
                return "orgpwd";
            }
            else if (org.OrgPrincipal.Equals("") || org.OrgPrincipal.Length > 20)
            {
                return "orgprincipal";
            }
            else if (org.OrgTel.Equals("") || org.OrgTel.Length > 50)
            {
                return "orgtel";
            }
            else if (org.OrgDepartment.Equals("") || org.OrgDepartment.Length > 20)
            {
                return "orgdepartment";
            }
            else if (org.OrgIntroduction.Equals("") || org.OrgIntroduction.Length > 500)
            {
                return "orgintroduction";
            }
            else if (org.OrgContact.Equals("") || org.OrgContact.Length > 20)
            {
                return "orgcontact";
            }
            else
            {
                return "ok";
            }
        }
        /// <summary>
        /// 社团组织注册
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public bool Register(org org)
        {
            if (IsBlankReg(org).Equals("ok"))
            {
                org.OrgID = Guid.NewGuid();
                org.RegisterTime = DateTime.Now;
                org.LastLogin = DateTime.Now;
                org.OrgIP = GetClientIP.GetIP();
                org.State = 2;//待审核
                return orgDAL.Register(org);
            }
            else
            {
                return false;
            }
        }
        #endregion 社团组织注册

        #region 判断社团/组织名称是否已被注册
        /// <summary>
        /// 判断社团/组织名称是否已被注册
        /// </summary>
        /// <param name="orgname"></param>
        /// <returns></returns>
        public bool IsNameExist(string orgname)
        {
            if (orgDAL.SelectOrgByName(orgname) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion 判断社团/组织名称是否存在

        #region 注销
        /// <summary>
        /// 注销社团组织,同时把其发布的活动及留言全部删除
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public bool DelOrg(Guid orgid)
        {
            return orgDAL.DelOrg(orgid) && actDAL.DelActByOrgID(orgid) && comDAL.DelComByOrgID(orgid);
        }
        #endregion 注销

        #region 修改信息
        /// <summary>
        /// 更新社团信息（logo、负责人、负责人电话、所属部门、介绍、联系方式）
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public bool UpdateOrg(org org)
        {
            return orgDAL.UpdateOrgInfo(org);
        }
        #endregion 修改信息
    }
}
