using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class CardSaleReportRepository : ICardSaleRepository
    {
        private readonly Context _dbContext;
        public CardSaleReportRepository( Context dbContext)
        {
            _dbContext = dbContext;
        }
        public CardSaleReport Create(CardSaleReport cardSaleReport)
        {
            _dbContext.CardSaleReports.Add(cardSaleReport);
            _dbContext.SaveChanges();
            return cardSaleReport;
        }

        public CardSaleReport Delete(long cardSaleReportId)
        {
            var cardSaleReportToDelete = _dbContext.CardSaleReports.Find(cardSaleReportId);
            if (cardSaleReportToDelete != null)
            {
                _dbContext.CardSaleReports.Remove(cardSaleReportToDelete);
                _dbContext.SaveChanges();
            }
            return cardSaleReportToDelete;
        }

        public List<CardSaleReport> GetAll()
        {
            return _dbContext.CardSaleReports.ToList();
        }
    }
}
