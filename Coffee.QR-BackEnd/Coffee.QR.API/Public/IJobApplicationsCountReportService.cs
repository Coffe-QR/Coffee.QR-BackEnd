using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IJobApplicationsCountReportService
    {
        Result<JobApplicationsCountReportDto> CreateReport(JobApplicationsCountReportDto jobApplicationsCountReportDto);
        Result<List<JobApplicationsCountReportDto>> GetAllReports();
        bool DeleteReport(long jobApplicationsReportId);

        Result<List<ReportDto>> GetAllForLocal(long localId);
    }
}
