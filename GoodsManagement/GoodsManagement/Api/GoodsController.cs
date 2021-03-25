using GoodsManagement.IService;
using GoodsManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GoodsManagement.Api
{
    /// <summary>
    /// 商品API
    /// </summary>
    public class GoodsController : ApiController
    {
        /// <summary>
        /// 引入接口
        /// </summary>
        private readonly IGoodsService _goodsService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public GoodsController()
        {
            _goodsService = new GoodsService();
        }

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="dto">新增商品模型</param>
        /// <returns></returns>
        [HttpPost]
        public long Create(CreateGoodsDto dto)
        {
            //校验模型是否为空
            if (dto == null)
            {
                throw new Exception("数据无效");
            }
            dto.Validate();
            var flag = _goodsService.Create(dto);
            return flag;
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="GoodsId">商品ID</param>
        /// <returns></returns>
        [HttpPost]
        public bool Delete(int GoodsId)
        {
            //校验商品ID是否为正常格式
            if (GoodsId <= 0)
            {
                throw new Exception("数据无效");
            }
            var flag = _goodsService.Delete(GoodsId);
            return flag;
        }
        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="dto">商品更新模型</param>
        /// <returns></returns>
        [HttpPost]
        public long Updete(UpdateGoodsDto dto)
        {
            //校验模型是否为空
            if (dto == null)
            {
                throw new Exception("数据无效");
            }
            dto.Validate();
            var flag = _goodsService.Updete(dto);
            return flag;
        }
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="dto">分页及搜索模型</param>
        /// <returns></returns>
        [HttpGet]
        public List<GoodsDto> GetList([FromUri]QueryGoodsDto dto)
        {
            //校验模型是否为空
            if (dto == null)
            {
                throw new Exception("数据无效");
            }
            if (dto.PageIndex<=0||dto.PageSize!=20)
            {
                throw new Exception("分页错误");
            }
            var List = _goodsService.GetList(dto);
            return List;
        }
        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="GoodsId">商品ID</param>
        /// <returns></returns>
        [HttpGet]
        public GoodsDto GetDetail(long GoodsId)
        {
            //校验商品ID是否为正常格式
            if (GoodsId <= 0)
            {
                throw new Exception("数据无效");
            }
            var flag = _goodsService.GetDetail(GoodsId);
            return flag;
        }
        /// <summary>
        /// 设置上架状态
        /// </summary>
        /// <param name="GoodsId">商品ID</param>
        /// <param name="goodsPutawayTypes">商品上架状态</param>
        /// <returns></returns>
        [HttpPost]
        public bool SetGoodsPutawayState(long GoodsId, GoodsPutawayTypes goodsPutawayTypes)
        {
            //校验商品ID是否为正常格式
            if (GoodsId <= 0)
            {
                throw new Exception("数据无效");
            }
            var flag = _goodsService.SetPutawayState(GoodsId, goodsPutawayTypes);
            return flag;
        }
    }
}