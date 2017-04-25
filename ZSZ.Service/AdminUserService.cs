using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;
using ZSZ.Service.Entities;
using System.Data.Entity;
using ZSZ.Common;

namespace ZSZ.Service
{
    public class AdminUserService : IAdminUserService
    {
        public long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                AdminUserEntity user = new AdminUserEntity();
                user.CityId = cityId;
                user.Email = email;
                user.Name = name;
                user.PhoneNum = phoneNum;
                user.PasswordSalt = CommonHelper.GenerateCaptchaCode(6);//盐
                user.PasswordHash = GetPasswordHash(user.PasswordSalt, password);

                //检查手机号是否存在：使用基类服务，已经筛选掉了被删除的
                CommonService<AdminUserEntity> service = new CommonService<AdminUserEntity>(ctx);
                if (service.GetAll().Any(u => u.PhoneNum.Equals(phoneNum)))
                {
                    //抛出异常
                    throw new ArgumentException("手机号已经存在：" + phoneNum);
                }

                ctx.AdminUsers.Add(user);
                ctx.SaveChanges();

                return user.Id;
            }
            
        }

        public bool CheckLogin(string phoneNum, string password)
        {
            using(ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> service = new CommonService<AdminUserEntity>(ctx);

                //如果有多余一条的数据，直接抛出异常
                //可怕的不是有错误，而是有错误了，看上去却是风平浪静
                var user = service.GetAll().SingleOrDefault(u => u.PhoneNum.Equals(phoneNum));
                if (user == null)
                {
                    return false;
                }

                string dbPwd = user.PasswordHash;
                string usrPwd = GetPasswordHash(user.PasswordSalt, password);

                return dbPwd.Equals(usrPwd);
            }
        }

        private string GetPasswordHash(string salt, string password)
        {
            return EncryptHelper.CalcMD5(salt + password);
        }

        private AdminUserDTO ToDTO(AdminUserEntity user)
        {
            using(ZSZDbContext ctx = new ZSZDbContext())
            {
                AdminUserDTO dto = new AdminUserDTO();
                dto.CityId = user.CityId;
                if(user.City != null)
                {
                    dto.CityName = user.City.Name;//需要Include提升性能
                }
                else
                {
                    dto.CityName = "总部";
                }
                
                dto.CreateDateTime = user.CreateDateTime;
                dto.Email = user.Email;
                dto.Id = user.Id;
                dto.LastLoginErrorDateTime = user.LastLoginErrorDateTime;
                dto.LoginErrorTimes = user.LoginErrorTimes;
                dto.Name = user.Name;
                dto.PhoneNum = user.PhoneNum;
                return dto;
            }

           
        }

        public AdminUserDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                //using System.Data.Entity;才能在IQueryable中用Include、AsNoTracking
                CommonService<AdminUserEntity> bs 
                    = new CommonService<AdminUserEntity>(ctx);
                return bs.GetAll().Include(u=>u.City)//不做延迟加载
                    .AsNoTracking().Select(u => ToDTO(u)).ToArray();//只会显示，不需要修改则不缓存
            }
        }

        /// <summary>
        /// 获取某城市的管理员
        /// </summary>
        /// <param name="cityId">如果为空，则返回总部的管理员</param>
        /// <returns></returns>
        public AdminUserDTO[] GetAll(long? cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> bs
                    = new CommonService<AdminUserEntity>(ctx);
                var all = bs.GetAll().Include(u => u.City)
                    .AsNoTracking().Where(u => u.CityId == cityId);
                return all.Select(u => ToDTO(u)).ToArray();
            }
        }

        public AdminUserDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> bs
                    = new CommonService<AdminUserEntity>(ctx);
                var user = bs.GetAll().Include(u => u.City)
                    .AsNoTracking().SingleOrDefault(u=>u.Id.Equals(id));
                //var user = bs.GetById(id); 没机会使用include了，DTO需要返回城市信息
                if (user==null)
                {
                    return null;//不抛异常，由调用者决定处理方式
                }
                return ToDTO(user);
            }
        }

        public AdminUserDTO GetByPhoneNum(string phoneNum)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> bs
                    = new CommonService<AdminUserEntity>(ctx);
                var users = bs.GetAll().Include(u => u.City)
                    .AsNoTracking().Where(u => u.PhoneNum == phoneNum);
                int count = users.Count();
                if(count <= 0)
                {
                    return null;
                }
                else if(count==1)
                {
                    return ToDTO(users.Single());
                }
                else
                {
                    throw new ApplicationException("找到多个手机号为"+phoneNum+"的管理员");
                }
            }
        }

        public bool HasPermission(long adminUserId, string permissionName)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> bs
                    = new CommonService<AdminUserEntity>(ctx);
                //var user = bs.GetById(adminUserId);//没法使用include
                var user = bs.GetAll().Include(u => u.City).Single(u => u.Id.Equals(adminUserId));
                if (user==null)
                {
                    throw new ArgumentException("找不到id="+adminUserId+"的用户");
                }

                //Roles前面使用include连接查询出来了，不存在延迟加载
                return user.Roles.SelectMany(r => r.Permissions)
                    .Any(p=>p.Name==permissionName);
            }
        }

        public void MarkDeleted(long adminUserId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> bs 
                    = new CommonService<AdminUserEntity>(ctx);
                bs.MarkDeleted(adminUserId);
            }
        }

        public void RecordLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void ResetLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdminUser(long id, string name, string phoneNum, 
            string password, string email, long? cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<AdminUserEntity> bs
                    = new CommonService<AdminUserEntity>(ctx);
                //先查一次再更新，更新也不会那么频繁，这个性能不用考虑
                var user = bs.GetById(id);

                if(user==null)
                {
                    throw new ArgumentException("找不到id="+id+"的管理员");
                }

                //手机号重复检查
                if (bs.GetAll().Any(u => !u.Id.Equals(id) && u.PhoneNum.Equals(phoneNum)))
                {
                    //抛出异常
                    throw new ArgumentException("手机号已经存在：" + phoneNum);
                }

                user.Name = name;
                user.PhoneNum = phoneNum;
                user.Email = email;
                user.CityId = cityId;
                user.PasswordHash = GetPasswordHash(user.PasswordSalt, password);
                
                ctx.SaveChanges();
            }
        }
    }
}
