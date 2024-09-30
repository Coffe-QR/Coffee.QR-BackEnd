using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iTextSharp.text.pdf.AcroFields;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class ErrorSupplyRepository : IErrorSupplyRepository
    {
        private readonly Context _dbContext;
        public ErrorSupplyRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public ErrorSupply Create(ErrorSupply errorSupply)
        {
            _dbContext.ErrorSupplies.Add(errorSupply);
            _dbContext.SaveChanges();
            return errorSupply;
        }

        public List<ErrorSupply> GetAllForSupply(long supplyId)
        {
            return _dbContext.ErrorSupplies.Include(s => s.Item).Where(e => e.SupplyId == supplyId).ToList();
        }
    }
}
