using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using DataLinq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            DAL_User userDAL = new DAL_User();
            user user = new user();
            user.uNum = "41211017";
            user.uMail = "zhzq326@ghy.cn";
            user.uTel = "15708428703";
            user.uGrade = "2012";
            user.uSex = 1;
            user.trueName = "";
            Console.WriteLine(userDAL.UpdateUserInfo(user));
        }
    }
}
