using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotNetBaseLearn.Options {
    internal class BookOptionsValidated : IValidateOptions<BookOptions> {
        public ValidateOptionsResult Validate(string? name, BookOptions options) {
            
            var failures = new List<string>();
            if(options.Age >= 50) {
                failures.Add($"Age 必须小于 50");
            }

            if (failures.Any()) {
                return ValidateOptionsResult.Fail( failures );
            }

            return ValidateOptionsResult.Success;
        }
    }
}
