using BeiDream.EasyUi.Common;
using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Mvc;
using BeiDream.EasyUi.Areas.Systems.ViewModel;
using BeiDream.Service.IService;
using BeiDream.Service.PetaPoco.Service;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.EasyUi.Areas.Systems.Controllers
{
    public class MenuManageController : Controller
    {
        //todo IOC依赖注入，Autofac
        readonly IMenuService _menuService = new PetaPocoMenuService();
        //
        // GET: /Systems/MenuManage/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Query(QueryModel query)
        {
            List<ITreeNode> treeNodes = _menuService.GetNavigationMenu();
            foreach (var treeNode in treeNodes)
            {
                treeNode.state = "closed";
            }
            PagedList<ITreeNode> result = new PagedList<ITreeNode>(treeNodes, query.Page, query.Rows);
            return Json(new TreeGridResult { rows = result, total = result.TotalItemCount });
        }
    }

    public class TreeGridResult
    {
        public PagedList<ITreeNode> rows { get; set; }
        public int total { get; set; }
    }

    public class QueryModel
    {
        public string Id { get; set; }
        public int Page { get; set; }
        public int Rows { get; set; }
        public string Order { get; set; }
        public string Sort { get; set; }
    }

}
