using AutoMapper;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using FluentResults;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Services
{
    public class JobApplicationsCountReportService : CrudService<JobApplicationsCountReportDto, JobApplicationsCountReport>, IJobApplicationsCountReportService
    {
        private readonly IJobApplicationsCountReportRepository _jobApplicationsCountReportRepository;
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationsCountReportService(ICrudRepository<JobApplicationsCountReport> crudRepository, IMapper mapper, IJobApplicationsCountReportRepository jobApplicationsCountReportRepository) 
            : base(crudRepository,mapper)
        {
            _jobApplicationsCountReportRepository = jobApplicationsCountReportRepository;
        }

        public Result<JobApplicationsCountReportDto> CreateReport(JobApplicationsCountReportDto reportDto)
        {
            try
            {
                var report = _jobApplicationsCountReportRepository.Create(new JobApplicationsCountReport(CreateReportPdf(reportDto), reportDto.Date, reportDto.LocalId, (JobReportType)Enum.Parse(typeof(JobReportType), reportDto.Type.ToString(), true)));

                JobApplicationsCountReportDto resultDto = new JobApplicationsCountReportDto
                {
                    Id = report.Id,
                    Path = report.Path,
                    Date = report.Date,
                    LocalId = report.LocalId,
                    Type = (JobReportTypeDto)Enum.Parse(typeof(JobReportTypeDto), report.Type.ToString(), true),
                };
                return Result.Ok(resultDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<JobApplicationsCountReportDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }




        public bool DeleteReport(long jobApplicationReportId)
        {
            var reportToDelete = _jobApplicationsCountReportRepository.Delete(jobApplicationReportId);
            return reportToDelete != null;
        }

        public Result<List<JobApplicationsCountReportDto>> GetAllReports()
        {
            throw new NotImplementedException();
        }

        private string CreateReportPdf(JobApplicationsCountReportDto reportDto)
        {
           
        }

    }
}
