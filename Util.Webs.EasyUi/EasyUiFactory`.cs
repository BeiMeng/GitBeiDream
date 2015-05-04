using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Util.Webs.EasyUi.Forms.Comboxs;
using Util.Webs.EasyUi.Forms.TextBoxs;
using Util.Webs.EasyUi.Grids;
using Util.Webs.EasyUi.Services;

namespace Util.Webs.EasyUi {
    /// <summary>
    /// EasyUi工厂
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class EasyUiFactory<TEntity>{
        /// <summary>
        /// 创建EasyUi服务
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        public static IEasyUiService<TEntity> CreateEasyUiService( HtmlHelper<TEntity> helper ) {
            return new EasyUiService<TEntity>( helper );
        }

        /// <summary>
        /// 创建文本框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="helper">HtmlHelper</param>
        public static ITextBox CreateTextBox<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, HtmlHelper<TEntity> helper ) {
            return new EntityTextBox<TEntity, TProperty>( propertyExpression, helper );
        }

        /// <summary>
        /// 创建组合框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="helper">HtmlHelper</param>
        public static ICombox CreateCombox<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, HtmlHelper<TEntity> helper ) {
            return new EntityCombox<TEntity, TProperty>( propertyExpression, helper );
        }

        /// <summary>
        /// 创建表格列
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="isEdit">是否允许编辑</param>
        public static IDataGridColumn CreateDataGridColumn<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, HtmlHelper<TEntity> helper, bool isEdit ) {
            return new EntityDataGridColumn<TEntity, TProperty>( propertyExpression, helper, isEdit );
        }

        /// <summary>
        /// 创建子表格
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">集合属性,范例：t => t.Users</param>
        /// <param name="funcCreateColumns">创建列集合方法</param>
        public static ISubDataGrid CreateSubGrid<TProperty>( Expression<Func<TEntity, IEnumerable<TProperty>>> expression, Func<TProperty, ISubGridColumn[]> funcCreateColumns ) {
            return new EntitySubGrid<TEntity, TProperty>( expression, funcCreateColumns );
        }

        /// <summary>
        /// 创建子表格列
        /// </summary>
        /// <param name="expression">表达式</param>
        public static ISubGridColumn CreateSubGridColumn<TProperty>( Expression<Func<TProperty>> expression ) {
            return new EntitySubGridColumn<TProperty>( expression );
        }
    }
}
