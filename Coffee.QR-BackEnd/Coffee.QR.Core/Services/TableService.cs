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
    public class TableService : CrudService<TableDto, Table>, ITableService
    {
        private readonly ITableRepository _tableRepository;

        public TableService(ICrudRepository<Table> crudRepository, IMapper mapper, ITableRepository tableRepository)
            : base(crudRepository, mapper)
        {
            _tableRepository = tableRepository;
        }

        public Result<TableDto> CreateTable(TableDto tableDto)
        {
            try
            {
                var tablet = _tableRepository.Create(new Table(tableDto.Name, tableDto.Capacity, tableDto.IsSmokingArea, tableDto.LocalId));

                TableDto resultDto = new TableDto
                {
                    Id = tablet.Id,
                    Name = tablet.Name,
                    Capacity = tablet.Capacity,
                    IsSmokingArea = tablet.IsSmokingArea,
                    LocalId = tablet.LocalId,
                };

                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<TableDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<TableDto>> GetAllTables()
        {
            try
            {
                var tables = _tableRepository.GetAll();
                var tableDtos = tables.Select(t => new TableDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Capacity = t.Capacity,
                    IsSmokingArea = t.IsSmokingArea,
                    LocalId = t.LocalId,
                }).ToList();

                return Result.Ok(tableDtos);
            }
            catch (Exception e)
            {
                return Result.Fail<List<TableDto>>("Failed to retrieve tables").WithError(e.Message);
            }
        }

        public Result<TableDto> GetById(long tableId)
        {
            try
            {
                Table table = _tableRepository.GetById(tableId);
                if (table != null)
                {
                    TableDto tableDto = new TableDto
                    {
                        Id = table.Id,
                        Name = table.Name,
                        Capacity = table.Capacity,
                        IsSmokingArea = table.IsSmokingArea,
                        LocalId = table.LocalId
                    };
                    return Result.Ok(tableDto);
                }
                else 
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return Result.Fail<TableDto>("Failed to retrieve table").WithError(e.Message);
            }
        }


        public bool DeleteTable(long tableId)
        {
            var tableToDelete = _tableRepository.Delete(tableId);
            return tableToDelete != null;
        }
    }
}
