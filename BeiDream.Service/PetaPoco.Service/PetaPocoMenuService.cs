﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Service.IService;
using PetaPoco;
using PetaPocoORM;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.Service.PetaPoco.Service
{
    public class PetaPocoMenuService:IMenuService
    {
        public List<TreeNode> GetNavigationMenu()
        {
            List<TreeNode> treeNodes=new List<TreeNode>();
            Sql sql=new Sql();
            sql.Where("Level=@0", 1);
            List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            foreach (var menu in menus)
            {
                TreeNode treeNode=new TreeNode();
                treeNode.Id = menu.MenuId.ToString();
                treeNode.Text = menu.Text;
                treeNode.IconClass = menu.IconClass;
                treeNodes.Add(treeNode);
            }
            return treeNodes;
        }


        public List<TreeNode> GetNavigationMenuTree(string parentId)
        {
            List<TreeNode> treeNodes = new List<TreeNode>();
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
    }
}