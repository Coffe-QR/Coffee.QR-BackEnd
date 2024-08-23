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
    public class RentOfferRepository : IRentOfferRepository
    {
        private readonly Context _dbContext;

        public RentOfferRepository(Context context)
        {
            _dbContext = context;
        }

        public RentOffer Create(RentOffer rentOffer)
        {
            _dbContext.RentOffers.Add(rentOffer);
            _dbContext.SaveChanges();
            return rentOffer;
        }

        public RentOffer GetById(long id)
        {
            return _dbContext.RentOffers
                .Include(ro => ro.User)
                .Include(ro => ro.Local)
                .FirstOrDefault(ro => ro.Id == id);
        }

        public IEnumerable<RentOffer> GetAll()
        {
            return _dbContext.RentOffers
                .Include(ro => ro.User)
                .Include(ro => ro.Local)
                .ToList();
        }

        public RentOffer Update(RentOffer rentOffer)
        {
            _dbContext.RentOffers.Update(rentOffer);
            _dbContext.SaveChanges();
            return rentOffer;
        }

        public bool Delete(long id)
        {
            var rentOffer = GetById(id);
            if (rentOffer == null) return false;

            _dbContext.RentOffers.Remove(rentOffer);
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<RentOffer> GetByLocalId(long localId)
        {
            return _dbContext.RentOffers
                .Where(ro => ro.LocalId == localId)
                .ToList();
        }


    }
}
