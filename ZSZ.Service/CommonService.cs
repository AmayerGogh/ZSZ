using System.Linq;
using ZSZ.Service.Entities;

namespace ZSZ.Service
{
    //这个类是用来引用的，不是用来继承的
    //上层不应该知道数据是如何存储的，使用的什么ORM（如：EF)
    internal sealed class CommonService<T> where T:BaseEntity
    {
        private ZSZDbContext ctx;
        public CommonService(ZSZDbContext ctx)
        {
            this.ctx = ctx;
        }

        /// <summary>
        /// 获取所有没有软删除的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return ctx.Set<T>().Where(e=>e.IsDeleted==false);
        }

        /// <summary>
        /// 获取总数据条数
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()
        {
            return GetAll().LongCount();
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IQueryable<T> GetPagedData(int startIndex,int count)
        {
            return GetAll().OrderBy(e => e.CreateDateTime)
                .Skip(startIndex).Take(count);
        }

        /// <summary>
        /// 查找id=id的数据，如果找不到返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(long id)
        {
            return GetAll().Where(e=>e.Id==id).SingleOrDefault();
        }

        public void MarkDeleted(long id)
        {
            var data = GetById(id);
            data.IsDeleted = true;
            ctx.SaveChanges();
        }
    }
}
