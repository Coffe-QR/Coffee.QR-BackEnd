using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{

    //public enum ReportType { MONTHLY, YEARLY }
    public class CardSaleReport : Entity
    {
        public string Path { get; set; }
        public DateOnly Date { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } 

        public CardSaleReport(string path, DateOnly date, long userId)
        {
            Path = path;
            Date = date;
            UserId = userId;
        }
    }
}
