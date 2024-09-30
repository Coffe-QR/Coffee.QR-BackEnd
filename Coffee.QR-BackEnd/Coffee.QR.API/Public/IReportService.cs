using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface IReportService
    {
        Result<ReportDto> CreateReport(ReportDto eventDto);
        Result<List<ReportDto>> GetAllReports();
        bool DeleteReport(long eventId);
        Result<List<ReportDto>> GetAllForLocalProfit(long localId);
        Result<List<ReportDto>> GetAllForLocalCost(long localId);
        Result<ReportDto> CreateCostReport(ReportDto reportDto);
        Result<ReportDto> CreateNewReport(ReportDto reportDto);
        Result<List<ReportDto>> GetNewReport();
    }
}
