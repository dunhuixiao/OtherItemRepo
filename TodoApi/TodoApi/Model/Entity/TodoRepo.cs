using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Model
{
    /// <summary>
    /// 代办项目
    /// </summary>
    [Table("todolist")]
    public class TodoRepo
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 代办项目描述
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsComplete { get; set; } = false;
    }
}
