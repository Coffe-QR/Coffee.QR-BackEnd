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
    public class FrequencyService : BaseService<FrequencyDto, Frequency>, IFrequencyService
    {
        private readonly IFrequencyRepository _frequencyRepository;
        public FrequencyService(ICrudRepository<Item> crudRepository, IMapper mapper, IFrequencyRepository frequencyRepository)
            : base(mapper)
        {
            _frequencyRepository = frequencyRepository;
        }

        public Result<FrequencyDto> Create(FrequencyDto item)
        {
            try
            {
                Frequency frequency = MapToDomain(item);

                frequency = _frequencyRepository.Create(frequency);

                return MapToDto(frequency);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<FrequencyDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<List<FrequencyDto>> GetAll()
        {
            return MapToDto(_frequencyRepository.GetAll());
        }
    }
}