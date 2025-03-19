using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotNetBaseLearn.MyDbContext;

namespace NotNetBaseLearn.Providers {
    internal class EFConfigurationProvider : ConfigurationProvider{

        Action<DbContextOptionsBuilder> optionsAction { get; set; }

        public EFConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction) {
            this.optionsAction = optionsAction;
        }

        public override void Load() {
            var builder = new DbContextOptionsBuilder<CacheDbContext>();

            optionsAction(builder);
            using var dbContext = new CacheDbContext(builder.Options);
            dbContext.Database.EnsureCreated();
            if (!dbContext.JsonConfigurations.Any()) {
                CreateAndSaveDefaultValues(dbContext);
            }

            // 如果没有任何配置添加默认配置
            Data = dbContext.JsonConfigurations.ToDictionary(x => x.Key, x => x.Value);
        }

        public IDictionary<string, string?> CreateAndSaveDefaultValues(CacheDbContext dbContext) { 
            var configValues = 
                new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase) {
                    {"Setting:Key", "2" },
                    {"Setting:Value", "2" }
                };

            if(dbContext == null || dbContext.JsonConfigurations == null) {
                throw new Exception("Null DB context");
            }
            dbContext.JsonConfigurations.AddRange(configValues.Select(
                x => new MyDbContext.Model.JsonConfiguration { 
                    Key = x.Key,
                    Value = x.Value
                }).ToList());

            dbContext.SaveChanges();
            return configValues;
        }
    }
}
