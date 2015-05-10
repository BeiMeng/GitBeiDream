using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeiDream.Service;
using BeiDream.Service.IService;
using BeiDream.Service.PetaPoco.Service;
using FluentBootstrap.Typography;
using Util.Webs.EasyUi.Results;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.EasyUi.Controllers
{
    public class HomeController : Controller
    {
        //todo IOC依赖注入，Autofac
        readonly IMenuService _menuService = new PetaPocoMenuService();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.UserName = "管理员";
            return View();

        }

        public ActionResult LeftMenu()
        {
            List<TreeNode> treeNodes = _menuService.GetNavigationMenu();
            var node1 = new TreeNode { Id = "10", ParentId = "1", Text = "应用程序管理-表格编辑", IconClass = "icon-edit", Attributes = new { url = "/systems/application" } };
            var node2 = new TreeNode { Id = "11", ParentId = "1", Text = "应用程序管理-表单编辑", IconClass = "icon-edit", Attributes = new { url = "/systems/application2" } };
            var node11 = new TreeNode { Id = "21", ParentId = "1", Text = "应用程序管理-行内编辑", IconClass = "icon-edit", Attributes = new { url = "/systems/application3" } };
            var node3 = new TreeNode { Id = "12", ParentId = "1", Text = "字典管理", IconClass = "icon-edit", Attributes = new { url = "/commons/dic" } };
            var node4 = new TreeNode { Id = "13", ParentId = "1", Text = "地区管理", IconClass = "icon-edit", Attributes = new { url = "/commons/area" } };
            var node5 = new TreeNode { Id = "14", ParentId = "1", Text = "图标管理", IconClass = "icon-edit", Attributes = new { url = "/commons/icon" } };
            return PartialView("Left", treeNodes);
        }

        public string GetMenuTree(string parentId)
        {
            List<TreeNode> navigationMenuTree = _menuService.GetNavigationMenuTree(parentId);
            

            var node1 = new TreeNode { Id = "10", ParentId = "1", Text = "应用程序管理-表格编辑", IconClass = "icon-edit", Attributes = new { url = "/systems/application" } };
            var node2 = new TreeNode { Id = "11", ParentId = "10", Text = "应用程序管理-表单编辑", IconClass = "icon-edit", Attributes = new { url = "/systems/application2" } };
            var node11 = new TreeNode { Id = "21", ParentId = "1", Text = "应用程序管理-行内编辑", IconClass = "icon-edit", Attributes = new { url = "/systems/application3" } };
            var node3 = new TreeNode { Id = "12", ParentId = "1", Text = "字典管理", IconClass = "icon-edit", Attributes = new { url = "/commons/dic" } };
            var node4 = new TreeNode { Id = "13", ParentId = "1", Text = "地区管理", IconClass = "icon-edit", Attributes = new { url = "/commons/area" } };
            var node5 = new TreeNode { Id = "14", ParentId = "1", Text = "图标管理", IconClass = "icon-edit", Attributes = new { url = "/commons/icon" } };

            List<TreeNode> List = new List<TreeNode>();
            var node = new TreeNode { Id = "1", Text = "系统管理", IconClass = "icon-edit",children=List };
            return new TreeResult(navigationMenuTree).ToString();
        }
    }
}
