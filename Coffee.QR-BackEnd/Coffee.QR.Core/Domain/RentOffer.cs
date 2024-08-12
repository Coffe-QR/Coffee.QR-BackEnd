using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public enum RentOfferStatus
    {
        SENT,
        ACCEPTED,
        DECLINED
    }
    public class RentOffer : Entity
    {
     public User User { get; private set; }
     public long UserId { get; set; }

     public Local Local { get; private set; }
     public long LocalId {  get; set; }
     public double Price { get; set; }
     public DateTime DateTime { get; set; }
     public RentOfferStatus RentOfferStatus { get; set; }

        public RentOffer(long userId,long localId, double price, DateTime dateTime, RentOfferStatus rentOfferStatus)
        {
            UserId = userId;
            LocalId = localId;
            Price = price;
            DateTime = dateTime;
            RentOfferStatus = rentOfferStatus;

        }

    }
}
