namespace Tools.API.Data
{
    public class ToolInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Tool>().AnyAsync(token: cancellation))
            {
                return;
            }

            session.Store(GetPreconfiguredTools());
            await session.SaveChangesAsync(cancellation);
        }

        private static IEnumerable<Tool> GetPreconfiguredTools() =>
            [
                new() {
                    Id = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                    Name = "ChatGPT",
                    Description = "ChatGPT helps you get answers, find inspiration and be more productive.",
                    Category = ["Chatbot"]
                },
                new() {
                    Id = new Guid("01980a23-be29-487e-a329-939cfc0012f2"),
                    Name = "Hugging Face",
                    Description = "The platform where the machine learning community collaborates on models, datasets, and applications.",
                    Category = ["AI Platform"]
                },
                new() {
                    Id = new Guid("01980a23-8457-4cf0-815c-939cfc0012f2"),
                    Name = "Google Gemini",
                    Description = "Meet Gemini, your personal AI assistant.",
                    Category = ["AI assistant"]
                }
            ];
    }
}
