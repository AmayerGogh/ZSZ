using System.Data.Entity;
using System.Reflection;
using ZSZ.Common;
using ZSZ.Service.Entities;

namespace ZSZ.Service
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ZSZDbContext : DbContext
    {
        //name=connstr表示使用连接字符串中名字为connstr的去连接数据库
        public ZSZDbContext():base("name=connstr")            
        {
            Database.SetInitializer<ZSZDbContext>(null);//不检查实体变化
            this.Database.Log = (sql) => {
                LogHelper.Debug(string.Format("EF执行SQL：{0}", sql));
            };        
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());           
        }
        public DbSet<AdminUserEntity> AdminUsers { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<CommunityEntity> Communities { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<RegionEntity> Regions { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<SettingEntity> Settings { get; set; }
        public DbSet<AttachmentEntity> Attachments { get; set; }
        public DbSet<HouseEntity> Houses { get; set; }
        public DbSet<HouseAppointmentEntity> HouseAppointments { get; set; }
        public DbSet<IdNameEntity> IdNames { get; set; }
        public DbSet<HousePicEntity> HousePics { get; set; }
        public DbSet<AdminLogEntity> AdminUserLogs { get; set; }
    }

}
