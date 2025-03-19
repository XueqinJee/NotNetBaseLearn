using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDemo.Options {
    internal class BookValidateOption : IValidateOptions<BookOption> {
        public ValidateOptionsResult Validate(string? name, BookOption options) {
            var fails = new List<string>();

            if(options.Age < 20) {
                fails.Add("Age 不可小于20");
            }

            if (fails.Any()) {
                return ValidateOptionsResult.Fail(fails);
            }

            return ValidateOptionsResult.Success;
        }
    }
}
