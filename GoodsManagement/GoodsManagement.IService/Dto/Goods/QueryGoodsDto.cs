using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.IService
{
    /// <summary>
    /// 分页及搜索模型
    /// </summary>
    public class QueryGoodsDto
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 分页显示数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 上架状态
        /// </summary>
        public GoodsPutawayTypes? GoodsPutawayState { get; set; }
        /// <summary>
        /// 商品标签
        /// </summary>
        public long? TagId { get; set; }
    }
}
