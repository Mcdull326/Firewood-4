using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using BLL;
using DataLinq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            BLL_Org orgBLL = new BLL_Org();

            Console.WriteLine(orgBLL.Login("光华园网站","123").OrgName);
        }
    }
}
