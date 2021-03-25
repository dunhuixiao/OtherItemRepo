using GoodsManagement.IService;
using GoodsManagement.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.Service
{
    /// <summary>
    /// 标签接口实现
    /// </summary>
    public class TagService: ITagService
    {
        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="dto">新增标签模型</param>
        /// <returns></returns>
        public bool Create(CreateTagDto dto)
        {
            using (var db = new Db())
            {
                //校验标签名称是否重复
                if (db.TagRepo.Any(o => o.TagName == dto.TagName && !o.IsDelete))
                {
                    throw new Exception("标签名称重复");
                }
                //新增
                db.TagRepo.Add(new TagRepo
                {
                    TagName = dto.TagName,
                    IsDelete = false,
                    CreateAt = DateTime.Now,
                    LastEditAt = DateTime.Now
                });
                return db.SaveChanges() > 0;
            }    
        }
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="TagId">标签ID</param>
        /// <returns></returns>
        public bool Delete(int TagId)
        {
            using (var db = new Db())
            {
                var entity = db.TagRepo.Where(o => o.Id == TagId && !o.IsDelete).FirstOrDefault();
                //并发性校验
                if (entity==null)
                {
                    throw new Exception("找不到数据");
                }
                //修改删除状态
                entity.IsDelete = true;
                entity.LastEditAt = DateTime.Now;
                return db.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="dto">标签修改模型</param>
        /// <returns></returns>
        public bool Update(UpdateTagDto dto)
        {
            using (var db = new Db())
            {
                var entity = db.TagRepo.Where(o => o.Id == dto.Id && !o.IsDelete).FirstOrDefault();
                //并发性校验
                if (entity == null)
                {
                    throw new Exception("找不到数据");
                }
                //修改删除状态
                entity.TagName = dto.TagName;
                entity.LastEditAt = DateTime.Now;
                return db.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 查询标签列表，也可作为下拉列表
        /// </summary>
        /// <returns></returns>
        public List<TagDto> GetList()
        {
            using (var db=new Db())
            {
                //获取未被删除的标签列表
                var list = from t in db.TagRepo
                           where !t.IsDelete
                           select new TagDto
                           {
                               Id = t.Id,
                               TagName = t.TagName
                           };
                return list.ToList();
            }
        }
        /// <summary>
        /// 查询标签详情
        /// </summary>
        /// <param name="TagId">标签ID</param>
        /// <returns></returns>
        public TagDto GetDetail(int TagId)
        {
            using (var db=new Db())
            {
                var entity = db.TagRepo.Where(o => o.Id == TagId && !o.IsDelete).FirstOrDefault();
                //并发性校验
                if (entity == null)
                {
                    throw new Exception("找不到数据");
                }
                //获取详情信息
                var result = new TagDto()
                {
                    Id = entity.Id,
                    TagName = entity.TagName
                };
                return result;
            }
        }
    }
}
