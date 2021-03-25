using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using IService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Model;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Todo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly ITodoService _todoIService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="todoIService"></param>
        public TodoController(ITodoService todoIService)
        {
            _todoIService = todoIService;
        }        

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public List<TodoListDto> GetList()
        {
            var result = _todoIService.GetList();
            return result;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>代办项目详情</returns>
        [HttpGet("[action]")]
        public TodoListDto GetDetail(long id)
        {
            var result = _todoIService.GetDetail(id);
            return result;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="value">todoValue</param>
        /// <returns>true/false</returns>
        [HttpPost("[action]")]
        public bool Create([FromBody] string value)
        {
            var result = _todoIService.Create(value);
            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns>true/false</returns>
        [HttpPost("[action]")]
        public bool Update([FromBody]UpdateTodoDto dto)
        {
            var result = _todoIService.Update(dto);
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>true/false</returns>
        [HttpPost("[action]")]
        public bool Delete([FromBody]int id)
        {
            var result = _todoIService.Delete(id);
            return result;
        }

        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public bool Completed([FromBody]long id)
        {
            var result = _todoIService.Completed(id);
            return result;
        }
    }
}
