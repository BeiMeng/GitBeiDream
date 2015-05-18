using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using PetaPocoORM;

namespace BeiDream.Service.Common
{
    /// <summary>
    /// 修正增改数据的Path和level,todo做成通用的
    /// </summary>
    public class TreeServiceHelper
    {
        #region 字段

        /// <summary>
        /// 需要更新路径的实体列表
        /// </summary>
        private readonly List<BeiDream_Menu> _pathChangeList;

        /// <summary>
        /// 已更新路径Id列表
        /// </summary>
        private readonly List<Guid> _updatedPathIds;

        #endregion
        /// <summary>
        /// 新增列表
        /// </summary>
        protected List<BeiDream_Menu> AddList;
        /// <summary>
        /// 修改列表
        /// </summary>
        protected List<BeiDream_Menu> UpdateList;

        public TreeServiceHelper(List<BeiDream_Menu> addList, List<BeiDream_Menu> updateList)
        {
            this.AddList = addList;
            this.UpdateList = updateList;
            _pathChangeList = new List<BeiDream_Menu>();
            _updatedPathIds = new List<Guid>();
            var parentChangeList = GetParentChanges();
            GetPathChangeList(parentChangeList);
            InitPath();
        }
        /// <summary>
        /// 获取父节点被修改的集合
        /// </summary>
        private List<BeiDream_Menu> GetParentChanges()
        {
            var result = new List<BeiDream_Menu>();
            UpdateList.ForEach(t =>
            {
                var entity = PetaPocoHelper.GetInstance().SingleOrDefault<BeiDream_Menu>(t.MenuId); 
                if (entity == null)
                    return;
                if (t.ParentId.Equals(entity.ParentId))
                    return;
                result.Add(t);
            });
            FilterByPath(result);
            return result;
        }

        /// <summary>
        /// 根据路径过滤，仅保留最顶级节点
        /// </summary>
        protected void FilterByPath(List<BeiDream_Menu> entities)
        {
            entities.Select(t => t.Path)
                .ToList()
                .ForEach(path => entities.RemoveAll(t => t.Path.StartsWith(path) && t.Path != path));
        }

        /// <summary>
        /// 初始化需要更新路径的实体列表
        /// </summary>
        private void GetPathChangeList(List<BeiDream_Menu> parentChangeList)
        {
            AddPathChangeList(parentChangeList);
            AddPathChangeList(AddList);
            foreach (var parent in parentChangeList)
            {
                AddPathChangeList(UpdateList.Where(t => t.Path.StartsWith(parent.Path)).ToList());
                AddPathChangeList(GetAllChilds(parent));
            }
        }
        /// <summary>
        /// 获取全部下级实体
        /// </summary>
        /// <param name="parent">父实体</param>
        protected virtual List<BeiDream_Menu> GetAllChilds(BeiDream_Menu parent)
        {
            Sql sql = new Sql();
            sql.Where("Path like @0", parent.Path + "%");
            sql.Where("MenuId <>@0", parent.MenuId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDream_Menu> list = PetaPocoHelper.GetInstance().Fetch<BeiDream_Menu>(sql);
            return list.Where(t => !t.MenuId.Equals(parent.MenuId)).ToList();
        }
        /// <summary>
        /// 添加需要更新路径的实体列表
        /// </summary>
        private void AddPathChangeList(List<BeiDream_Menu> list)
        {
            list.ForEach(entity =>
            {
                if (_pathChangeList.Any(t => t.MenuId.Equals(entity.MenuId)))
                    return;
                _pathChangeList.Add(entity);
            });
        }
        /// <summary>
        /// 初始化路径
        /// </summary>
        private void InitPath()
        {
            _pathChangeList.ForEach(InitPath);
        }

        /// <summary>
        /// 初始化路径
        /// </summary>
        private void InitPath(BeiDream_Menu entity)
        {
            if (entity == null)
                return;
            if (_updatedPathIds.Contains(entity.MenuId))
                return;
            UpdateParentPath(entity);
            InitEntityPath(entity);
        }

        //更新父节点路径
        private void UpdateParentPath(BeiDream_Menu entity)
        {
            if (Equals(entity.ParentId, null))
                return;
            var parent = _pathChangeList.Find(t => t.MenuId.Equals(entity.ParentId));
            InitPath(parent);
        }

        /// <summary>
        /// 初始化实体路径
        /// </summary>
        private void InitEntityPath(BeiDream_Menu entity)
        {
            //entity.InitPath(GetParent(entity));
            EntityInitPath(GetParent(entity), entity);
            _updatedPathIds.Add(entity.MenuId);
        }
        /// <summary>
        /// 初始化路径
        /// </summary>
        /// <param name="parent">父对象</param>
        public void EntityInitPath(BeiDream_Menu parent,BeiDream_Menu me)
        {
            if (Equals(parent, null))
                InitFirstLevel(me);
            else
                InitChild(parent,me);
        }
        /// <summary>
        /// 初始化1级节点
        /// </summary>
        private void InitFirstLevel(BeiDream_Menu me)
        {
            me.ParentId = null;   //一级节点前台会自动生成"0000-0000-0000-00000000"
            me.Level = 1;
            me.Path = string.Format("{0},", me.MenuId);
        }

        /// <summary>
        /// 初始化下级节点
        /// </summary>
        private void InitChild(BeiDream_Menu parent, BeiDream_Menu me)
        {
            me.Level = parent.Level + 1;
            me.Path = string.Format("{0}{1},", parent.Path, me.MenuId);
        }
        /// <summary>
        /// 获取父节点
        /// </summary>
        private BeiDream_Menu GetParent(BeiDream_Menu entity)
        {
            var result = _pathChangeList.Find(t => t.MenuId.Equals(entity.ParentId));
            return result ?? PetaPocoHelper.GetInstance().SingleOrDefault<BeiDream_Menu>(entity.ParentId);
        }
    }
}
