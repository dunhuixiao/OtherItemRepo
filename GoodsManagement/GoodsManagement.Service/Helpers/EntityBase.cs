using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.Service
{
    public class EntityBase
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public int RowVersion { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatorId { get; set; }

        /// <summary>
        /// 最后编辑人
        /// </summary>
        public long LastEditorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime LastEditAt { get; set; } = DateTime.Now;
    }
}
