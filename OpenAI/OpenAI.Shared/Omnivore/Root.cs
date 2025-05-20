namespace OpenAI.Shared.Omnivore;

public class Root
{
    public RootData? Data { get; set; }
}

public class RootData
{
    public SearchResult? Search { get; set; }
}

public class SearchResult
{
    public List<Edge>? Edges { get; set; }
}