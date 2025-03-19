using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDemo.Config.Extensions {
    internal class EntityCacheConfigSource : IConfigurationSource {
        private readonly Action<DbContextOptionsBuilder> _action;

        public EntityCacheConfigSource(Action<DbContextOptionsBuilder> action) {
            _action = action;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) {
            return new EntityCacheConfigProvider(_action);
        }
    }
}
