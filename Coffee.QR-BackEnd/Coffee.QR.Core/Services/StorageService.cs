using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class StorageService : CrudService<StorageDto, Storage>, IStorageService 
    {
        private readonly IStorageRepository _storageRepository;


        public StorageService(ICrudRepository<Storage> crudRepository, IMapper mapper, IStorageRepository storageRepository)
            : base(crudRepository, mapper)
        {
            _storageRepository = storageRepository;
        }

        public Result<StorageDto> CreateStorage(StorageDto storageDto)
        {
            try
            {
                var storaget = _storageRepository.Create(new Storage(storageDto.CompanyId));

                StorageDto resultDto = new StorageDto
                {
                    Id = storaget.Id,
                    CompanyId = storaget.LocalId,
                    
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<StorageDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<StorageDto>> GetAllStorages()
        {
            try
            {
                var storages = _storageRepository.GetAll();
                var storageDtos = storages.Select(s => new StorageDto
                {
                    Id = s.Id,
                    CompanyId = s.LocalId,
                }).ToList();

                return Result.Ok(storageDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<StorageDto>>("Failed to retrieve storages").WithError(e.Message);
            }
        }


        public bool DeleteStorage(long storageId)
        {
            var storageToDelete = _storageRepository.Delete(storageId);
            return storageToDelete != null;
        }


        public Task<Result<StorageDto>> GetStorageByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<StorageDto>> UpdateStorageAsync(StorageDto storageDto)
        {
            throw new NotImplementedException();
        }
    }
}
