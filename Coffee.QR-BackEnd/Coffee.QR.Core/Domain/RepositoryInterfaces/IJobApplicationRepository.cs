using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IJobApplicationRepository
    {
        JobApplication Create(JobApplication jobApplication);
        List<JobApplication> GetAll();
        JobApplication Delete(long jobApplicationId);
        List<JobApplication> GetJobApplicationsByLocal(long localId);
    }
}
