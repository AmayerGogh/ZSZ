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
    public class AttachmentService : IAttachmentService
    {
        private AttachmentDTO ToDTO(AttachmentEntity att)
        {
            AttachmentDTO dto = new AttachmentDTO();
            dto.CreateDateTime = att.CreateDateTime;
            dto.IconName = att.IconName;
            dto.Id = att.Id;
            dto.Name = att.Name;
            return dto;
        }
        public AttachmentDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AttachmentEntity> bs
                    = new CommonService<AttachmentEntity>(ctx);
                var items = bs.GetAll().AsNoTracking();
                return items.Select(a=>ToDTO(a)).ToArray();
            }
        }

        public AttachmentDTO[] GetAttachments(long houseId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<HouseEntity> houseBS
                    = new CommonService<HouseEntity>(ctx);
                var house = houseBS.GetAll().Include(a => a.Attachments)
                    .AsNoTracking().SingleOrDefault(h=>h.Id.Equals(houseId));

                if(house==null)
                {
                    throw new ArgumentException("houseId"+houseId+"不存在");
                }
                return house.Attachments.Select(a => ToDTO(a)).ToArray();
            }
        }
    }
}
