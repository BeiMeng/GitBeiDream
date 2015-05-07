using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeiDream.EasyUi.ViewModel
{
        public class LogOnModel
        {
             [Required(ErrorMessage = @"用户名不能为空")]
            [Display(Name = @"用户名")]
            public string UserName { get; set; }

            [Required(ErrorMessage = @"密码不能为空")]
            [DataType(DataType.Password)]
            [StringLength(8, MinimumLength = 6, ErrorMessage = @"密码最短6位，不能超过8位")]
            [Display(Name = @"密码")]
            public string Password { get; set; }

            [Required(ErrorMessage = @"验证码不能为空")]
            [StringLength(6, MinimumLength = 6, ErrorMessage = @"验证码长度为6位")]
            [Display(Name = @"验证码")]
            public string Vcode { get; set; }

            [Display(Name = @"记住我?")]
            public bool RememberMe { get; set; }
        }
   
}