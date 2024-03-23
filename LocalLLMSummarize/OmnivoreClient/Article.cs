namespace OmnivoreClient;

public sealed record Article(string Title, string Author, string Url, string Slug, string Content,  Labels[] Labels);

public sealed record Labels(string Name);

public sealed record SummarizedArticle(string Title, string Summary, string Author, string Url,  string[] Labels);