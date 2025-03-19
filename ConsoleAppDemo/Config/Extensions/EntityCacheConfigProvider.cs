using ConsoleAppDemo.Model;
using ConsoleAppDemo.Model.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDemo.Config.Extensions {
    internal class EntityCacheConfigProvider : ConfigurationProvider {
        private readonly Action<DbContextOptionsBuilder> _action;

        public EntityCacheConfigProvider(Action<DbContextOptionsBuilder> action) {
            _action = action;
        }

        public override void Load() {
            var builder = new DbContextOptionsBuilder<MyDbContext>();

            _action(builder);
            using var dbContext = new MyDbContext(builder.Options);
            dbContext.Database.EnsureCreated();
            if (!dbContext.Settings.Any()) {
                var dic = new Dictionary<string, string>() {
                    {"Test:A", "1" },
                    {"Test:B", "2" },
                    {"Test:C", "3" },
                };

                dbContext.AddRange(dic.Select(x => new Settings { Key = x.Key, Value = x.Value}).ToList());
                dbContext.SaveChanges();
            }

            Data = dbContext.Settings.ToDictionary(x => x.Key == null ? "" : x.Key, x => x.Value);
        }
    }
}
