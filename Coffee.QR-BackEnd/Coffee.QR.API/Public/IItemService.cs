using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IItemService
    {
        Result<ItemDto> CreateItem(ItemDto itemDto);
        Result<List<ItemDto>> GetAllItems();
        bool DeleteItem(long itemId);
        Result<List<ItemDto>> GetAllForStorage(long storageId);
        bool UpdateItem(ItemDto newItem);
        Result<ItemDto> GetById(long itemId);
    }
}
