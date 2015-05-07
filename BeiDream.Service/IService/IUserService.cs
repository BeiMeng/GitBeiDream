using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeiDream.Service.IService
{
    public interface IUserService
    {
        string ValidateLogin(string userName, string passWord);
    }
}
