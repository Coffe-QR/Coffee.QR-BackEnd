using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IStorageItemRepository
    {
        StorageItem Create(StorageItem storage);
        List<StorageItem> GetAll();
        StorageItem Delete(long storageId);

    }
}
