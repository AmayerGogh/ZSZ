using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.DTO
{
    public class HouseFindDTO : HouseNewDTO
    {
        public long CityId { get; set; }
        public String CityName { get; set; }
        public long RegionId { get; set; }
        public String RegionName { get; set; }
        public String CommunityName { get; set; }
        public String CommunityLocation { get; set; }
        public String CommunityTraffic { get; set; }
        public int? CommunityBuiltYear { get; set; }

        public String RoomTypeName { get; set; }       
        public String StatusName { get; set; }
        public String DecorateStatusName { get; set; }
        public String TypeName { get; set; }
        public String FirstThumbUrl { get; set; }
    }

}
