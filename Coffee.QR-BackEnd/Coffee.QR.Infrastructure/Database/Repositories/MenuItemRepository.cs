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

        public MenuItem Delete(long eventId)
        {
            var eventToDelete = _dbContext.MenuItems.Find(eventId);
            if (eventToDelete != null)
            {
                _dbContext.MenuItems.Remove(eventToDelete);
                _dbContext.SaveChanges();
            }
            return eventToDelete;
        }
    }
}
