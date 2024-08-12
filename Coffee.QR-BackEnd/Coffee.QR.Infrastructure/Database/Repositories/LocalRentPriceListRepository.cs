using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class LocalRentPriceListRepository : ILocalRentPriceListRepository
    {
        private readonly Context _dbContext;
        public LocalRentPriceListRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public LocalRentPriceList Create(LocalRentPriceList localRentPriceList)
        {
            _dbContext.LocalRentPriceLists.Add(localRentPriceList);
            _dbContext.SaveChanges();
            return localRentPriceList;
        }

        public LocalRentPriceList CreateAndDeactivateExisting(LocalRentPriceList newLocalRentPriceList)
        {
            var existingPriceLists = _dbContext.LocalRentPriceLists
                .Where(l => l.LocalId == newLocalRentPriceList.LocalId && l.IsActive)
                .ToList();

            foreach (var priceList in existingPriceLists)
            {
                priceList.IsActive = false;
                _dbContext.LocalRentPriceLists.Update(priceList);
            }

            _dbContext.LocalRentPriceLists.Add(newLocalRentPriceList);

            _dbContext.SaveChanges();

            return newLocalRentPriceList;
        }

        public LocalRentPriceList? GetActiveByLocalId(long localId)
        {
            return _dbContext.LocalRentPriceLists
                .FirstOrDefault(l => l.LocalId == localId && l.IsActive);
        }


    }
}
