using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPocoORM;
using Util;

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
                Url=entity.Url,
                IconClass=entity.IconClass,
                iconCls=entity.IconClass,
                PinYin=entity.PinYin,
                Enabled=entity.Enabled,
                UpdateTime=entity.UpdateTime,
                Version=entity.Version
            };
            return menuViewModel;
        }
        public static BeiDream_Menu ToEntity(this MenuViewModel dto)
        {
            BeiDream_Menu beiDreamMenuModel = new BeiDream_Menu
            {
                MenuId = dto.Id.ToGuid(),
                ParentId = dto.ParentId.ToGuid(),
                Code = dto.Code,
                Text = dto.Text,
                Path = dto.Path,
                Level = Conv.ToInt(dto.Level),
                SortId = Conv.ToInt(dto.SortId),                
                Url=dto.Url,
                IconClass = dto.IconClass,
                PinYin = dto.PinYin,
                Enabled=dto.Enabled,
                Version=dto.Version
            };
            return beiDreamMenuModel;
        }
    }
}
