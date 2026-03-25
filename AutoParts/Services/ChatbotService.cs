namespace AutoParts.Services
{
    using Mscc.GenerativeAI;
    using Mscc.GenerativeAI.Types;

    public class ChatbotService(GenerativeModel model)
    {
        private readonly GenerativeModel _model = model;

        public async Task<string> GenerateResponseAsync(string userQuery, string carModelName, List<Models.Part> parts)
        {
            string prompt = $"""
                You are an assistant for an auto parts shop.
                The customer drives a {carModelName} and reports: "{userQuery}"

                Available compatible parts from inventory:
                {string.Join("\n", parts.Select(p =>
                    $"- {p.Name} | Category: {p.PartCategory.Name} | OEM: {p.OemNumber} | Price: {p.Price:C} | In stock: {p.StockQuantity}"))}

                Recommend the most suitable part(s) based on the customer's problem.
                Only use the parts listed above — do not suggest anything outside this list.
                """;

            var apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");

            using var http = new HttpClient();
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

            var body = new
            {
                contents = new[]
                {
            new { parts = new[] { new { text = prompt } } }
        }
            };

            var json = System.Text.Json.JsonSerializer.Serialize(body);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await http.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Gemini status: {response.StatusCode}");
            Console.WriteLine($"Gemini response: {responseBody}");

            if (!response.IsSuccessStatusCode)
                return $"Gemini error: {response.StatusCode}";

            using var doc = System.Text.Json.JsonDocument.Parse(responseBody);
            return doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? string.Empty;
        }
    }
}