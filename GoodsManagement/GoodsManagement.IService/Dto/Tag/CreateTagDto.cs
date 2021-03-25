using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManagement.IService
{
    /// <summary>
    /// 标签新增模型
    /// </summary>
    public class CreateTagDto
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        [Required, MaxLength(100)]
        public string TagName { get; set; }
    }
}
