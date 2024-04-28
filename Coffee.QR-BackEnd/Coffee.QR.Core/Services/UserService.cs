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
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class UserService : CrudService<UserDto, User>, IUserService
    {
        private readonly IUserRepository UserRepository;

        public UserService(ICrudRepository<User> crudRepository, IMapper mapper, IUserRepository userRepository) : base(crudRepository,mapper)
        {
            UserRepository = userRepository;
        }
        public Result<UserDto> GetById(long userId)
        {
            User user = UserRepository.GetById(userId);
            return MapToDto(user);
        }


    }
}
