using GoodsManagement.IService;
using GoodsManagement.Service.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.Service
{
    public class GoodsService: IGoodsService
    {
        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="dto">新增商品模型</param>
        /// <returns></returns>
        public long Create(CreateGoodsDto dto)
        {
            using (var db=new Db())
            {
                //编号重复校验
                if (db.GoodsRepo.Any(o=>o.GoodsCode==dto.GoodsCode&&!o.IsDelete))
                {
                    throw new Exception("商品编号重复");
                }
                var tran = db.Database.BeginTransaction();
                try
                {
                    //新增商品
                    var entity = new GoodsRepo
                    {
                        GoodsCode = dto.GoodsCode,
                        GoodsName = dto.GoodsName,
                        GoodsPutawayState = GoodsPutawayTypes.NotPutaway,
                        GoodsPrice = dto.GoodsPrice,
                        GoodsRemark = dto.GoodsRemark,
                        IsDelete = false,
                        CreateAt = DateTime.Now,
                        LastEditAt = DateTime.Now
                    };
                    db.GoodsRepo.Add(entity);
                    db.SaveChanges();
                    //新增商品对应的标签
                    if (dto.TagsId.Any())
                    {
                        foreach (var item in dto.TagsId)
                        {
                            db.GoodsTagRepo.Add(new GoodsTagRepo
                            {
                                GoodsId=entity.Id,
                                TagId=item
                            });
                        }
                    }
                    db.SaveChanges();
                    tran.Commit();
                    //返回商品ID
                    return entity.Id;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                
            }
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="GoodsId">商品ID</param>
        /// <returns></returns>
        public bool Delete(int GoodsId)
        {
            using (var db=new Db())
            {
                //根据商品ID查询对应的商品信息
                var entity = db.GoodsRepo.FirstOrDefault(o => o.Id == GoodsId && !o.IsDelete);
                //并发性校验
                if (entity==null)
                {
                    throw new Exception("找不到数据");
                }
                //判断状态是否未待上架或已下架
                if (entity.GoodsPutawayState==GoodsPutawayTypes.Putaway)
                {
                    throw new Exception("商品已上架，不可删除");
                }
                //修改删除状态
                entity.IsDelete = true;
                entity.LastEditAt = DateTime.Now;
                return db.SaveChanges()>0;
            }
        }
        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="dto">商品更新模型</param>
        /// <returns></returns>
        public long Updete(UpdateGoodsDto dto)
        {
            using (var db=new Db())
            {
                //根据商品ID查询对应的商品信息
                var entity = db.GoodsRepo.FirstOrDefault(o => o.Id == dto.Id && !o.IsDelete);
                //并发性校验
                if (entity == null)
                {
                    throw new Exception("找不到数据");
                }
                var tran = db.Database.BeginTransaction();
                try
                {
                    //更新商品标签
                    UpdateGoodsTag(db, dto.Id, dto.TagsId);
                    //修改商品
                    entity.GoodsName = dto.GoodsName;
                    entity.GoodsPrice = dto.GoodsPrice;
                    entity.GoodsRemark = dto.GoodsRemark;
                    entity.LastEditAt = DateTime.Now;
                    db.SaveChanges();
                    tran.Commit();
                    //返回商品ID
                    return entity.Id;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="dto">分页及搜索模型</param>
        /// <returns></returns>
        public List<GoodsDto> GetList(QueryGoodsDto dto)
        {
            using (var db=new Db())
            {
                //SQL参数化
                var param = new List<MySqlParameter>();
                param.Add(new MySqlParameter() { ParameterName = "PageIndex", Value = (dto.PageIndex - 1) * dto.PageSize });//当前页面码
                param.Add(new MySqlParameter() { ParameterName = "PageSize", Value = dto.PageSize });//分页显示数量
                //SQL语句
                var sql = $@"SELECT g.GoodsCode,g.GoodsName,g.GoodsPrice,g.GoodsPutawayState,g.CreateAt,g.LastEditAt
                             FROM goods g LEFT JOIN goodstag gt ON g.Id=gt.GoodsId
                             WHERE !g.IsDelete";
                //标签条件
                if (dto.TagId!=null)
                {
                    sql += @" AND gt.TagId=@TagId";
                    param.Add(new MySqlParameter() { ParameterName = "TagId", Value = dto.TagId });//标签
                }
                //上架状态
                if (dto.GoodsPutawayState!=null)
                {
                    sql += @" AND g.GoodsPutawayState=@GoodsPutawayState";
                    param.Add(new MySqlParameter() { ParameterName = "GoodsPutawayState", Value = dto.GoodsPutawayState });//上架状态
                }
                //搜索条件
                if (!string.IsNullOrEmpty(dto.Key))
                {
                    sql += @" AND CONCAT(g.GoodsCode,g.GoodsName,to_pinyin(g.GoodsName)) LIKE @Key";
                    param.Add(new MySqlParameter() { ParameterName = "Key", Value = string.Format("%{0}%", dto.Key) });//搜索条件
                }
                //排序及分组
                sql += @" GROUP BY g.GoodsCode 
                          ORDER BY g.LastEditAt DESC";
                //拼接分页
                sql += @" LIMIT @PageIndex,@PageSize";
                //数据集
                var List = db.Database.SqlQuery<GoodsDto>(sql, param.ToArray()).ToList();
                return List;
            }
        }
        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="GoodsId">商品ID</param>
        /// <returns></returns>
        public GoodsDto GetDetail(long GoodsId)
        {
            using (var db=new Db())
            {
                //根据商品ID查询对应的商品信息
                
                var entity = db.GoodsRepo.FirstOrDefault(o => o.Id == GoodsId && !o.IsDelete);
                //并发性校验
                if (entity==null)
                {
                    throw new Exception("找不到数据");
                }
                //获取详情信息
                var detail=new GoodsDto{
                    GoodsCode = entity.GoodsCode,//商品编号
                    GoodsName = entity.GoodsName,//商品名称
                    GoodsPrice = entity.GoodsPrice,//商品价格
                    GoodsPutawayState = (GoodsPutawayTypes)entity.GoodsPutawayState,//上架状态
                    GoodsRemark = entity.GoodsRemark,//商品描述
                    CreateAt = entity.CreateAt,//创建时间
                    LastEditAt = entity.LastEditAt//最后修改时间
                };
                //根据商品ID获取标签
                detail.Tags = GetTags(db, GoodsId);
                return detail;
            }
        }
        /// <summary>
        /// 设置上架状态
        /// </summary>
        /// <param name="GoodsId">商品ID</param>
        /// <param name="goodsPutawayTypes">商品上架状态</param>
        /// <returns></returns>
        public bool SetPutawayState(long GoodsId, GoodsPutawayTypes goodsPutawayTypes)
        {
            using (var db = new Db())
            {
                //根据商品ID查询对应的商品信息
                var entity = db.GoodsRepo.FirstOrDefault(o => o.Id == GoodsId && !o.IsDelete);
                //判断是上架还是下架
                if (goodsPutawayTypes == GoodsPutawayTypes.Putaway)
                {
                    //上架
                    //判断商品状态是否为未上架或已下架
                    if (entity.GoodsPutawayState != GoodsPutawayTypes.NotPutaway && entity.GoodsPutawayState != GoodsPutawayTypes.SoldOut)
                    {
                        throw new Exception("待上架和已下架的商品，才能上架");
                    }
                    //修改上架状态和操作时间并保存
                    entity.GoodsPutawayState = goodsPutawayTypes;
                    entity.LastEditAt = DateTime.Now;
                    return db.SaveChanges() > 0;
                }
                else
                {
                    //下架
                    //判断商品状态是否为已上架
                    if (entity.GoodsPutawayState!=GoodsPutawayTypes.Putaway)
                    {
                        throw new Exception("已上架的商品，才能下架");
                    }
                    if (entity.GoodsPutawayState==GoodsPutawayTypes.Putaway&&goodsPutawayTypes==GoodsPutawayTypes.NotPutaway)
                    {
                        throw new Exception("已上架的商品，只能进行下架");
                    }
                    //修改上架状态和操作时间并保存
                    entity.GoodsPutawayState = goodsPutawayTypes;
                    entity.LastEditAt = DateTime.Now;
                    return db.SaveChanges() > 0;
                }
            }
        }
        /// <summary>
        /// 更新商品标签
        /// </summary>
        /// <param name="db">db</param>
        /// <param name="GoodsId">商品ID</param>
        /// <param name="TagsId">商品标签</param>
        private void UpdateGoodsTag(Db db,long GoodsId, long[] TagsId)
        {
            //获取旧标签的集合
            var oldTags = db.GoodsTagRepo.Where(o => o.GoodsId == GoodsId).ToList();
            //获取新标签的集合
            var newTags = TagsId.Select(o => new GoodsTagRepo
            {
                Id=0,
                GoodsId=GoodsId,
                TagId=o
            }).ToList();
            //删除旧标签集合
            db.GoodsTagRepo.RemoveRange(oldTags);
            //新增新标签集合
            db.GoodsTagRepo.AddRange(newTags);
            ////遍历新标签集合对比旧标签集合
            //foreach (var item in newTags)
            //{
            //    //判断旧集合里是否存在新集合里的数据
            //    if (!oldTags.Any(o=>o.GoodsId==item.GoodsId&&o.TagId==item.TagId))
            //    {
            //        //不存在，新增
            //        db.GoodsTagRepo.Add(item);
            //    }
            //}
            ////遍历旧标签集合对比新标签集合
            //foreach (var item in oldTags)
            //{
            //    //判断新集合里是否存在旧集合里的数据
            //    if (!newTags.Any(o => o.GoodsId == item.GoodsId && o.TagId == item.TagId))
            //    {
            //        //不存在，删除
            //        db.GoodsTagRepo.Remove(item);
            //    }
            //}
            //保存
            db.SaveChanges();
        }
        /// <summary>
        /// 获取商品标签
        /// </summary>
        /// <param name="db">db</param>
        /// <param name="GoodsId">商品ID</param>
        /// <returns></returns>
        private List<TagDto> GetTags(Db db, long GoodsId)
        {
            var sql = $@"SELECT t.Id,t.TagName
                       FROM tag t JOIN goodstag gt ON t.Id=gt.TagID
                       WHERE gt.GoodsId={GoodsId}";
            var List = db.Database.SqlQuery<TagDto>(sql).ToList();
            return List;
        }
    }
}
