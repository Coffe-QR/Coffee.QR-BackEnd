using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class JobApplicationsCountReportRepository : IJobApplicationsCountReportRepository
    {
        private readonly Context _dbContext;
        public JobApplicationsCountReportRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }
        public JobApplicationsCountReport Create(JobApplicationsCountReport jobApplicationsCountReport)
        {
            _dbContext.JobApplicationsCountReports.Add(jobApplicationsCountReport);
            _dbContext.SaveChanges();
            return jobApplicationsCountReport;
        }

        public JobApplicationsCountReport Delete(long jobApplicationsCountReportId)
        {
            var jobApplicationsCountReportToDelete = _dbContext.JobApplicationsCountReports.Find(jobApplicationsCountReportId);
            if (jobApplicationsCountReportToDelete != null)
            {
                _dbContext.JobApplicationsCountReports.Remove(jobApplicationsCountReportToDelete);
                _dbContext.SaveChanges();
            }
            return jobApplicationsCountReportToDelete;
        }

        public List<JobApplicationsCountReport> GetAll()
        {
            return _dbContext.JobApplicationsCountReports.ToList();
        }
    }
}
