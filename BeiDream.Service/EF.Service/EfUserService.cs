using BeiDream.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeiDream.Service
{
    public class EfUserService : IUserService
    {
        public string ValidateLogin(string userName, string passWord)
        {
            if (userName != "beidream")
                return "用户名不存在";
            if (passWord != "123456")
                return "密码错误";
            return null;
        }
    }
}
