using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;
using ZSZ.Service.Entities;
using System.Data.Entity;

namespace ZSZ.Service
{
    public class CommunityService : ICommunityService
    {
        private CommunityDTO ToDTO(CommunityEntity com)
        {
            CommunityDTO dto = new CommunityDTO()
            {
                BuiltYear = com.BuiltYear,
                CreateDateTime = com.CreateDateTime,
                Id = com.Id,
                Location = com.Location,
                Name = com.Name,
                RegionId = com.RegionId,
                Traffic = com.Traffic
            };

            return dto;
        }

        public CommunityDTO[] GetByRegionId(long regionId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<CommunityEntity> bs 
                    = new CommonService<CommunityEntity>(ctx);
                var cities = bs.GetAll().AsNoTracking()
                    .Where(c => c.RegionId == regionId);
                return cities.Select(c=>ToDTO(c)).ToArray();
            }
        }
    }
}
