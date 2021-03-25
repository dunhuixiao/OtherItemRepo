using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.IService
{
    /// <summary>
    /// 商品接口
    /// </summary>
    public interface IGoodsService
    {
        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="dto">新增商品模型</param>
        /// <returns></returns>
        long Create(CreateGoodsDto dto);
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="GoodsId">商品ID</param>
        /// <returns></returns>
        bool Delete(int GoodsId);
        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="dto">商品更新模型</param>
        /// <returns></returns>
        long Updete(UpdateGoodsDto dto);
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="dto">分页及搜索模型</param>
        /// <returns></returns>
        List<GoodsDto> GetList(QueryGoodsDto dto);
        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="GoodsId">商品ID</param>
        /// <returns></returns>
        GoodsDto GetDetail(long GoodsId);
        /// <summary>
        /// 设置上架状态
        /// </summary>
        /// <param name="GoodsId">商品ID</param>
        /// <param name="goodsPutawayTypes">商品上架状态</param>
        /// <returns></returns>
        bool SetPutawayState(long GoodsId, GoodsPutawayTypes goodsPutawayTypes);
    }
}
