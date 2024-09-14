using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class RegionItemRepository : IRegionItemRepository
    {
        private readonly Context _dbContext;
        public RegionItemRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public RegionItem Create(RegionItem regionItem)
        {
            _dbContext.RegionItems.Add(regionItem);
            _dbContext.SaveChanges();
            return regionItem;
        }

        public List<RegionItem> GetAll()
        {
            return _dbContext.RegionItems.ToList();
        }

        public RegionItem Delete(long regionItemId)
        {
            var regionItemToDelete = _dbContext.RegionItems.Find(regionItemId);
            if (regionItemToDelete != null)
            {
                _dbContext.RegionItems.Remove(regionItemToDelete);
                _dbContext.SaveChanges();
            }
            return regionItemToDelete;
        }

        public bool DeleteByRegionIdAndItemId(long regionId, long itemId)
        {
            var regionItem = _dbContext.RegionItems.FirstOrDefault(r => r.RegionId == regionId && r.ItemId == itemId);
            if (regionItem != null)
            {
                _dbContext.RegionItems.Remove(regionItem);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<RegionItem> GetAllByRegionId(long regionId)
        {
            return _dbContext.RegionItems.Where(r => r.RegionId == regionId).ToList();
        }
    }
}
