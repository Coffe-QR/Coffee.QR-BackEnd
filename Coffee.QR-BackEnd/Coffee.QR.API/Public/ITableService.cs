using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface ITableService
    {
        Result<TableDto> CreateTable(TableDto tableDto);
        Result<List<TableDto>> GetAllTables();
        bool DeleteTable(long tableId);
        Result<TableDto> GetById(long tableId);
    }
}
