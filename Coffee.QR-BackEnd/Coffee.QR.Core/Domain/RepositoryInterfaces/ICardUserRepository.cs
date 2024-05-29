using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain.RepositoryInterfaces
{
    public interface ICardUserRepository
    {
        Task<IEnumerable<CardUser>> GetAllAsync();
        Task<CardUser> GetByIdAsync(long cardUserId);
        CardUser Create(CardUser cardUser);
        Task UpdateAsync(CardUser cardUser);
        Task DeleteAsync(long cardUserId);
        List<CardUser> GetAll();
    }
}
