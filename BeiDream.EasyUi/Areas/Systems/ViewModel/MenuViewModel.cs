using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.EasyUi.Areas.Systems.ViewModel
{

    public class MenuViewModel
    {
        [DataMember]
        public string Id { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        [Required(ErrorMessage = "文本不能为空")]
        [StringLength(50, ErrorMessage = "文本输入过长，不能超过50位")]
        [Display(Name = "文本")]
        [DataMember]
        public string Text { get; set; }
        /// <summary>
        /// 拼音简码
        /// </summary>
        [Required(ErrorMessage = "拼音简码不能为空")]
        [StringLength(200, ErrorMessage = "拼音简码输入过长，不能超过200位")]
        [Display(Name = "拼音简码")]
        [DataMember]
        public string IconClass { get; set; }
        [DataMember]
        public string state { get; set; }
    }
}