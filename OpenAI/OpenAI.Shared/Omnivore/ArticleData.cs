namespace OpenAI.Shared.Omnivore;

public class ArticleData
{
    public ArticleSuccess? Article { get; set; }
}

public class ArticleSuccess
{
    public Article? Article { get; set; }
}