using BeiDream.EasyUi.Common;
using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Mvc;
using BeiDream.EasyUi.Areas.Systems.ViewModel;
using BeiDream.Service.IService;
using BeiDream.Service.PetaPoco.Service;
using Util.Webs.EasyUi.Results;
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
            List<ITreeNode> treeNodes;
            if (query.Id == null)
            {
                treeNodes = _menuService.GetNavigationMenu();
                List<MenuViewModel> liss=new List<MenuViewModel>();
                foreach (var treeNode in treeNodes)
                {
                    treeNode.state = "closed";
                    liss.Add(Mapper(treeNode));
                }
                //PagedList<ITreeNode> result = new PagedList<ITreeNode>(treeNodes, query.Page, query.Rows);
                PagedList<MenuViewModel> result = new PagedList<MenuViewModel>(liss, query.Page, query.Rows);
                return new TreeGridResult(result,true,result.TotalItemCount).GetResult();
            }
            else
            {
                treeNodes = _menuService.GetNavigationMenuChildrenNodes(query.Id);
                List<MenuViewModel> liss = new List<MenuViewModel>();
                foreach (var treeNode in treeNodes)
                {
                    treeNode.state = "closed";
                    liss.Add(Mapper(treeNode));
                }
                return new TreeGridResult(liss, true, -1).GetResult();
            }

        }

        private MenuViewModel Mapper(ITreeNode model)
        {
            MenuViewModel menuViewModel=new MenuViewModel();
            menuViewModel.Attributes = model.Attributes;
            menuViewModel.Checked = model.Checked;
            menuViewModel.IconClass = model.IconClass;
            menuViewModel.iconCls = model.IconClass;
            menuViewModel.Id = model.Id;
            menuViewModel.ParentId = model.ParentId;
            menuViewModel.Text = model.Text;
            menuViewModel.children = model.children;
            menuViewModel.state = model.state;
            menuViewModel.Level = model.Level;
            return menuViewModel;
        }
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
