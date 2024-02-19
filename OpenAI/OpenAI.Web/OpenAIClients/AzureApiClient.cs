using Dapr.Client;

namespace OpenAI.Web.OpenAIClients;

public sealed class AzureApiClient(HttpClient httpClient, DaprClient daprClient) 
    : OpenAIClientBase(httpClient, daprClient);