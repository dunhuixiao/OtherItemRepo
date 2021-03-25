using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.IService
{
    /// <summary>
    /// 商品保存模型
    /// </summary>
    public class CreateGoodsDto
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string GoodsCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        [Required]
        public double GoodsPrice { get; set; }
        /// <summary>
        /// 商品备注
        /// </summary>
        [MaxLength(2000)]
        public string GoodsRemark { get; set; }
        /// <summary>
        /// 商品标签
        /// </summary>
        [MaxLength(5)]
        public long[] TagsId { get; set; }
    }
}
