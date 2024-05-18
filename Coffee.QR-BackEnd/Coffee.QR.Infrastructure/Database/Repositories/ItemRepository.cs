using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly Context _dbContext;
        public ItemRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Item Create(Item item)
        {
            _dbContext.Items.Add(item);
            _dbContext.SaveChanges();
            return item;
        }

        public Item Delete(long itemId)
        {
            var itemToDelete = _dbContext.Items.Find(itemId);
            if (itemToDelete != null)
            {
                _dbContext.Items.Remove(itemToDelete);
                _dbContext.SaveChanges();
            }
            return itemToDelete;
        }

        public List<Item> GetAll()
        {
            return _dbContext.Items.ToList();
        }
        
        public Item GetItem(long itemId)
        {
            Item item = _dbContext.Items.FirstOrDefault(item => item.Id == itemId);
            if (item == null) throw new KeyNotFoundException("Not found.");
            return item;
        }
        public Item GetById(long itemId)
        {
            return _dbContext.Items.Find(itemId);
        }

        public bool UpdateItem(Item item)
        {
            var existingItem = _dbContext.Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Description = item.Description;
                existingItem.Price = item.Price;
                existingItem.Picture = item.Picture;
                _dbContext.SaveChanges();
                return true;
            }
            return false;

        }
    }
}
