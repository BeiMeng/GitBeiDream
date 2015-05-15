using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Service.Dtos.Systems;
using BeiDream.Service.IService;
using PetaPoco;
using PetaPocoORM;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.Service.PetaPoco.Service
{
    public class PetaPocoMenuService:IMenuService
    {
        public List<MenuViewModel> GetNavigationMenu()
        {
            List<MenuViewModel> treeNodes = new List<MenuViewModel>();
            Sql sql=new Sql();
            sql.Where("Level=@0", 1);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            foreach (var menu in menus)
            {

                treeNodes.Add(menu.ToDto());
            }
            return treeNodes;
        }


        public List<ITreeNode> GetNavigationMenuTreeByPath(string parentId)
        {
            List<ITreeNode> treeNodes = new List<ITreeNode>();
            Sql sql = new Sql();
            sql.Where("Path like @0", parentId+"%");
            sql.Where("MenuId <>@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            foreach (var menu in menus)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Id = menu.MenuId.ToString();
                treeNode.Text = menu.Text;
                treeNode.IconClass = menu.IconClass;
                treeNode.ParentId = parentId;
                if (menu.Url != null)
                    treeNode.Attributes = new {url = menu.Url};
                treeNodes.Add(treeNode);
            }
            return treeNodes;
        }


        public List<ITreeNode> GetNavigationMenuTreeByChildren(string parentId)
        {
            List<ITreeNode> treeNodes = GetNavigationMenuChildrenNodes(parentId);
            return GetTreeChildren(treeNodes);
        }

        public List<ITreeNode> GetNavigationMenuChildrenNodes(string parentId)
        {
            List<ITreeNode> treeNodes = new List<ITreeNode>();
            Sql sql = new Sql();
            sql.Where("ParentId=@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            foreach (var menu in menus)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Id = menu.MenuId.ToString();
                treeNode.Text = menu.Text;
                treeNode.IconClass = menu.IconClass;
                treeNode.ParentId = parentId;
                if (menu.Url != null)
                    treeNode.Attributes = new { url = menu.Url };
                treeNodes.Add(treeNode);
            }
            return treeNodes;
        }

        private List<ITreeNode> GetTreeChildren(List<ITreeNode> treeList)
        {
            foreach (var treeNode in treeList)
            {
                List<ITreeNode> childrenTreeNodes = GetNavigationMenuChildrenNodes(treeNode.Id);
                treeNode.children = childrenTreeNodes;
                if (treeList.Count > 0)
                {
                    GetTreeChildren(childrenTreeNodes);
                }
            }
            return treeList;
        }

        public List<MenuViewModel> GetMenuManageChildrenNodes(string parentId)
        {
            List<MenuViewModel> treeNodes = new List<MenuViewModel>();
            Sql sql = new Sql();
            sql.Where("ParentId=@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            foreach (var menu in menus)
            {

                treeNodes.Add(menu.ToDto());
            }
            return treeNodes;
        }


        public List<MenuViewModel> GetAllTreeNodes()
        {
            List<MenuViewModel> treeNodes = new List<MenuViewModel>();
            Sql sql = new Sql();
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            foreach (var menu in menus)
            {

                treeNodes.Add(menu.ToDto());
            }
            return treeNodes;
        }
    }
}
