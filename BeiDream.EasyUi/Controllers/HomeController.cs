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
            List<ITreeNode> treeNodes = _menuService.GetNavigationMenu();
            //var node1 = new TreeNode { Id = "10", ParentId = "1", Text = "应用程序管理-表格编辑", IconClass = "icon-edit", Attributes = new { url = "/systems/application" } };
            //var node2 = new TreeNode { Id = "11", ParentId = "1", Text = "应用程序管理-表单编辑", IconClass = "icon-edit", Attributes = new { url = "/systems/application2" } };
            //var node11 = new TreeNode { Id = "21", ParentId = "1", Text = "应用程序管理-行内编辑", IconClass = "icon-edit", Attributes = new { url = "/systems/application3" } };
            //var node3 = new TreeNode { Id = "12", ParentId = "1", Text = "字典管理", IconClass = "icon-edit", Attributes = new { url = "/commons/dic" } };
            //var node4 = new TreeNode { Id = "13", ParentId = "1", Text = "地区管理", IconClass = "icon-edit", Attributes = new { url = "/commons/area" } };
            //var node5 = new TreeNode { Id = "14", ParentId = "1", Text = "图标管理", IconClass = "icon-edit", Attributes = new { url = "/commons/icon" } };
            return PartialView("Left", treeNodes);
        }

        public string GetMenuTree(string parentId)
        {
            //通过查找物理路径方式得到子孙节点
            //List<ITreeNode> navigationMenuTree = _menuService.GetNavigationMenuTreeByPath(parentId);
            //通过递归查询子节点方式,与上面的传递到前台数据不同，上面以ParentID定位子孙节点，此以children定位子节点
            List<ITreeNode> navigationMenuTree = _menuService.GetNavigationMenuTreeByChildren(parentId);

            //var node1 = new TreeNode { Id = "10", Text = "应用程序管理-表格编辑", IconClass = "icon-edit", Attributes = new { url = "/systems/application" } };
            //var node2 = new TreeNode { Id = "11",  Text = "应用程序管理-表单编辑", IconClass = "icon-edit", Attributes = new { url = "/systems/application2" } };
            //var node11 = new TreeNode { Id = "21", ParentId = "1", Text = "应用程序管理-行内编辑", IconClass = "icon-edit", Attributes = new { url = "/systems/application3" } };
            //var node3 = new TreeNode { Id = "12", ParentId = "1", Text = "字典管理", IconClass = "icon-edit", Attributes = new { url = "/commons/dic" } };
            //var node4 = new TreeNode { Id = "13", ParentId = "1", Text = "地区管理", IconClass = "icon-edit", Attributes = new { url = "/commons/area" } };
            //var node5 = new TreeNode { Id = "14", ParentId = "1", Text = "图标管理", IconClass = "icon-edit", Attributes = new { url = "/commons/icon" } };

            //List<ITreeNode> List = new List<ITreeNode>();
            //List.Add(node1);
            //List.Add(node2);
            //var node = new TreeNode { Id = "1", Text = "系统管理", IconClass = "icon-edit",children=List };
            return new TreeResult(navigationMenuTree).ToString();
        }
    }
}
