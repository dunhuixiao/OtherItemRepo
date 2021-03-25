using GoodsManagement.IService;
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
    /// 商品表
    /// </summary>
    [Table("goods")]
    public class GoodsRepo:EntityBase
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
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
        public double GoodsPrice { get; set;}
        /// <summary>
        /// 商品备注
        /// </summary>
        public string GoodsRemark { get; set; }
        /// <summary>
        /// 商品上架状态
        /// </summary>
        public GoodsPutawayTypes GoodsPutawayState { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

    }
}
