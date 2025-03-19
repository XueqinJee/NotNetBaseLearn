using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIConsole {
    public class Rootobject {
        public string model { get; set; }
        public DateTime created_at { get; set; }
        public string response { get; set; }
        public bool done { get; set; }
    }
}
