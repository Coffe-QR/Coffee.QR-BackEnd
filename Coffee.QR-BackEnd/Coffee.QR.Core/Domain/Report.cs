using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{

    public enum ReportType { MONTHLY, YEARLY }

    public class Report : Entity
    {
        public string Path { get; set; }
        public ReportType Type { get; set; } 
        public DateOnly Date { get; set; }

        public long LocalId { get; set; }

        public Report(string path, ReportType type, DateOnly date, long localId)
        {
            Path = path;
            Type = type;
            Date = date;
            LocalId = localId;
        }
    }
}