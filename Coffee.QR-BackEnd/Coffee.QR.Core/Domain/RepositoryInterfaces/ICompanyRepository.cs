using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface ICompanyRepository
    {
        Company Create(Company company);
        List<Company> GetAll();
        Company Delete(long companyId);
    }
}
