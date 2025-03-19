using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDemo.Config.Extensions {
    internal static class ConfigurationExtionsion {

        public static void AddEntityCacheConfiguration(this IConfigurationBuilder config, Action<DbContextOptionsBuilder> options) {

            var source = new EntityCacheConfigSource(options);
            config.Add(source);
        }
    }
}
