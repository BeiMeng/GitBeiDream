using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPocoORM;

namespace BeiDream.Service.Dtos.Systems
{
    public static class MenuViewModelExtension
    {
        public static MenuViewModel ToDto(this BeiDream_Menu entity)
        {
            MenuViewModel menuViewModel = new MenuViewModel
            {
                Id=entity.MenuId.ToString(),
                ParentId=entity.ParentId.ToString(),
                Code=entity.Code,
                Text=entity.Text,
                Path=entity.Path,
                Level=entity.Level,
                SortId=entity.SortId,
                Attributes = entity.Url == null ? null : new { url = entity.Url },
                IconClass=entity.IconClass,
                iconCls=entity.IconClass,
                PinYin=entity.PinYin,
                UpdateTime=entity.UpdateTime
            };
            return menuViewModel;
        }
    }
}
