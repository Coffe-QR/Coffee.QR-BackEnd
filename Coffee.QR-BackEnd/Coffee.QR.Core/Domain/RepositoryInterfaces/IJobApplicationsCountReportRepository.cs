using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IJobApplicationsCountReportRepository
    {
        JobApplicationsCountReport Create(JobApplicationsCountReport jobApplicationsCountReport);
        List<JobApplicationsCountReport> GetAll();
        JobApplicationsCountReport Delete(long jobApplicationsCountReportId);
    }
}
