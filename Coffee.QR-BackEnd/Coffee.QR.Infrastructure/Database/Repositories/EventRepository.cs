﻿using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly Context _dbContext;
        public EventRepository(Context dbContext)
        {
                _dbContext = dbContext;
        }
        
    }
}