using System;
using System.Collections.Generic;
using System.Text;

namespace IService.IService
{
    public interface ITodoService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        List<TodoListDto> GetList();
        
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TodoListDto GetDetail(long id);

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Create(string value);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dto">更新todoDto</param>
        /// <returns></returns>
        bool Update(UpdateTodoDto dto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(long id);

        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Completed(long id);
    }
}