using IService;
using IService.IService;
using Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Service
{
    public class TodoService: ITodoService
    {
        private readonly Db db;
        public TodoService(Db db)
        {
            this.db = db;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<TodoListDto> GetList()
        {
            using (db)
            {
                var result = db.TodoRepos.Where(o => !o.Deleted).Select(o => new TodoListDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Completed = o.Completed
                }).ToList();
                return result;
            }
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TodoListDto GetDetail(long id)
        {
            using (db)
            {
                var result = db.TodoRepos.Where(o => o.Id == id && !o.Deleted).Select(o => new TodoListDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Completed = o.Completed
                }).FirstOrDefault();
                if (result == null)
                {
                    return new TodoListDto();
                }
                return result;
            }
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Create(string value)
        {
            using (db)
            {
                var result = new TodoRepo
                {
                    Name = value,
                    Completed = false,
                    Deleted=false
                };
                db.TodoRepos.Add(result);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dto">更新todoDto</param>
        /// <returns></returns>
        public bool Update(UpdateTodoDto dto)
        {
            using (db)
            {
                var result = db.TodoRepos.Where(o => o.Id == dto.Id && !o.Deleted).FirstOrDefault();
                if (result == null)
                {
                    throw new Exception("DataError");
                }
                result.Name = dto.Value;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            using (db)
            {
                var result = db.TodoRepos.Where(o => o.Id == id).FirstOrDefault();
                if (result == null)
                {
                    throw new Exception("DataError");
                }
                if (result.Deleted)
                {
                    return true;
                }
                result.Deleted = true;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Completed(long id)
        {
            using (db)
            {
                var result = db.TodoRepos.Where(o => o.Id == id).FirstOrDefault();
                if (result == null)
                {
                    throw new Exception("DataError");
                }
                if (result.Completed)
                {
                    return true;
                }
                result.Completed = true;
                return db.SaveChanges() > 0;
            }
        }
    }
}
