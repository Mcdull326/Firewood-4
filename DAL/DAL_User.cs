using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLinq;
using System.Data.Linq;

namespace DAL
{
    public class DAL_User
    {
        private UserDataContext userDataContext = new UserDataContext();

        #region 公共方法
        /// <summary>
        /// 判断查询到的是否为空
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public user ifQuery(IQueryable<user> query)
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
        /// <param name="userDataContext"></param>
        /// <returns></returns>
        public bool SubmitChangesWithReturnValue(UserDataContext userDataContext)
        {
            try
            {
                userDataContext.SubmitChanges();
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
        /// 通过uID获得user实体
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public user SelectUserByID(Guid userid)
        {
            var query = userDataContext.user.Where(u => u.uID == userid && u.state == 0);
            return ifQuery(query);
        }
        /// <summary>
        /// 通过昵称获得user实体
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public user SelectUserByName(string username)
        {
            var query = userDataContext.user.Where(u => u.uName == username && u.state == 0);
            return ifQuery(query);
        }
        /// <summary>
        /// 通过学号获得user实体
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public user SelectUserByNum(string usernum)
        {
            var query = userDataContext.user.Where(u => u.uNum == usernum && u.state == 0);
            return ifQuery(query);
        }
        /// <summary>
        /// 通过邮箱获得user实体
        /// </summary>
        /// <param name="usermail"></param>
        /// <returns></returns>
        public user SelectUserByMail(string usermail)
        {
            var query = userDataContext.user.Where(u => u.uMail == usermail && u.state == 0);
            return ifQuery(query);
        }
        /// <summary>
        /// 通过手机号获得user实体
        /// </summary>
        /// <param name="usertel"></param>
        /// <returns></returns>
        public user SelectUserByTel(string usertel)
        {
            var query = userDataContext.user.Where(u => u.uTel == usertel && u.state == 0);
            return ifQuery(query);
        }
        /// <summary>
        /// 用昵称和密码判断登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public user LoginByName(string username, string password)
        {
            var query = userDataContext.user.Where(u => u.uName == username && u.uPwd == password && u.state == 0);
            return ifQuery(query);
        }
        /// <summary>
        /// 学号和密码登录
        /// </summary>
        /// <param name="usernum"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public user LoginByNum(string usernum, string password)
        {
            var query = userDataContext.user.Where(u => u.uNum == usernum && u.uPwd == password && u.state == 0);
            return ifQuery(query);
        }
        /// <summary>
        /// 通过学号查询UserID
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public Guid SelectUseridByNum(string usernum)
        {
            var query = userDataContext.user.Where(u => u.uNum == usernum && u.state == 0);
            user user = ifQuery(query);

            if (user == null)
            {
                return Guid.Empty;
            }
            else
            {
                return ifQuery(query).uID;
            }
        }
        #endregion 查询

        #region 增
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Register(user user){
            userDataContext.user.InsertOnSubmit(user);
            return SubmitChangesWithReturnValue(userDataContext);
        }
        #endregion 增

        #region 改
        /// <summary>
        /// 由学号修改密码
        /// </summary>
        /// <param name="usernum"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UpdateUserPwd(string usernum,string password)
        {
            var query = userDataContext.user.Where(u => u.uNum == usernum && u.state == 0);
            if (ifQuery(query) != null)
            {
                query.Single().uPwd = password;
                return SubmitChangesWithReturnValue(userDataContext);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 完善信息（昵称、邮箱、手机、年级、性别、头像、真实姓名）
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUserInfo(user user)
        {
            var query = userDataContext.user.Where(u => u.uNum == user.uNum && u.state == 0);
            if (ifQuery(query) != null)
            {
                //query.Single().uName = user.uName;
                query.Single().uMail = user.uMail;
                query.Single().uTel = user.uTel;
                query.Single().uGrade = user.uGrade;
                query.Single().uSex = user.uSex;
                //query.Single().uPic = user.uPic;
                query.Single().trueName = user.trueName;
                return SubmitChangesWithReturnValue(userDataContext);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据学号 更新用户ip和登录时间
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public bool UpdateIPTime(string usernum)
        {
            var query = userDataContext.user.Where(u => u.uNum == usernum && u.state == 0);
            if (query != null)
            {
                query.Single().uIP = GetClientIP.GetIP();
                query.Single().lastLogin = DateTime.Now;
                return SubmitChangesWithReturnValue(userDataContext);
            }
            else
            {
                return false;
            }
        }
        #endregion 改

        #region 删
        /// <summary>
        /// 由学号注销用户
        /// </summary>
        /// <param name="usernum"></param>
        /// <returns></returns>
        public bool DelUser(string usernum)
        {
            var query = userDataContext.user.Where(u => u.uNum == usernum && u.state == 0);
            if (ifQuery(query) != null)
            {
                query.Single().state = 1;
                return SubmitChangesWithReturnValue(userDataContext);
            }
            else
            {
                return false;
            }
        }
        #endregion 删
    }
}
