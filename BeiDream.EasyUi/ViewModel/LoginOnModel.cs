using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeiDream.EasyUi.ViewModel
{
        public class LogOnModel
        {
            [Required]
            [Display(Name = "用户名")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "密码")]
            public string Password { get; set; }

            [Required]
            [StringLength(6, MinimumLength = 6)]
            [Display(Name = "验证码")]
            public string Vcode { get; set; }

            [Display(Name = "记住我?")]
            public bool RememberMe { get; set; }
        }
   
}