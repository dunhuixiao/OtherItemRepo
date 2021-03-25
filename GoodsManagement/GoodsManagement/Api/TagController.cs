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
    /// 标签Api
    /// </summary>
    public class TagController : ApiController
    {
        /// <summary>
        /// 引入接口
        /// </summary>
        private readonly ITagService _tagService;


        /// <summary>
        /// 构造函数
        /// </summary>
        public TagController()
        {
            _tagService = new TagService();
        }

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="dto">新增标签模型</param>
        /// <returns></returns>
        [HttpPost]
        public bool Create(CreateTagDto dto)
        {
            //校验模型是否为空
            if (dto == null)
            {
                throw new Exception("数据无效");
            }
            dto.Validate();
            var flag = _tagService.Create(dto);
            return flag;
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="TagId">标签ID</param>
        /// <returns></returns>
        [HttpPost]
        public bool Delete(int TagId)
        {
            //校验标签ID是否为正常格式
            if (TagId <= 0)
            {
                throw new Exception("数据无效");
            }
            var flag = _tagService.Delete(TagId);
            return flag;
        }

        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="dto">标签修改模型</param>
        /// <returns></returns>
        [HttpPost]
        public bool Update(UpdateTagDto dto)
        {
            //校验模型是否为空
            if (dto == null)
            {
                throw new Exception("数据无效");
            }
            dto.Validate();
            var flag = _tagService.Update(dto);
            return flag;
        }

        /// <summary>
        /// 查询标签列表，也可作为下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<TagDto> GetList()
        {
            var list = _tagService.GetList();
            return list;
        }

        /// <summary>
        /// 查询标签详情
        /// </summary>
        /// <param name="TagId">标签ID</param>
        /// <returns></returns>
        [HttpGet]
        public TagDto GetDetail(int TagId)
        {
            //校验标签ID是否为正常格式
            if (TagId <= 0)
            {
                throw new Exception("数据无效");
            }
            var detail = _tagService.GetDetail(TagId);
            return detail;
        }
    }
}