using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IStorageService
    {
        Result<StorageDto> CreateStorage(StorageDto storageDto);
        Result<List<StorageDto>> GetAllStorages();
        bool DeleteStorage(long storageId);
        Task<Result<StorageDto>> GetStorageByIdAsync(long id);
        Task<Result<StorageDto>> UpdateStorageAsync(StorageDto storageDto);
    }
}
