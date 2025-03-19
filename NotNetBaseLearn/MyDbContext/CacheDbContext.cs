using Microsoft.EntityFrameworkCore;
using NotNetBaseLearn.MyDbContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotNetBaseLearn.MyDbContext {
    internal class CacheDbContext : DbContext{
        public CacheDbContext(DbContextOptions<CacheDbContext> options) : base(options) { }
        public DbSet<JsonConfiguration> JsonConfigurations { get; set; }
    }
}
