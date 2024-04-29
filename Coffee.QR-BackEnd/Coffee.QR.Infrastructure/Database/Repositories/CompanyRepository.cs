using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly Context _dbContext;
        public CompanyRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Company Create(Company company)
        {
            _dbContext.Companies.Add(company);
            _dbContext.SaveChanges();
            return company;
        }

        public List<Company> GetAll()
        {
            return _dbContext.Companies.ToList();
        }

        public Company Delete(long eventId)
        {
            var companyToDelete = _dbContext.Companies.Find(eventId);
            if (companyToDelete != null)
            {
                _dbContext.Companies.Remove(companyToDelete);
                _dbContext.SaveChanges();
            }
            return companyToDelete;
        }
    }
}
