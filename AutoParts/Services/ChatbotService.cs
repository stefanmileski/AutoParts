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

            GenerateContentResponse? response = await _model.GenerateContent(prompt);
            return response.Text ?? string.Empty;
        }
    }
}