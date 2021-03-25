using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.IService
{
    /// <summary>
    /// 标签修改模型
    /// </summary>
    public class UpdateTagDto
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required]
        public long Id { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        [Required, MaxLength(100)]
        public string TagName { get; set; }
    }
}
