﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopOnAbp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace EShopOnAbp.Vips
{
    public class VipRepository : EfCoreRepository<EShopOnAbpDbContext, Vip, string>, IVipRepository
    {
        public VipRepository(IDbContextProvider<EShopOnAbpDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<string>> GetVipIdsFromRecordsAsync(ISpecification<VipScoreRecord> specification)
        {
            var dbContext = await GetDbContextAsync();
            List<string> vipIds = await dbContext.VipScoreRecords
                .Where(specification.ToExpression())
                .Select(v => v.VipId).Distinct().ToListAsync();

            return vipIds;
        }
    }
}