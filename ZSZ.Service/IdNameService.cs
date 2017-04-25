using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;
using ZSZ.Service.Entities;

namespace ZSZ.Service
{
    public class IdNameService : IIdNameService
    {
        public long AddNew(string typeName, string name)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                IdNameEntity idName =
                    new IdNameEntity { Name = name, TypeName = typeName };

                //检查重复性
                if (ctx.IdNames.Any(n => n.Name.Equals(name) && n.TypeName.Equals(typeName)))
                {
                    throw new ArgumentException("字典项已经存在：" + name);
                }

                ctx.IdNames.Add(idName);
                ctx.SaveChanges();
                return idName.Id;
            }
        }

        private IdNameDTO ToDTO(IdNameEntity entity)
        {
            IdNameDTO dto = new IdNameDTO();
            dto.CreateDateTime = entity.CreateDateTime;
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.TypeName = entity.TypeName;
            return dto;
        }

        public IdNameDTO[] GetAll(string typeName)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<IdNameEntity> bs 
                    = new CommonService<IdNameEntity>(ctx);
                return bs.GetAll().Where(e => e.TypeName == typeName)
                    .Select(e=>ToDTO(e)).ToArray();
            }
        }

        public IdNameDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<IdNameEntity> bs
                    = new CommonService<IdNameEntity>(ctx);
                return ToDTO(bs.GetById(id));
            }
        }
    }
}
