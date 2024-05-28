using Coffee.QR.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface ILocalRepository
    {
        Local Create(Local local);
        List<Local> GetAll();
        Local Delete(long localId);
        Task<Local> GetByIdAsync(long id);
        Local GetById(long localId);
    }
}
