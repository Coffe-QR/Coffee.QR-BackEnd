using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IMenuRegionRepository
    {
        MenuRegion Create(MenuRegion menuRegion);

        List<MenuRegion> GetAll();

        MenuRegion Delete(long menuRegionId);

        List<MenuRegion> GetAllByMenuId(long menuId);
        bool UpdateMenuRegion(MenuRegion menuRegion);
        MenuRegion GetById(long menuRegionId);
    }
}
