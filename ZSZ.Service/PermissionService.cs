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
    public class PermissionService : IPermissionService
    {
        public void AddPermIds(long roleId, long[] permIds)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                RoleEntity role = ctx.Roles.SingleOrDefault(r => r.Id.Equals(roleId));
                if (role == null)
                {
                    throw new ArgumentException("没有这个角色：" + roleId);
                }

                var pers = ctx.Permissions.Where(p => permIds.Contains(p.Id));
                foreach (var item in pers)
                {
                    role.Permissions.Add(item);
                }

                ctx.SaveChanges();
            }
        }

        private PermissionDTO ToDTO(PermissionEntity p)
        {
            PermissionDTO dto = new PermissionDTO();
            dto.CreateDateTime = p.CreateDateTime;
            dto.Description = p.Description;
            dto.Id = p.Id;
            dto.Name = p.Name;
            return dto;
        }

        public PermissionDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<PermissionEntity> bs = new CommonService<PermissionEntity>(ctx);
                return bs.GetAll().Select(p=>ToDTO(p)).ToArray();
            }
        }

        public PermissionDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<PermissionEntity> bs = new CommonService<PermissionEntity>(ctx);
                var pe = bs.GetById(id);
                return pe == null ? null : ToDTO(pe);
            }
        }

        public PermissionDTO GetByName(string name)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<PermissionEntity> bs = new CommonService<PermissionEntity>(ctx);
                var pe = bs.GetAll().SingleOrDefault(p=>p.Name==name);
                return pe == null ? null : ToDTO(pe);
            }
        }

        public PermissionDTO[] GetByRoleId(long roleId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> bs = new CommonService<RoleEntity>(ctx);
                return bs.GetById(roleId).Permissions.Select(p => ToDTO(p)).ToArray();
            }
        }

        public void UpdatePermIds(long roleId, long[] permIds)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                RoleEntity role = ctx.Roles.SingleOrDefault(r => r.Id.Equals(roleId));
                if (role == null)
                {
                    throw new ArgumentException("没有这个角色：" + roleId);
                }

                role.Permissions.Clear();

                var pers = ctx.Permissions.Where(p => permIds.Contains(p.Id));
                foreach (var item in pers)
                {
                    role.Permissions.Add(item);
                }

                ctx.SaveChanges();
            }
        }
    }
}
