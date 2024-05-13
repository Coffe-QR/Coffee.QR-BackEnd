using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface  IStorageItemService
    {
        Result<StorageItemDto> CreateStorageItem(StorageItemDto storageItemDto);
        Result<List<StorageItemDto>> GetAllStorageItems();
        bool DeleteStorageItem(long storageItemId);
        Task<Result<StorageItemDto>> GetStorageItemByIdAsync(long id);
        Task<Result<StorageItemDto>> UpdateStorageItemAsync(StorageItemDto storageItemDto);
    }
}
