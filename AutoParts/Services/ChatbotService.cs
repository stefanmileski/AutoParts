namespace AutoParts.Services
{
    public class ChatbotService
    {
        public async Task<string> GenerateResponseAsync(string userQuery, string carModelName, List<Models.Part> parts)
        {
            string systemPrompt = $"""
                You are an assistant for an auto parts shop.
                The customer drives a {carModelName}.

                Available compatible parts from inventory:
                {string.Join("\n", parts.Select(p =>
                    $"- {p.Name} | Category: {p.PartCategory.Name} | OEM: {p.OemNumber} | Price: {p.Price:C} | In stock: {p.StockQuantity}"))}

                Recommend the most suitable part(s) based on the customer's problem.
                Only use the parts listed above — do not suggest anything outside this list.
                """;

            var apiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY");

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var body = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userQuery }
                }
            };

            var json = System.Text.Json.JsonSerializer.Serialize(body);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await http.PostAsync("https://api.groq.com/openai/v1/chat/completions", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Groq status: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
                return $"Error: {response.StatusCode}";

            using var doc = System.Text.Json.JsonDocument.Parse(responseBody);
            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? string.Empty;
        }
    }
}