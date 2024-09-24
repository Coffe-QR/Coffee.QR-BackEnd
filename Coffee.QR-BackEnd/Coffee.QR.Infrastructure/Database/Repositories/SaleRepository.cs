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
    public class SaleRepository : ISaleRepository
    {
        private readonly Context _dbContext;

        public SaleRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Sale> GetAllForCompany(string companyName)
        {
            return _dbContext.Sales.Include(s => s.Company).Where(s => s.Company.Name == companyName).ToList();
        }
    }
}
