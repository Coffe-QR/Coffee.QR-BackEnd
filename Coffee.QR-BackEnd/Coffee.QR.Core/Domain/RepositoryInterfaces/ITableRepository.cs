using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface ITableRepository
    {
        Table Create(Table table);
        List<Table> GetAll();
        Table Delete(long tableId);
        Table GetById(long tableId);
    }
}
