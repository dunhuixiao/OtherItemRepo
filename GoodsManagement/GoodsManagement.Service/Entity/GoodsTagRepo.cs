using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.Service.Entity
{
    /// <summary>
    /// 商品标签表
    /// </summary>
    [Table("goodstag")]
    public class GoodsTagRepo
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
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
