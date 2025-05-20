namespace OpenAI.Shared.Omnivore;

public class Article
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public List<Label>? Labels { get; set; }
}

public class ArticleRootObject
{
    public ArticleData? Data { get; set; }
}