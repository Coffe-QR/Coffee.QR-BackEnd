using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public enum MenuStatus
    {
        ACTIVE,
        WAITING,
        FINISHED
    }
    public class Menu : Entity
    {
        public DateOnly StartDate {  get; set; }
        public DateOnly EndDate { get; set; }
        public MenuStatus Status { get; set; }
        public long CafeId { get; set; }

        public Menu(DateOnly startDate, DateOnly endDate, MenuStatus status, long cafeId)
        {
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            CafeId = cafeId;
        }
    }
}
