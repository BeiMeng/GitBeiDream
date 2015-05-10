using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.Service.IService
{
    public interface IMenuService
    {
        /// <summary>
        /// 获取导航菜单,也就是第一级（Level=1）菜单
        /// </summary>
        /// <returns></returns>
        List<TreeNode> GetNavigationMenu();
        /// <summary>
        /// 根据导航菜单ID获取其下面的菜单树
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        List<TreeNode> GetNavigationMenuTree(string parentId);
    }
}
