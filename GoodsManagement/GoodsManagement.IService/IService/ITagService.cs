using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.IService
{
    /// <summary>
    /// 标签接口
    /// </summary>
    public interface ITagService
    {
        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="dto">新增标签模型</param>
        /// <returns></returns>
        bool Create(CreateTagDto dto);
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="TagId">标签ID</param>
        /// <returns></returns>
        bool Delete(int TagId);
        /// <summary>
        /// 修改标签
        /// </summary>
        /// <param name="dto">标签修改模型</param>
        /// <returns></returns>
        bool Update(UpdateTagDto dto);
        /// <summary>
        /// 查询标签列表，也可作为下拉列表
        /// </summary>
        /// <returns></returns>
        List<TagDto> GetList();
        /// <summary>
        /// 查询标签详情
        /// </summary>
        /// <param name="TagId">标签ID</param>
        /// <returns></returns>
        TagDto GetDetail(int TagId);
    }
}
