namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 面板选项
    /// </summary>
    public class PanelOption : RegionOption , IPanelOption{
        /// <summary>
        /// 初始化面板选项
        /// </summary>
        public PanelOption() {
            AddClass( "easyui-panel" );
        }
    }
}
