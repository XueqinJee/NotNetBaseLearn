using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotNetBaseLearn.Providers {
    internal class EFConfigurationSource : IConfigurationSource {
        private readonly Action<DbContextOptionsBuilder> _action;
        public EFConfigurationSource(Action<DbContextOptionsBuilder> optionAction){
            this._action = optionAction;
        }



        public IConfigurationProvider Build(IConfigurationBuilder builder) {
            return new EFConfigurationProvider(_action);
        }
    }
}
