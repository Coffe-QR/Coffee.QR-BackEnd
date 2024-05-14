using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface ILocalUserRepository
    {
        LocalUser Create(LocalUser localUser);
        List<LocalUser> GetAll();
        LocalUser Delete(long localUserId);
        public LocalUser GetByUserId(long userId);
    }
}
