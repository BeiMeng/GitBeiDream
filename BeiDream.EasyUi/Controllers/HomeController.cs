using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Util.Webs.EasyUi.Results;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.EasyUi.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.UserName = "管理员";
            return View();

        }

        public string GetTree()
        {
            var node = new TreeNode { Id = "1", Text = "系统管理", Icon = "icon-edit" };
            var node1 = new TreeNode { Id = "10", ParentId = "1", Text = "应用程序管理-表格编辑", Icon = "icon-edit", Attributes = new { url = "/systems/application" } };
            var node2 = new TreeNode { Id = "11", ParentId = "1", Text = "应用程序管理-表单编辑", Icon = "icon-edit", Attributes = new { url = "/systems/application2" } };
            var node11 = new TreeNode { Id = "21", ParentId = "1", Text = "应用程序管理-行内编辑", Icon = "icon-edit", Attributes = new { url = "/systems/application3" } };
            var node3 = new TreeNode { Id = "12", ParentId = "1", Text = "字典管理", Icon = "icon-edit", Attributes = new { url = "/commons/dic" } };
            var node4 = new TreeNode { Id = "13", ParentId = "1", Text = "地区管理", Icon = "icon-edit", Attributes = new { url = "/commons/area" } };
            var node5 = new TreeNode { Id = "14", ParentId = "1", Text = "图标管理", Icon = "icon-edit", Attributes = new { url = "/commons/icon" } };
            return new TreeResult(new[] { node, node1, node2, node11, node3, node4, node5 }).ToString();
        }
    }
}
