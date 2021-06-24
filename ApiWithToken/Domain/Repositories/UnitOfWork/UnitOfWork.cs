using ApiWithToken.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApiWihTokenDbContext _context;

        public UnitOfWork(ApiWihTokenDbContext context)
        {
            _context = context;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public async Task CompleteAsnyc()
        {
            await _context.SaveChangesAsync();
        }
    }
}
