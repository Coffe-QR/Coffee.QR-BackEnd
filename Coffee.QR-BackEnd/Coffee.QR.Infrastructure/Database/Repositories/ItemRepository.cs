﻿using Coffee.QR.Core.Domain;
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
    }
}