using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.IService
{
    /// <summary>
    /// 商品查询模型
    /// </summary>
    public class GoodsDto
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodsCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public double GoodsPrice { get; set; }
        /// <summary>
        /// 商品备注
        /// </summary>
        public string GoodsRemark { get; set; }
        /// <summary>
        /// 商品上架状态
        /// </summary>
        public GoodsPutawayTypes GoodsPutawayState { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastEditAt { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }
        /// <summary>
        /// 商品标签
        /// </summary>
        public List<TagDto> Tags { get; set; }
    }
}
