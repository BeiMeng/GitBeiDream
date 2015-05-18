using System;
using System.Collections.Generic;
using System.Linq;
using BeiDream.Service.Common;
using BeiDream.Service.Dtos.Systems;
using BeiDream.Service.IService;
using PetaPoco;
using PetaPocoORM;
using Util;
using Util.Exceptions;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.Service.PetaPoco.Service
{
    public class PetaPocoMenuService:IMenuService
    {
        public List<MenuViewModel> GetNavigationMenu()
        {
            Sql sql=new Sql();
            sql.Where("Level=@0", 1);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            return menus.Select(menu => menu.ToDto()).ToList();
        }

        public List<MenuViewModel> GetAllChildrenMenuByPath(string parentId)
        {
            List<MenuViewModel> treeNodes = new List<MenuViewModel>();
            Sql sql = new Sql();
            sql.Where("Path like @0", parentId + "%");
            sql.Where("MenuId <>@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            foreach (var menu in menus)
            {
                MenuViewModel treeNode = new MenuViewModel
                {
                    Id = menu.MenuId.ToString(),
                    Text = menu.Text,
                    IconClass = menu.IconClass,
                    ParentId = parentId
                };
                if (menu.Url != null)
                    treeNode.Attributes = new { url = menu.Url };
                treeNodes.Add(menu.ToDto());
            }
            return treeNodes;
        }
        /// <summary>
        /// 根据导航菜单ID获取其下面的菜单树(通过查找物理路径方式)
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
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
                TreeNode treeNode = new TreeNode
                {
                    Id = menu.MenuId.ToString(),
                    Text = menu.Text,
                    IconClass = menu.IconClass,
                    ParentId = menu.ParentId.ToString()
                };
                if (menu.Url != null)
                    treeNode.Attributes = new {url = menu.Url};
                treeNodes.Add(treeNode);
            }
            return treeNodes;
        }
        /// <summary>
        /// 根据导航菜单ID获取其下面的菜单树(通过递归查询子节点方式)
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        public List<ITreeNode> GetNavigationMenuTreeByChildren(string parentId)
        {
            List<ITreeNode> treeNodes = GetNavigationMenuChildrenNodes(parentId);
            return GetTreeChildren(treeNodes);
        }
        /// <summary>
        /// 根据父ID找到其下的导航菜单子节点(只是子节点)
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<ITreeNode> GetNavigationMenuChildrenNodes(string parentId)
        {
            List<ITreeNode> treeNodes = new List<ITreeNode>();
            Sql sql = new Sql();
            sql.Where("ParentId=@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            foreach (var menu in menus)
            {
                TreeNode treeNode = new TreeNode
                {
                    Id = menu.MenuId.ToString(),
                    Text = menu.Text,
                    IconClass = menu.IconClass,
                    ParentId = parentId
                };
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
        /// <summary>
        /// 根据父ID找到其下的菜单管理子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<MenuViewModel> GetMenuManageChildrenNodes(string parentId)
        {
            Sql sql = new Sql();
            sql.Where("ParentId=@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            return menus.Select(menu => menu.ToDto()).ToList();
        }
        /// <summary>
        /// 获取所有的菜单管理树节点
        /// </summary>
        /// <returns></returns>
        public List<MenuViewModel> GetAllTreeNodes()
        {
            Sql sql = new Sql();
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            return menus.Select(menu => menu.ToDto()).ToList();
        }


        #region 新增的一系列操作
        private void AddBefore(MenuViewModel dto)
        {
            BeiDream_Menu beiDreamMenu = dto.ToEntity();
            ValidateAddCodeRepeatAndTextRepeat(beiDreamMenu);
        }
        /// <summary>
        /// 验证编码重复问题
        /// </summary>
        private void ValidateAddCodeRepeatAndTextRepeat(BeiDream_Menu beiDreamMenu)
        {
            Sql sql = new Sql();
            sql.Where("Code=@Code  or Text=@Text", new {beiDreamMenu.Code, beiDreamMenu.Text });
            List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            if (menus == null || menus.Count == 0)
                return;
            else
                throw new Warning(string.Format("菜单 '{0}' 已存在，请修改", "编码"));
        }
        /// <summary>
        /// 新增初始化
        /// </summary>
        /// <param name="addModel"></param>
        private void AddInit(BeiDream_Menu addModel)
        {
            //FixPathAndLevel(addModel);
            addModel.CreatePerson = "BeiDrem";
            addModel.CreateTime = DateTime.Now;
            addModel.PinYin = Str.PinYin(addModel.Text);
        }
        public void Add(BeiDream_Menu beiDreamMenu)
        {
            AddInit(beiDreamMenu);
            PetaPocoHelper.GetInstance().Insert(beiDreamMenu);
        } 
        #endregion

        public void Delete(BeiDream_Menu beiDreamMenu)
        {
            PetaPocoHelper.GetInstance().Delete(beiDreamMenu);
        }
        #region 更新的一系列操作
        private void UpdateBefore(MenuViewModel dto)
        {
            BeiDream_Menu beiDreamMenu = dto.ToEntity();
            ValidateUpdateCodeRepeatAndTextRepeat(beiDreamMenu);
            if (!ValidateVersion(beiDreamMenu))
                throw new ConcurrencyException();
        }
        /// <summary>
        /// 验证编码或菜单名称重复问题
        /// </summary>
        private void ValidateUpdateCodeRepeatAndTextRepeat(BeiDream_Menu beiDreamMenu)
        {
            Sql sqlCode = new Sql();
            sqlCode.Where("Code=@Code and MenuId<>@MenuId", new { Code = beiDreamMenu.Code, MenuId = beiDreamMenu.MenuId });
            List<BeiDream_Menu> menusCode = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sqlCode);
            if (menusCode == null || menusCode.Count == 0)
            {
                Sql sql = new Sql();
                sql.Where("Text=@Text and MenuId<>@MenuId", new { Text = beiDreamMenu.Text, MenuId = beiDreamMenu.MenuId });
                List<BeiDream_Menu> menus = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
                if (menus == null || menus.Count == 0)
                    return;
                else
                    throw new Warning(string.Format("菜单 '{0}' 已存在，请修改", "名称"));
            }
            else
                throw new Warning(string.Format("菜单 '{0}' 已存在，请修改", "编码"));
        }
        /// <summary>
        /// 更新初始化
        /// </summary>
        /// <param name="updateModel"></param>
        private void UpdateInit(BeiDream_Menu updateModel)
        {
            //FixPathAndLevel(updateModel);
            updateModel.UpdatePerson = "BeiDrem";
            updateModel.UpdateTime = DateTime.Now;
            updateModel.PinYin = Str.PinYin(updateModel.Text);
        }
        public void Update(BeiDream_Menu beiDreamMenu)
        {
            UpdateInit(beiDreamMenu);
            PetaPocoHelper.GetInstance().Update(beiDreamMenu);
        } 
        /// <summary>
        /// 版本号,乐观离线锁通过为每行数据添加一个版本号来识别当前数据的版本，在获取数据时将版本号保存下来，
        /// 更新数据时将版本号作为Where中的过滤条件，如果该记录被更新，则版本号会发生变化，所以导致更新数据时影响行数为0，
        /// 通过引发一个并发更新异常让你了解数据已经被别人更新。
        /// </summary>
        //验证版本号
        private bool ValidateVersion(BeiDream_Menu newBeiDreamMenu)
        {
            BeiDream_Menu oldBeiDreamMenu =
                PetaPocoHelper.GetInstance().SingleOrDefault<BeiDream_Menu>(newBeiDreamMenu.MenuId);
            if (newBeiDreamMenu.Version == null)
                return false;
            for (int i = 0; i < oldBeiDreamMenu.Version.Length; i++)
                if (newBeiDreamMenu.Version[i] != oldBeiDreamMenu.Version[i])
                    return false;
            return true;
        }
        #endregion

        #region 修正增改数据的Path和level,目前有BUG
        /// <summary>
        /// 获取父Path
        /// </summary>
        /// <param name="beiDreamMenu"></param>
        /// <returns></returns>
        private String GetParentPath(BeiDream_Menu beiDreamMenu)
        {
            string path = string.Empty;
            string parentId = beiDreamMenu.ParentId.ToString();
            if (!String.IsNullOrEmpty(beiDreamMenu.ParentId.ToString()))
            {
                path = parentId + "," + path;
            }
            string nextParentId = GetNextParentId(parentId);
            while (!string.IsNullOrEmpty(nextParentId))
            {
                path = nextParentId + "," + path;
                nextParentId = GetNextParentId(nextParentId);
            }
            return path;
        }

        private string GetNextParentId(string parentId)
        {
            BeiDream_Menu beiDreamMenu =
  PetaPocoHelper.GetInstance().SingleOrDefault<BeiDream_Menu>((object)parentId);
            return beiDreamMenu != null ? beiDreamMenu.ParentId.ToString() : null;
        }
        /// <summary>
        /// 修正增改数据的Path和level,目前有BUG
        /// </summary>
        /// <param name="beiDreamMenu"></param>
        private void FixPathAndLevel(BeiDream_Menu beiDreamMenu)
        {
            if (String.IsNullOrEmpty(beiDreamMenu.ParentId.ToString()))
            {
                beiDreamMenu.Path = beiDreamMenu.MenuId + ",";
                beiDreamMenu.ParentId = null;
                beiDreamMenu.Level = 1;
            }
            else
            {
                beiDreamMenu.Path = GetParentPath(beiDreamMenu) + beiDreamMenu.MenuId + ",";
                string[] str = beiDreamMenu.Path.Split(',');
                beiDreamMenu.Level = str.Length - 1;
            }
        } 
        #endregion
        /// <summary>
        /// 获取所有前台增删改的数据id
        /// </summary>
        /// <param name="addList"></param>
        /// <param name="updateList"></param>
        /// <param name="deleteList"></param>
        /// <returns></returns>
        private List<string> GetIds(List<BeiDream_Menu> addList, List<BeiDream_Menu> updateList, List<BeiDream_Menu> deleteList)
        {
            List<string> strList= addList.Select(addModel => addModel.MenuId.ToString()).ToList();
            strList.AddRange(updateList.Select(updateModel => updateModel.MenuId.ToString()));
            strList.AddRange(deleteList.Select(deleteModel => deleteModel.MenuId.ToString()));
            return strList;
        }
        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="addList"></param>
        /// <param name="updateList"></param>
        /// <param name="deleteList"></param>
        /// <returns></returns>
        public List<MenuViewModel> Save(List<MenuViewModel> addList, List<MenuViewModel> updateList, List<MenuViewModel> deleteList)
        {
            SaveBefore(addList, updateList, deleteList);
            var _addList = addList.Select(ToEntity).Distinct().ToList();
            var _updateList = updateList.Select(ToEntity).Distinct().ToList();
            var _deleteList = deleteList.Select(ToEntity).Distinct().ToList();
            List<string> ids = GetIds(_addList, _updateList, _deleteList);
            //修正增改数据的Path和level
            new TreeServiceHelper(_addList, _updateList);
            #region 保存操作数据到数据库的事,物
            var dbContext = PetaPocoHelper.GetInstance();
            dbContext.BeginTransaction();
            try
            {
                foreach (var addModel in _addList)
                {
                    AddInit(addModel);
                    dbContext.Insert(addModel);
                }
                foreach (var updateModel in _updateList)
                {
                    UpdateInit(updateModel);                  

                    dbContext.Update(updateModel);
                }
                foreach (var deleteModel in _deleteList)
                {
                    dbContext.Delete<BeiDream_Menu>(deleteModel.MenuId);
                    List<MenuViewModel> childrenMenus = this.GetAllChildrenMenuByPath(deleteModel.MenuId.ToString());
                    if (childrenMenus == null || childrenMenus.Count == 0)
                        continue;
                    else
                    {
                        foreach (var childrenMenu in childrenMenus)
                        {
                            dbContext.Delete<BeiDream_Menu>((object)childrenMenu.Id);
                        }
                    }
                }
                dbContext.CompleteTransaction();
            }
            catch (Exception ex)
            {
                dbContext.AbortTransaction();
                throw new Warning(ex);
            } 
            #endregion
            Sql sql=new Sql();
            foreach (var id in ids)
            {
                sql.WhereOR("MenuId=@0", id);
            }
            List<BeiDream_Menu> returnList= dbContext.Fetch<BeiDream_Menu>(sql);
            return returnList.Select(ToDto).ToList();
        }
        /// <summary>
        /// 保存前操作
        /// </summary>
        /// <param name="addList">新增列表</param>
        /// <param name="updateList">修改列表</param>
        /// <param name="deleteList">删除列表</param>
        private void SaveBefore(List<MenuViewModel> addList, List<MenuViewModel> updateList, List<MenuViewModel> deleteList)
        {
            FilterList(addList, deleteList);
            FilterList(updateList, deleteList);
            addList.ForEach(AddBefore);
            updateList.ForEach(UpdateBefore);
        }
        /// <summary>
        /// 过滤无效数据
        /// </summary>
        private void FilterList(List<MenuViewModel> list, IEnumerable<MenuViewModel> deleteList)
        {
            list.Select(t => t.Id).ToList().ForEach(id =>
            {
                if (deleteList.Any(d => d.Id == id))
                    list.Remove(list.Find(t => t.Id == id));
            });
        }

        /// <summary>
        /// 创建新行，供前台调用
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public MenuViewModel CreatNew(string parentId)
        {
            MenuViewModel newDto = new MenuViewModel
            {
                ParentId = parentId,
                SortId = GetSortId(parentId),
                Id = Guid.NewGuid().ToString(),
                Enabled = true
            };
            return newDto;
        }
        /// <summary>
        /// 获取新增行的此层级的排序id
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private int GetSortId(string parentId)
        {
            Sql sql = new Sql();
            sql.Where(string.IsNullOrEmpty(parentId) ? "ParentId is null" : "ParentId=@0", parentId);
            sql.OrderBy("SortId DESC");   //默认ASC升序，降序为DESC
            BeiDream_Menu beiDreamMenu = PetaPocoHelper.GetInstance().FirstOrDefault<BeiDream_Menu>(sql);
            if (beiDreamMenu == null)
                return 0;
            return beiDreamMenu.SortId + 1;
        }

        #region 数据传输对象Dto和实体Enitiy相互转化的方法
        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        private BeiDream_Menu ToEntity(MenuViewModel dto)
        {
            return dto.ToEntity();
        }
        /// <summary>
        /// 转换为数据传输对象
        /// </summary>
        /// <param name="entity">数据传输对象</param>
        private MenuViewModel ToDto(BeiDream_Menu entity)
        {
            return entity.ToDto();
        } 
        #endregion
    }
}
