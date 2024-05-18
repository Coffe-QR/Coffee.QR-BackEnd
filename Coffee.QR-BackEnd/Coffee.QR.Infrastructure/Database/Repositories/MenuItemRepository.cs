using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly Context _dbContext;
        public MenuItemRepository(Context dbContext) 
        {
            _dbContext = dbContext;
        }

        public MenuItem Create(MenuItem menuItem)
        {
            _dbContext.MenuItems.Add(menuItem);
            _dbContext.SaveChanges();
            return menuItem;
        }

        public List<MenuItem> GetAll()
        {
            return _dbContext.MenuItems.ToList();
        }

        public MenuItem Delete(long menuItemId)
        {
            var menuItemToDelete = _dbContext.MenuItems.Find(menuItemId);
            if (menuItemToDelete != null)
            {
                _dbContext.MenuItems.Remove(menuItemToDelete);
                _dbContext.SaveChanges();
            }
            return menuItemToDelete;
        }

        public bool DeleteByMenuIdAndItemId(long menuId, long itemId)
        {
            var menuItem = _dbContext.MenuItems.FirstOrDefault(mi => mi.MenuId == menuId && mi.ItemId == itemId);
            if (menuItem != null)
            {
                _dbContext.MenuItems.Remove(menuItem);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<MenuItem> GetAllByMenuId(long menuId)
        {
            return _dbContext.MenuItems.Where(mi => mi.MenuId == menuId).ToList();
        }

    }
}
