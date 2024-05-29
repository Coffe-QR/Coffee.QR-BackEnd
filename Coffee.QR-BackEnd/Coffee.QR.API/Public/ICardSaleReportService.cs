using Coffee.QR.API.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.Public
{
    public interface ICardSaleReportService
    {
        Result<CardSaleReportDto> CreateReport(CardSaleReportDto cardSaleReportDto);
        Result<List<CardSaleReportDto>> GetAllReports();
        bool DeleteReport(long cardSaleReportId);
    }
}
