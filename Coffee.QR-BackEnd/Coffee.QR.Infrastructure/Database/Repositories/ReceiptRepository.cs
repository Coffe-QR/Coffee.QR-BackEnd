using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly Context _dbContext;
        public ReceiptRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Receipt Create(Receipt receipt)
        {
            _dbContext.Receipts.Add(receipt);
            _dbContext.SaveChanges();
            return receipt;
        }

        public List<Receipt> GetAll()
        {
            return _dbContext.Receipts.ToList();
        }

        public Receipt Delete(long receiptId)
        {
            var receiptToDelete = _dbContext.Receipts.Find(receiptId);
            if (receiptToDelete != null)
            {
                _dbContext.Receipts.Remove(receiptToDelete);
                _dbContext.SaveChanges();
            }
            return receiptToDelete;
        }
    }
}
