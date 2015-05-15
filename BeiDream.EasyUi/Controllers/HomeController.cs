using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeiDream.Service;
using BeiDream.Service.Dtos.Systems;
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
            List<MenuViewModel> treeNodes = _menuService.GetNavigationMenu();
            return PartialView("Left", treeNodes);
        }

        public string GetMenuTree(string parentId)
        {
            //通过查找物理路径方式得到子孙节点
            //List<ITreeNode> navigationMenuTree = _menuService.GetNavigationMenuTreeByPath(parentId);
            //通过递归查询子节点方式,与上面的传递到前台数据不同，上面以ParentID定位子孙节点，此以children定位子节点
            List<ITreeNode> navigationMenuTree = _menuService.GetNavigationMenuTreeByChildren(parentId);
            return new TreeResult(navigationMenuTree).ToString();
        }
    }
}
