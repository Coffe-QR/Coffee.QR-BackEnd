using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface IFrequencyRepository
    {
        Frequency Create(Frequency item);
        List<Frequency> GetAll();
    }
}
