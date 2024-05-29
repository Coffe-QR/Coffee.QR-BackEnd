using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface ICardSaleRepository
    {
        CardSaleReport Create(CardSaleReport cardSaleReport);
        List<CardSaleReport> GetAll();
        CardSaleReport Delete(long cardSaleReportId);
    }
}
