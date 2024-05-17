using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IReportRepository
    {
        Report Create(Report report);
        List<Report> GetAll();
        Report Delete(long reportId);
    }
}
