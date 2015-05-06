using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLinq;
using DAL;

namespace BLL
{
    public class BLL_User
    {
        private DAL_User userDAL = new DAL_User();
        private DAL_Join joinDAL = new DAL_Join();
        private DAL_Com comDAL = new DAL_Com();

        #region 登录
        /// <summary>
        /// 由学号登录,错误返回null
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public user Login(string usernum, string password)
        {
            usernum = usernum != null ? usernum : "";
            password = password != null ? Md5.MD5_encrypt(password) : "";
            if (userDAL.UpdateIPTime(usernum))//更新IP和登录时间
            {
                return userDAL.LoginByNum(usernum, password);
            }
            else
            {
                return null;
            }
        }
        #endregion 登录

        #region 注册
        /// <summary>
        /// 判断注册字段长度（学号、昵称、密码）
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string IsBlankReg(user user)
        {
            if (user.uNum.Equals("") || user.uNum.Length > 20)
            {
                return "usernum";
            }
            else if (user.uName.Equals("") || user.uName.Length > 20)
            {
                return "username";
            }
            else if (user.uPwd.Equals("") || user.uPwd.Length > 50)
            {
                return "userpassword";
            }
            else
            {
                return "ok";
            }
        }
        /// <summary>
        /// 注册（学号、昵称、密码）
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Register(user user)
        {
            if (IsBlankReg(user).Equals("ok"))
            {
                user.uID = Guid.NewGuid();
                user.uPwd = Md5.MD5_encrypt(user.uPwd);
                user.registerTime = DateTime.Now;
                user.lastLogin = DateTime.Now;
                user.uIP = GetClientIP.GetIP();
                user.state = 0;
                return userDAL.Register(user);
            }
            else
            {
                return false;
            }
        }
        #endregion 注册

        #region 完善信息
        /// <summary>
        /// 判断完善信息时的字段（昵称、邮箱、手机号、真实姓名）
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string IsBlankAdd(user user)
        {
            if (user.uName.Equals("") || user.uName.Length > 20)
            {
                return "username";
            }
            else if (user.uMail.Equals("") || user.uMail.Length > 20)
            {
                return "usermail";
            }
            else if (user.uTel.Equals("") || user.uTel.Length > 50)
            {
                return "usertel";
            }
            else
            {
                return "ok";
            }
        }
        /// <summary>
        /// 完善信息（昵称、邮箱、手机号、年级、性别、头像、真实姓名）
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUserInfo(user user)
        {
            return userDAL.UpdateUserInfo(user);
        }
        #endregion 完善信息

        #region 注销
        /// <summary>
        /// 由学号注销用户,同时把用户想参加的活动及留言也注销
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public bool DelUser(string usernum)
        {
            Guid userid = userDAL.SelectUseridByNum(usernum);
            return userDAL.DelUser(usernum) && joinDAL.DelJoinByUserID(userid) && comDAL.DelComByUserID(userid);
        }
        #endregion 注销

        #region 由学号查昵称
        /// <summary>
        /// 由学号查昵称，若学号未被注册，返回null
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public string GetNameByNum(string usernum)
        {
            var user = userDAL.SelectUserByNum(usernum);
            if (user == null)
            {
                return null;
            }
            else
            {
                return user.uName;
            }
        }
        #endregion 由学号查昵称

        #region 由学号查邮箱
        /// <summary>
        /// 由学号查邮箱
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public string SelectMailByNum(string usernum)
        {
            user user = userDAL.SelectUserByNum(usernum);
            if (user == null)
            {
                return null;
            }
            else
            {
                return user.uMail;
            }
        }
        #endregion 由学号查邮箱

        #region 由学号查手机号
        /// <summary>
        /// 由学号查手机号
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public string SelectTelByNum(string usernum)
        {
            user user = userDAL.SelectUserByNum(usernum);
            if (user == null)
            {
                return null;
            }
            else
            {
                return user.uTel;
            }
        }
        #endregion 由学号查手机号

        #region 判断学号是否已被注册
        /// <summary>
        /// 判断学号是否已被注册
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public bool IsUserNumExist(string usernum)
        {
            if (userDAL.SelectUserByNum(usernum) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion 判断学号是否已被注册

        #region 判断昵称是否已被注册
        /// <summary>
        /// 判断昵称是否已被注册
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsUserNameExist(string username)
        {
            if (userDAL.SelectUserByName(username) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion 判断昵称是否已被注册

        #region 判断邮箱是否已被注册
        /// <summary>
        /// 判断邮箱是否已被注册
        /// </summary>
        /// <param name="usermail"></param>
        /// <returns></returns>
        public bool IsUserMailExist(string usermail)
        {
            if (userDAL.SelectUserByMail(usermail) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion 判断邮箱是否已被注册

        #region 判断手机号是否已被注册
        /// <summary>
        /// 判断邮箱是否已被注册
        /// </summary>
        /// <param name="usertel"></param>
        /// <returns></returns>
        public bool IsUserTelExist(string usertel)
        {
            if (userDAL.SelectUserByTel(usertel) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion 判断手机号是否已被注册

        #region 修改密码
        /// <summary>
        /// 由学号修改密码
        /// </summary>
        /// <param name="usernum"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UpdateUserPwd(string usernum, string password)
        {
            usernum = usernum != null ? usernum : "";
            password = password != null ? Md5.MD5_encrypt(password) : "";
            return userDAL.UpdateUserPwd(usernum, password);
        }
        #endregion 修改密码

        #region 验证学号
        /// <summary>
        /// 验证学号，返回bool
        /// </summary>
        /// <param name="usernum"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckNum(string usernum, string password)
        {
            usernum = usernum != null ? usernum : "";
            password = password != null ? password : "";
            return CheckStuNum.CheckNum(usernum, password);
        }
        #endregion 验证学号
    }
}
