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
    public class UserService : IUserService
    {
        public long AddNew(string phoneNum, string password)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                UserEntity user = new UserEntity();
                user.PhoneNum = phoneNum;

                ctx.SaveChanges();
                throw new NotImplementedException();
            }
        }

        public bool CheckLogin(string phoneNum, string password)
        {
            throw new NotImplementedException();
        }

        private UserDTO ToDTO(UserEntity user)
        {
            UserDTO dto = new UserDTO();
            dto.CityId = user.CityId;
            dto.CreateDateTime = user.CreateDateTime;
            dto.Id = user.Id;
            dto.LastLoginErrorDateTime = user.LastLoginErrorDateTime;
            dto.LoginErrorTimes = user.LoginErrorTimes;
            dto.PhoneNum = user.PhoneNum;
            return dto;
        }

        public UserDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<UserEntity> bs = new CommonService<UserEntity>(ctx);
                var user = bs.GetById(id);
                return user == null ? null : ToDTO(user);
            }
        }

        public UserDTO GetByPhoneNum(string phoneNum)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                CommonService<UserEntity> bs = new CommonService<UserEntity>(ctx);
                var user = bs.GetAll().SingleOrDefault(u => u.PhoneNum == phoneNum);
                return user == null ? null : ToDTO(user);
            }
        }

        public void SetUserCityId(long userId, long cityId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePwd(long userId, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
