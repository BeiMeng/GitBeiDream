using BeiDream.Service.IService;

namespace BeiDream.Service
{
    public class PetaPocoUserService : IUserService
    {
        public string ValidateLogin(string userName,string passWord)
        {
            if (userName != "beidream")
                return "用户名不存在";
            if (passWord != "123456")
                return "密码错误";
            return null;
        }
    }
}
