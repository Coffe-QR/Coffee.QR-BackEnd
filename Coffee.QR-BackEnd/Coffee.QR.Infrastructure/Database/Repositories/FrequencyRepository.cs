using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class FrequencyRepository : IFrequencyRepository
    {
        private readonly Context _context;

        public FrequencyRepository(Context context)
        {
            _context = context;
        }

        public List<Frequency> GetAll()
        {
            return _context.Frequencies.ToList();
        }

     
        public Frequency Create(Frequency cardUser)
        {
            _context.Frequencies.Add(cardUser);
            _context.SaveChanges();
            return cardUser;
        }

        
    }
}
