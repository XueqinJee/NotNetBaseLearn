using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIConsole {
    public class AiClient {
        string baseUrl = "http://127.0.0.1:11434/api";
        private readonly HttpClient _httpClient;
        public AiClient() {
            _httpClient = new HttpClient() { 
                BaseAddress = new Uri(baseUrl)
            };
        }

        public string Prompt(string message) { 
            var content = new StringBuilder();
            content.Append(message);
            return content.ToString();
        }
    }
}
