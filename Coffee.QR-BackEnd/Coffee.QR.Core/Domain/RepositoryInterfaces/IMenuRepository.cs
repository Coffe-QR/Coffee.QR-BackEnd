using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IMenuRepository
    {
        Menu Create(Menu menu);
        List<Menu> GetAll();
        Menu Delete(long menuId);
        List<Menu> GetAllByLocalId(long localId);
        Menu GetById(long menuId);
        bool UpdateMenu(Menu menu);
    }
}
