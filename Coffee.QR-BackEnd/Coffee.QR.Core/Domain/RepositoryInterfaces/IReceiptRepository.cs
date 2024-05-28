using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IReceiptRepository
    {
        Receipt Create(Receipt receipt);
        List<Receipt> GetAll();
        Receipt Delete(long receiptId);
    }
}
