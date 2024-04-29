using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
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
    }
}
