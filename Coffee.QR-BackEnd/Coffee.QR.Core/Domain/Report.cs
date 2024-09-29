using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{

    public enum ReportType { WEAKLY ,MONTHLY, YEARLY }
    public enum ReportKind { COST, PROFIT }

    public class Report : Entity
    {
        public string Path { get; set; }
        public ReportType Type { get; set; } 
        public DateOnly Start { get; set; }
        public DateOnly End{ get; set; }
        public long LocalId { get; set; }
        public ReportKind Kind { get; set; }

        public Report(string path, ReportType type, DateOnly start, DateOnly end, long localId, ReportKind kind)
        {
            Path = path;
            Type = type;
            Start = start;
            End = end;
            LocalId = localId;
            Kind = kind;
        }

        public Report()
        {

        }

        public Report(string path, ReportType type, DateOnly date, long localId)
        {
            Path = path;
            Type = type;
            LocalId = localId;
        }
    }
}