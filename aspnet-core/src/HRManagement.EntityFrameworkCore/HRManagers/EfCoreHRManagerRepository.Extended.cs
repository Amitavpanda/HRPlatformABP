using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using HRManagement.EntityFrameworkCore;

namespace HRManagement.HRManagers
{
    public class EfCoreHRManagerRepository : EfCoreHRManagerRepositoryBase, IHRManagerRepository
    {
        public EfCoreHRManagerRepository(IDbContextProvider<HRManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}