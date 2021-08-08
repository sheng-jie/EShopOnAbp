using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EShopOnAbp.Vips
{
    public class VipManager : DomainService
    {
        private readonly IVipRepository _vipRepository;

        public VipManager(IVipRepository vipRepository)
        {
            _vipRepository = vipRepository;
        }

        public async Task<Vip> CreateVipAsync(string customerId)
        {
            var vip = await _vipRepository.FirstOrDefaultAsync(t => t.CustomerId == customerId);

            if (vip != null) return vip;

            vip = new Vip(GuidGenerator.Create().ToString(), customerId);

            await _vipRepository.InsertAsync(vip);

            return vip;
        }

        public async Task<List<string>> GetHasExpiredRecordVipIdsAsync()
        {
            var vipIds = await _vipRepository.GetVipIdsFromRecordsAsync(new ExpiredVipScoreRecordSpecification());

            return vipIds;
        }


        public async Task MarkVipExpiredScoreRecordsAsync(string vipId)
        {
            var vip = await _vipRepository.FindAsync(vipId, includeDetails: true);
            vip.MarkExpiredRecord();
        }
    }
}