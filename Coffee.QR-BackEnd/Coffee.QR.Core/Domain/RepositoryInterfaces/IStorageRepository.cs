using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IStorageRepository
    {
        Storage Create (Storage storage);
        List<Storage> GetAll ();
        Storage Delete(long storageId);
    }
}
