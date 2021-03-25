using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.IService
{
    /// <summary>
    /// 上架类型
    /// </summary>
    public enum GoodsPutawayTypes
    {
        /// <summary>
        /// 未上架
        /// </summary>
        [Description("未上架")]
        NotPutaway = 1,
        /// <summary>
        /// 已上架
        /// </summary>
        [Description("已上架")]
        Putaway = 2,
        /// <summary>
        /// 下架
        /// </summary>
        [Description("下架")]
        SoldOut = 3,
    }
}
