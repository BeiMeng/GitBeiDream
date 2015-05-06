﻿using BeiDream.EasyUi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeiDream.EasyUi.Controllers
{
    public class LoginController : Controller
    {
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
            //Session["vcode"] = code;
            SessionHelper.SetSession("vcode", code);
            v.Padding = 10;
            byte[] bytes = v.CreateImage(code);
            return File(bytes, @"image/jpeg");
        }
    }
}