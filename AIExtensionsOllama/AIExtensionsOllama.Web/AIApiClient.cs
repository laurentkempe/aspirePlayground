namespace AIExtensionsOllama.Web;

public class AIApiClient(HttpClient httpClient)
{
    public async Task<string> GetAnswerAsync(string query, CancellationToken cancellationToken = default)
    {
        var chatResponse = await httpClient.GetAsync($"/chat/{query}", cancellationToken);
        
        return await chatResponse.Content.ReadAsStringAsync(cancellationToken);
    }
}