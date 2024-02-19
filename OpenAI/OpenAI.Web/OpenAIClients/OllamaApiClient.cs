using Dapr.Client;

namespace OpenAI.Web.OpenAIClients;

public sealed class OllamaApiClient(HttpClient httpClient, DaprClient daprClient)
    : OpenAIClientBase(httpClient, daprClient);