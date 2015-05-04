﻿namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 布局
    /// </summary>
    public interface ILayout {
        /// <summary>
        /// 布局选项
        /// </summary>
        /// <param name="fit">自适应布局</param>
        ILayoutOption Options( bool fit = false );
        /// <summary>
        /// 区域面板选项
        /// </summary>
        IRegionOption Region();
        /// <summary>
        /// 面板选项
        /// </summary>
        IPanelOption Panel();
    }
}
