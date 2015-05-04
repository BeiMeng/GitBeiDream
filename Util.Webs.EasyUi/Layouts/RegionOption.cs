using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 区域面板选项
    /// </summary>
    public class RegionOption : OptionBase<IRegionOption>, IRegionOption {
        /// <summary>
        /// 设置为顶部区域
        /// </summary>
        public IRegionOption Top() {
            return AddDataOption( "region", "'north'" );
        }

        /// <summary>
        /// 设置为底部区域
        /// </summary>
        public IRegionOption Bottom() {
            return AddDataOption( "region", "'south'" );
        }

        /// <summary>
        /// 设置为左侧区域
        /// </summary>
        public IRegionOption Left() {
            return AddDataOption( "region", "'west'" );
        }

        /// <summary>
        /// 设置为右侧区域
        /// </summary>
        public IRegionOption Right() {
            return AddDataOption( "region", "'east'" );
        }

        /// <summary>
        /// 设置为中间内容区域
        /// </summary>
        public IRegionOption Center() {
            return AddDataOption( "region", "'center'" );
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title">标题</param>
        public IRegionOption Title( string title ) {
            return AddDataOption( "title", title, true );
        }

        /// <summary>
        /// 显示边框
        /// </summary>
        /// <param name="isShow">是否显示边框，默认为显示</param>
        public IRegionOption Border( bool isShow = true ) {
            return AddDataOption( "border", isShow );
        }

        /// <summary>
        /// 显示分隔条
        /// </summary>
        public IRegionOption Split() {
            return AddDataOption( "split", true );
        }

        /// <summary>
        /// 设置图标
        /// </summary>
        /// <param name="iconClass">图标class</param>
        public IRegionOption Icon( string iconClass ) {
            return AddDataOption( "iconCls", iconClass, true );
        }

        /// <summary>
        /// 设置Url
        /// </summary>
        /// <param name="url">Url</param>
        public IRegionOption Href( string url ) {
            return AddDataOption( "href", url, true );
        }

        /// <summary>
        /// 允许折叠
        /// </summary>
        /// <param name="collapsible">是否允许折叠</param>
        public IRegionOption Collapsible( bool collapsible = true ) {
            return AddDataOption( "collapsible", collapsible );
        }

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="isPercent">是否百分比</param>
        public override IRegionOption Width( int? width, bool isPercent = false ) {
            return base.Width( width, isPercent ).AddDataOption( "minWidth", width.ToStr() );
        }

        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="height">高度</param>
        public override IRegionOption Height( int height ) {
            return base.Height( height ).AddDataOption( "minHeight", height.ToString() );
        }

        /// <summary>
        /// 设置最小宽度
        /// </summary>
        /// <param name="minWidth">最小宽度</param>
        public IRegionOption MinWidth( int minWidth ) {
            return Width( minWidth );
        }

        /// <summary>
        /// 设置最小高度
        /// </summary>
        /// <param name="minHeight">最小高度</param>
        public IRegionOption MinHeight( int minHeight ) {
            return Height( minHeight );
        }

        /// <summary>
        /// 设置最大宽度
        /// </summary>
        /// <param name="maxWidth">最大宽度</param>
        public IRegionOption MaxWidth( int maxWidth ) {
            return AddDataOption( "maxWidth", maxWidth.ToString() );
        }

        /// <summary>
        /// 设置最大高度
        /// </summary>
        /// <param name="maxHeight">最大高度</param>
        public IRegionOption MaxHeight( int maxHeight ) {
            return AddDataOption( "maxHeight", maxHeight.ToString() );
        }
    }
}
