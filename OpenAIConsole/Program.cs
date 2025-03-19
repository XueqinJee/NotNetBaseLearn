
using OpenAIConsole;
using System.Text;
using System.Text.Json;

string baseUrl = "http://127.0.0.1:11434/api";

var client = new HttpClient() {
    //BaseAddress = new Uri(baseUrl)
};

var aiClient = new AiClient();

while (true) {
    Console.Write("我：");
    string read = Console.ReadLine();

    StringContent jsonContent = new(
        JsonSerializer.Serialize(new {
            model = "phi4-mini",
            stream = true,
            prompt = aiClient.Prompt(read)
        }),
        Encoding.UTF8,
        "application/json");

    var response = await client.PostAsync("http://127.0.0.1:11434/api/generate", jsonContent);

    using (var responseStream = await response.Content.ReadAsStreamAsync()) {

        using var reader = new StreamReader(responseStream);
        string? line;
        Console.Write("AI：");
        while ((line = reader.ReadLine()) != null) {
            if (string.IsNullOrEmpty(line)) {
                continue;
            }
            string data = line.Substring(6);
            var obj = JsonSerializer.Deserialize<Rootobject>(line);
            Console.Write(obj.response);
            if (obj.done) {
                Console.Write("\n");
            }
        }
    }
}

