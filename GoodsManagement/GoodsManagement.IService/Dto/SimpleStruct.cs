namespace GoodsManagement.IService
{
    /// <summary>
    /// 简单数据结构，基础层，主要用于输出基础列表和继承
    /// </summary>
    public class SimpleStruct
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EName { get; set; }
    }
}