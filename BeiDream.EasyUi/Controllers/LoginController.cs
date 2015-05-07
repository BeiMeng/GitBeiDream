using BeiDream.EasyUi.Common;
using BeiDream.EasyUi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeiDream.EasyUi.WebUI;
using BeiDream.Service.IService;
using BeiDream.Service;
using Util.Validations;

namespace BeiDream.EasyUi.Controllers
{
    public class LoginController : Controller
    {
        //todo IOC依赖注入，Autofac
        readonly IUserService _userService = new PetaPocoUserService();
        readonly IValidation _validation = new Validation();        

        //
        // GET: /Login/

        public ActionResult Index()
        {
            ViewBag.Title = "权限管理系统";
            return View();
        }
        public FileContentResult GetVerifyCode()
        {
            VerifyCode v = new VerifyCode();
            string code = v.CreateVerifyCode();                //取随机码
            SessionHelper.SetSession("vcode", code);
            v.Padding = 10;
            byte[] bytes = v.CreateImage(code);
            return File(bytes, @"image/jpeg");
        }
        [HttpPost]
        public ActionResult LoginOn(LogOnModel model)
        {  
            var validateResult = _validation.Validate(model);//服务器端的验证
            if (!validateResult.IsValid)
            {
                return this.ValidateJsonResult(false, validateResult);
            }
            List<string> msgs = new List<string>();
            string vcode = "";
            if (Session["vcode"] != null)
            {
                vcode = Session["vcode"].ToString();
            }
            if (vcode.Any() && String.Equals(vcode, model.Vcode, StringComparison.CurrentCultureIgnoreCase))
            {
                string msg = _userService.ValidateLogin(model.UserName, model.Password);
                if (string.IsNullOrEmpty(msg))
                    return this.ValidateJsonResult(true);
                msgs.Add(msg);
                    return this.ValidateJsonResult(false, null, msgs);
            }
            msgs.Add("验证码错误");
            return this.ValidateJsonResult(false, null, msgs);
        }
    }
}
