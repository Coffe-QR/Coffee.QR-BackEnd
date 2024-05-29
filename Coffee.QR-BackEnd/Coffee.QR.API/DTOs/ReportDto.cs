using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public enum ReportTypeDto { MONTHLY, YEARLY }
    public class ReportDto
    {
        public long Id { get; set; }
        public string Path { get; set; }
        public ReportTypeDto Type { get; set; }
        public DateOnly Date { get; set; }
        public long LocalId { get; set; }
    }
}
