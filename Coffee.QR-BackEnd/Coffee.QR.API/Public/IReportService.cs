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
        Result<List<ReportDto>> GetAllForLocal(long localId);
    }
}
