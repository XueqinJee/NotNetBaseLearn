using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotNetBaseLearn.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotNetBaseLearn {
    public static class ConfigurationManagerExtensions {

        public static IConfigurationBuilder AddEntityConfiguration(this IConfigurationBuilder manager, Action<DbContextOptionsBuilder> optionsAction) {

            manager.Add(new EFConfigurationSource(optionsAction));
            return manager;
        }
    }
}
