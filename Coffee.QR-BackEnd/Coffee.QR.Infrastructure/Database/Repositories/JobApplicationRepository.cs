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
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly Context _dbContext;
        public JobApplicationRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public JobApplication Create(JobApplication jobApplication)
        {
            _dbContext.JobApplications.Add(jobApplication);
            _dbContext.SaveChanges();
            return jobApplication;
        }

        public List<JobApplication> GetAll()
        {
            return _dbContext.JobApplications.ToList();
        }

        public JobApplication Delete(long jobApplicationId)
        {
            var jobApplicationToDelete = _dbContext.JobApplications.Find(jobApplicationId);
            if (jobApplicationToDelete != null)
            {
                _dbContext.JobApplications.Remove(jobApplicationToDelete);
                _dbContext.SaveChanges();
            }
            return jobApplicationToDelete;
        }

        public List<JobApplication> GetJobApplicationsByLocal(long localId)
        {
            return _dbContext.JobApplications.Where(ja => ja.LocalId == localId).ToList();
        }
    }
}
