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
    public class LocalService : CrudService<LocalDto, Local>, ILocalService
    {
        private readonly ILocalRepository _localRepository;
        public LocalService(ICrudRepository<Local> crudRepository, IMapper mapper, ILocalRepository localRepository)
            : base(crudRepository, mapper)
        {
            _localRepository = localRepository;
        }

        public Result<LocalDto> CreateLocal(LocalDto localDto)
        {
            try
            {
                var localt = _localRepository.Create(new Local(localDto.Name, localDto.City, localDto.DateOfStartingPartnership, localDto.IsActive));

                LocalDto resultDto = new LocalDto
                {
                    Id = localt.Id,
                    Name = localt.Name,
                    City = localt.City,
                    DateOfStartingPartnership = localt.DateOfStartingPartnership,
                    IsActive = localt.IsActive,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<LocalDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<List<LocalDto>> GetAllLocals()
        {
            try
            {
                var locals = _localRepository.GetAll();
                var localDtos = locals.Select(l => new LocalDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    City = l.City,
                    DateOfStartingPartnership = l.DateOfStartingPartnership,
                    IsActive = l.IsActive,
                }).ToList();

                return Result.Ok(localDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<LocalDto>>("Failed to retrieve locals").WithError(e.Message);
            }
        }

        public bool DeleteLocal(long localId)
        {
            var localToDelete = _localRepository.Delete(localId);
            return localToDelete != null;
        }


        public async Task<LocalDto> GetByIdAsync(long id)
        {
            var local = await _localRepository.GetByIdAsync(id);
            if (local == null)
                return null;

            return new LocalDto
            {
                Id = local.Id,
                Name = local.Name,
                City = local.City,
                DateOfStartingPartnership = local.DateOfStartingPartnership,
                IsActive = local.IsActive
            };
        }

    }
}
