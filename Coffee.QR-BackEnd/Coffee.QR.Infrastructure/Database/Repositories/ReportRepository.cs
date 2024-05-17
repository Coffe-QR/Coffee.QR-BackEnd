using Coffee.QR.Core.Domain.RepositoryInterfaces;
using Coffee.QR.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    internal class ReportRepository : IReportRepository
    {
        private readonly Context _dbContext;
        public ReportRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Report Create(Report report)
        {
            _dbContext.Reports.Add(report);
            _dbContext.SaveChanges();
            return report;
        }

        public List<Report> GetAll()
        {
            return _dbContext.Reports.ToList();
        }

        public Report Delete(long eventId)
        {
            var reportToDelete = _dbContext.Reports.Find(eventId);
            if (reportToDelete != null)
            {
                _dbContext.Reports.Remove(reportToDelete);
                _dbContext.SaveChanges();
            }
            return reportToDelete;
        }
    }
}
