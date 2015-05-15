using BeiDream.EasyUi.Common;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using BeiDream.Service.Dtos.Systems;
using BeiDream.Service.IService;
using BeiDream.Service.PetaPoco.Service;
using Util;
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
            const LoadMode loadMode = LoadMode.Sync;
            switch (loadMode)
            {
                    case LoadMode.Async:
                        return AsyncQueryLoad(query);
                    case LoadMode.Sync:
                        return SyncQueryLoad(query);
                    default:
                        return AsyncQueryLoad(query);
            }
        }
        /// <summary>
        /// 同步一次性加载
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private ActionResult SyncQueryLoad(QueryModel query)
        {
            List<MenuViewModel> treeNodes = _menuService.GetAllTreeNodes();
            //下面这段执行则节点不展开
            //foreach (var treeNode in treeNodes)
            //{
            //    treeNode.state = "closed";
            //}
            PagedList<MenuViewModel> result = new PagedList<MenuViewModel>(treeNodes, query.Page, query.Rows);
            return new TreeGridResult(result, true, result.TotalItemCount).GetResult();
        }
        /// <summary>
        /// 异步，一次只加载一级节点
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private ActionResult AsyncQueryLoad(QueryModel query)
        {
            List<MenuViewModel> treeNodes;
            if (query.Id == null)
            {
                treeNodes = _menuService.GetNavigationMenu();
                foreach (var treeNode in treeNodes)
                {
                    treeNode.state = "closed";
                }
                PagedList<MenuViewModel> result = new PagedList<MenuViewModel>(treeNodes, query.Page, query.Rows);
                return new TreeGridResult(result, true, result.TotalItemCount).GetResult();
            }
            else
            {
                treeNodes = _menuService.GetMenuManageChildrenNodes(query.Id);
                foreach (var treeNode in treeNodes)
                {
                    treeNode.state = "closed";
                }
                return new TreeGridResult(treeNodes, true, -1).GetResult();
            }
        }

        public ActionResult Save(string addList, string updateList, string deleteList)
        {
            var listAdd = Util.Json.ToObject<List<MenuViewModel>>(addList);
            var listUpdate = Util.Json.ToObject<List<MenuViewModel>>(updateList);
            var listDelete = Util.Json.ToObject<List<MenuViewModel>>(deleteList);
            //var data = Service.Save(listAdd, listUpdate, listDelete);
            //return Ok(R.SaveSuccess, data);
            return Json("OK");
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
