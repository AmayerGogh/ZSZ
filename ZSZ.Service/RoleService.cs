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
    public class RoleService : IRoleService
    {
        public long AddNew(string roleName)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                //todo:重名检查
                RoleEntity role = new RoleEntity();
                role.Name = roleName;
                ctx.Roles.Add(role);
                ctx.SaveChanges();
                return role.Id;
            }
        }

        public void AddRoleIds(long adminUserId, long[] roleIds)
        {
            throw new NotImplementedException();
        }

        private RoleDTO ToDTO(RoleEntity en)
        {
            RoleDTO dto = new RoleDTO();
            dto.CreateDateTime = en.CreateDateTime;
            dto.Id = en.Id;
            dto.Name = en.Name;
            return dto;
        }

        public RoleDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> bs = new CommonService<RoleEntity>(ctx);
                return bs.GetAll().Select(p => ToDTO(p)).ToArray();
            }
        }

        public RoleDTO[] GetByAdminUserId(long adminUserId)
        {
            throw new NotImplementedException();
        }

        public RoleDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> bs = new CommonService<RoleEntity>(ctx);
                var role = bs.GetById(id);
                return role == null ? null : ToDTO(role);
            }
        }

        public RoleDTO GetByName(string name)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> bs = new CommonService<RoleEntity>(ctx);
                var role = bs.GetAll().SingleOrDefault(r => r.Name == name);
                return role == null ? null : ToDTO(role);
            }
        }

        public void MarkDeleted(long roleId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<RoleEntity> bs = new CommonService<RoleEntity>(ctx);
                bs.MarkDeleted(roleId);
            }
        }

        public void Update(long roleId, string roleName)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                RoleEntity role = new RoleEntity();
                role.Id = roleId;
                ctx.Entry(role).State = System.Data.Entity.EntityState.Unchanged;
                role.Name = roleName;
                ctx.SaveChanges();
            }
        }

        public void UpdateRoleIds(long adminUserId, long[] roleIds)
        {
            throw new NotImplementedException();
        }
    }
}
