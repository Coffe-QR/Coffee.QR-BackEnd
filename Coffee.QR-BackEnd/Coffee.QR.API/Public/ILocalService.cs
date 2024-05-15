using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface ILocalService
    {
        Result<LocalDto> CreateLocal(LocalDto localDto);
        Result<List<LocalDto>> GetAllLocals();
        bool DeleteLocal(long localId);
        Task<LocalDto> GetByIdAsync(long id);
    }
}
