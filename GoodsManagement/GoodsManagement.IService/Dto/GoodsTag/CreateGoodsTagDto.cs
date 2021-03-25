using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.IService
{
    /// <summary>
    /// 商品标签保存模型
    /// </summary>
    public class CreateGoodsTagDto
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long GoodsId { get; set; }
        /// <summary>
        /// 标签ID
        /// </summary>
        public long TagId { get; set; }
    }
}
