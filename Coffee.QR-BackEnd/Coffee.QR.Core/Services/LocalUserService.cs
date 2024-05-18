using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class LocalUserService : CrudService<LocalUserDto, LocalUser>, ILocalUserService
    {
        private readonly ILocalUserRepository _localUserRepository;

        public LocalUserService(ICrudRepository<LocalUser> crudRepository, IMapper mapper, ILocalUserRepository localUserRepository)
            : base(crudRepository, mapper)
        {
            _localUserRepository = localUserRepository;
        }

        public Result<LocalUserDto> CreateLocalUser(LocalUserDto localUserDto)
        {
            try
            {
                var localUser = _localUserRepository.Create(new LocalUser(localUserDto.LocalId, localUserDto.UserId));

                LocalUserDto resultDto = new LocalUserDto
                {
                    Id = localUser.Id,
                    LocalId = localUser.LocalId,
                    UserId = localUser.UserId
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<LocalUserDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public bool DeleteLocalUser(long localUserId)
        {
            var localUserToDelete = _localUserRepository.Delete(localUserId);
            return localUserToDelete != null;
        }

        public Result<List<LocalUserDto>> GetAllLocalUsers()
        {
            try
            {
                var localUsers = _localUserRepository.GetAll();
                var localUserDtos = localUsers.Select(i => new LocalUserDto
                {
                    Id = i.Id,
                    LocalId = i.LocalId,
                    UserId = i.UserId
                }).ToList();

                return Result.Ok(localUserDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<LocalUserDto>>("Failed to retrieve localUsers").WithError(e.Message);
            }
        }

        public Result<LocalUserDto> GetByUserId(long userId)
        {
            try
            {
                LocalUser localUser = _localUserRepository.GetByUserId(userId);
                if (localUser != null)
                {
                    LocalUserDto localUserDto = new LocalUserDto
                    {
                        Id = localUser.Id,
                        LocalId = localUser.LocalId,
                        UserId = localUser.UserId
                    };
                    return Result.Ok(localUserDto);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return Result.Fail<LocalUserDto>("Failed to retrieve events").WithError(e.Message);
            }
        }

        public Task<Result<LocalUserDto>> GetLocalUserByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<LocalUserDto>> UpdateLocalUserAsync(LocalUserDto localUserDto)
        {
            throw new NotImplementedException();
        }
    }
}
