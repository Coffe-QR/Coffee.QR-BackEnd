using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class UserService : CrudService<UserDto, User>, IUserService
    {
        private readonly IUserRepository UserRepository;
        private readonly ILocalUserRepository LocalUserRepository;

        public UserService(ICrudRepository<User> crudRepository, IMapper mapper, IUserRepository userRepository, ILocalUserRepository localUserRepository) : base(crudRepository,mapper)
        {
            UserRepository = userRepository;
            LocalUserRepository = localUserRepository;
        }
        public Result<UserDto> GetById(long userId)
        {
            User user = UserRepository.GetById(userId);
            return MapToDto(user);
        }

        
        public Result<List<UserDto>> GetAllManagers()
        {
            var managers = UserRepository.GetAllManagers();
            var managerDtos = managers.Select(MapToDto).ToList();

            //var allLU = localUserRepository.GetAll();
            //var postoje = allLU[2].ToString();

            return Result.Ok(managerDtos);
        }

        public Result<List<UserDto>> GetAllManagersNotInLocalUser()
        {
            var managers = UserRepository.GetAllManagers();
            var localUserIds = LocalUserRepository.GetAll().Select(l => l.UserId).ToList();

            var filteredManagers = managers.Where(manager => !localUserIds.Contains(manager.Id)).ToList();
            var managerDtos = filteredManagers.Select(MapToDto).ToList();

            return Result.Ok(managerDtos);
        }

    }
}
