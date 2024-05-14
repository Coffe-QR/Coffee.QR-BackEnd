using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Infrastructure.Database.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly Context _dbContext;
        public TableRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public Table Create(Table table)
        {
            _dbContext.Tables.Add(table);
            _dbContext.SaveChanges();
            return table;
        }

        public List<Table> GetAll()
        {
            return _dbContext.Tables.ToList();
        }

        public Table GetById(long tableId)
        {
            return _dbContext.Tables.Find(tableId);
        }

        public Table Delete(long tableId)
        {
            var tableToDelete = _dbContext.Tables.Find(tableId);
            if (tableToDelete != null)
            {
                _dbContext.Tables.Remove(tableToDelete);
                _dbContext.SaveChanges();
            }
            return tableToDelete;
        }
    }
}
