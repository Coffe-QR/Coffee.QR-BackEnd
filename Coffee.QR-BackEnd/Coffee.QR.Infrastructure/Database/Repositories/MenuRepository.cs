using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly Context _dbContext;
        public MenuRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Menu Create(Menu menu)
        {
            _dbContext.Menus.Add(menu);
            _dbContext.SaveChanges();
            return menu;
        }

        public List<Menu> GetAll()
        {
            return _dbContext.Menus.ToList();
        }

        public Menu Delete(long menuId)
        {
            var menuToDelete = _dbContext.Menus.Find(menuId);
            if (menuToDelete != null)
            {
                _dbContext.Menus.Remove(menuToDelete);
                _dbContext.SaveChanges();
            }
            return menuToDelete;
        }

        public List<Menu> GetAllByLocalId(long localId)
        {
            return _dbContext.Menus.Where(m => m.CafeId == localId).ToList();
        }

        public Menu GetById(long menuId)
        {
            return _dbContext.Menus.Find(menuId);
        }

        public bool UpdateMenu(Menu menu)
        {
            var existingMenu = _dbContext.Menus.FirstOrDefault(m => m.Id == menu.Id);
            if (existingMenu != null)
            {
                existingMenu.Name = menu.Name;
                existingMenu.Description = menu.Description;
                existingMenu.IsActive = menu.IsActive;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
