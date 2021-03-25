using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
    /// <summary>
    /// 更新todoDto
    /// </summary>
    public class UpdateTodoDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// todoValue
        /// </summary>
        public string Value { get; set; }
    }
}
