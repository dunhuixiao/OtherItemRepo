using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GoodsManagement.Service.Entity
{
    class Db : DbContext
    {
        /// <summary>
        /// DB
        /// </summary>
        public Db() : base("name=Mycontent")
        {
            Database.SetInitializer<Db>(null);
        }

        public DbSet<GoodsRepo> GoodsRepo { get; set; }
        public DbSet<TagRepo> TagRepo { get; set; }
        public DbSet<GoodsTagRepo> GoodsTagRepo { get; set; }

    }
}
