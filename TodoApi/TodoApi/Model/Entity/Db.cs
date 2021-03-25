using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApi.Model
{
    /// <summary>
    /// Db
    /// </summary>
    public class Db : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>       
        public Db(DbContextOptions<Db> options) : base(options)
        { }

        /// <summary>
        /// 待办事项
        /// </summary>
        public DbSet<TodoRepo> TodoRepos { get; set; }
    }
}
