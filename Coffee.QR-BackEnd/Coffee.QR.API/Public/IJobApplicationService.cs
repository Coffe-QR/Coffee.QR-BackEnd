using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IJobApplicationService
    {
        Result<JobApplicationDto> CreateJobApplication(JobApplicationDto jobApplicationDto);
        Result<List<JobApplicationDto>> GetAllJobApplications();
        bool DeleteJobApplication(long jobApplicationId);
        Task<Result<JobApplicationDto>> GetJobApplicationByIdAsync(long id);
        Task<Result<JobApplicationDto>> UpdateJobApplicationAsync(JobApplicationDto jobApplicationDto);
        Result<List<JobApplicationDto>> GetJobApplicationsByLocal(long localId);
    }
}
