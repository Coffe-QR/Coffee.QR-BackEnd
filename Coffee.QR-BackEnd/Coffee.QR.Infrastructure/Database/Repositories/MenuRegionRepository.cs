using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class MenuRegionRepository : IMenuRegionRepository
    {
        private readonly Context _dbContext;
        public MenuRegionRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public MenuRegion Create(MenuRegion menuRegion)
        {
            _dbContext.MenuRegions.Add(menuRegion);
            _dbContext.SaveChanges();
            return menuRegion;
        }

        public List<MenuRegion> GetAll()
        {
            return _dbContext.MenuRegions.ToList();
        }

        public MenuRegion Delete(long menuRegionId)
        {
            var menuRegionToDelete = _dbContext.MenuRegions.Find(menuRegionId);
            if (menuRegionToDelete != null)
            {
                _dbContext.MenuRegions.Remove(menuRegionToDelete);
                _dbContext.SaveChanges();
            }
            return menuRegionToDelete;
        }

        public List<MenuRegion> GetAllByMenuId(long menuId)
        {
            return _dbContext.MenuRegions.Where(mr => mr.MenuId == menuId).ToList();
        }

        public bool UpdateMenuRegion(MenuRegion menuRegion)
        {
            var existingMenuRegion = _dbContext.MenuRegions.FirstOrDefault(mr => mr.Id == menuRegion.Id);
            if (existingMenuRegion != null)
            {
                existingMenuRegion.Name = menuRegion.Name;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public MenuRegion GetById(long menuRegionId)
        {
            return _dbContext.MenuRegions.Find(menuRegionId);
        }
    }
}
