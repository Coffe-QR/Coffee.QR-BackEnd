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
using static System.Net.Mime.MediaTypeNames;

namespace Coffee.QR.Core.Services
{
    public class JobApplicationsCountReportService : CrudService<JobApplicationsCountReportDto, JobApplicationsCountReport>, IJobApplicationsCountReportService
    {
        private readonly IJobApplicationsCountReportRepository _jobApplicationsCountReportRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public JobApplicationsCountReportService(ICrudRepository<JobApplicationsCountReport> crudRepository, IMapper mapper, IJobApplicationsCountReportRepository jobApplicationsCountReportRepository, IJobApplicationRepository jobApplicationRepository) 
            : base(crudRepository,mapper)
        {
            _jobApplicationsCountReportRepository = jobApplicationsCountReportRepository;
            _jobApplicationRepository = jobApplicationRepository;
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

        public Result<List<JobApplicationsCountReportDto>> GetAllForLocal(long localId)
        {
            try
            {
                var reports = _jobApplicationsCountReportRepository.GetAll().FindAll(r => r.LocalId == localId);
                var reportDtos = reports.Select(r => new JobApplicationsCountReportDto
                {
                    Id = r.Id,
                    Path = r.Path,
                    Date = r.Date,
                    LocalId = r.LocalId,
                    Type = (JobReportTypeDto)Enum.Parse(typeof(JobReportTypeDto), r.Type.ToString(), true),
                }).ToList();

                return Result.Ok(reportDtos);

            }
            catch (Exception e)
            {
                return Result.Fail<List<JobApplicationsCountReportDto>>("Failed to retrieve reports").WithError(e.Message);
            }
        }

        private string CreateReportPdf(JobApplicationsCountReportDto reportDto)
        {
            string path = "..\\Coffee.QR-BackEnd\\Resources\\Pdfs\\JR_" + reportDto.Type + reportDto.LocalId + "_" + reportDto.Id + ".pdf";
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();
            doc.Add(new Paragraph(reportDto.Type.ToString() + " report!"));


            // Dodaj naslov dokumenta
            doc.Add(new Paragraph("Application List"));
            doc.Add(new Paragraph("\n"));

            // Kreiraj tabelu sa četiri kolone
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 3f, 5f });

            // Dodaj zaglavlja kolona
            table.AddCell("Role");
            table.AddCell("Application number");


            table.AddCell("Waiter");
            table.AddCell(_jobApplicationRepository.GetNumberOfApplicationForRole(reportDto.LocalId, JobPosition.WAITER).ToString());

            table.AddCell("Bartender");
            table.AddCell(_jobApplicationRepository.GetNumberOfApplicationForRole(reportDto.LocalId, JobPosition.BARTENDER).ToString());


            table.AddCell("Chef");
            table.AddCell(_jobApplicationRepository.GetNumberOfApplicationForRole(reportDto.LocalId, JobPosition.CHEF).ToString());



            table.AddCell("Manager");
            table.AddCell(_jobApplicationRepository.GetNumberOfApplicationForRole(reportDto.LocalId, JobPosition.MANAGER).ToString());



            // Dodaj tabelu u dokument
            doc.Add(table);

            // Zatvori dokument
            doc.Close();


            doc.Close();
            string pv = "\Pdfs\\JR_" + reportDto.Type + reportDto.LocalId + "_" + reportDto.Id + ".pdf";
            return pv;
        }

    }
}
