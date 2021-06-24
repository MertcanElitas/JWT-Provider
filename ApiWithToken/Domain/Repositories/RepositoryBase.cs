using ApiWithToken.Domain.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories
{
    public class RepositoryBase
    {
        protected readonly ApiWihTokenDbContext _context;

        public RepositoryBase(ApiWihTokenDbContext context)
        {
            _context = context;
        }
    }
}
