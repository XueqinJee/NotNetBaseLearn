using ConsoleAppDemo.Model.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDemo.Model {
    internal class MyDbContext : DbContext{

        public MyDbContext(DbContextOptions builder) : base(builder) { }

        public DbSet<Settings> Settings { get; set; }
    }
}
