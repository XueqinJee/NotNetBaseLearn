using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDemo.Options {
    internal class BookOption {
        public readonly static string Book = "Book";

        [Required(ErrorMessage = "Name 不可为空！")]
        public string? Name { get; set; }

        [Range(0, 100, ErrorMessage = "范围只可在 0 - 100 之间")]
        public int Age { get; set; }

        public string? Author { get; set; }
        public string? Description { get; set; }
    }
}
