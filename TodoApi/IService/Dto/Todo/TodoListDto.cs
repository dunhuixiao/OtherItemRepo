using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
    public class TodoListDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 代办项目描述
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool Completed { get; set; }
    }
}
