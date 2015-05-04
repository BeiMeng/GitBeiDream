﻿using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 布局选项
    /// </summary>
    public class LayoutOption : OptionBase<ILayoutOption>, ILayoutOption {
        /// <summary>
        /// 初始化布局选项
        /// </summary>
        public LayoutOption()
            : this( false ) {
        }

        /// <summary>
        /// 初始化布局选项
        /// </summary>
        /// <param name="fit">自适应布局</param>
        public LayoutOption( bool fit ) {
            AddClass( "easyui-layout" );
            if ( fit )
                AddDataOption( "fit", true );
        }
    }
}
