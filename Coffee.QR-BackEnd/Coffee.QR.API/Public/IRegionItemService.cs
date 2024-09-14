using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IRegionItemService
    {
        Result<RegionItemDto> CreateRegionItem(RegionItemDto regionItemDto);
        Result<List<RegionItemDto>> GetAllRegionItems();
        Result<List<ItemDto>> GetAllForRegion(long regionId);
        Result<List<ItemDto>> GetAllNotOnRegion(long regionId);
        bool DeleteRegionItem(long regionItemId);
        bool DeleteByRegionIdAndItemId(long regionId, long itemId);
    }
}
