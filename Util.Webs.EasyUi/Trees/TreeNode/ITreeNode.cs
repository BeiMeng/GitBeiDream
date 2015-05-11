using System.Collections.Generic;

namespace Util.Webs.EasyUi.Trees {
    /// <summary>
    /// 树节点
    /// </summary>
    public interface ITreeNode {
        /// <summary>
        /// 标识
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// 父标识
        /// </summary>
        string ParentId { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        string Text { get; set; }
        /// <summary>
        ///图标
        /// </summary>
        string IconClass { get; set; }
        /// <summary>
        /// 级数
        /// </summary>
        int? Level { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        bool? Checked { get; set; }
        /// <summary>
        /// 自定义扩展属性
        /// </summary>
        object Attributes { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        string state { get; set; }
        /// <summary>
        /// 子节点集合
        /// </summary>
        List<ITreeNode> children { get; set; }
    }
}
