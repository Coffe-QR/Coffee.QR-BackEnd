using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IMenuItemRepository
    {
        MenuItem Create(MenuItem item);
        List<MenuItem> GetAll();
        MenuItem Delete(long menuId);
        bool DeleteByMenuIdAndItemId(long menuId, long itemId);
        List<MenuItem> GetAllByMenuId(long menuId);
    }
}
