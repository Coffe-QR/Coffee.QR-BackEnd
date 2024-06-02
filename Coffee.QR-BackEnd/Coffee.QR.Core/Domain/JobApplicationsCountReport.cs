using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{

    public enum JobReportType { MONTHLY, YEARLY }

    public class JobApplicationsCountReport : Entity
    {
        public string Path { get; set; }
        public DateOnly Date { get; set; }
        public long LocalId { get; set; }
        public JobReportType Type { get; set; }

        public JobApplicationsCountReport(string path, DateOnly date, long localId, JobReportType type)
        {
            Path = path;
            Date = date;
            LocalId = localId;
            Type = type;
        }
    }
}
