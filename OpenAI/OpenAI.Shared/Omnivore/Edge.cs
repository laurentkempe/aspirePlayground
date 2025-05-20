namespace OpenAI.Shared.Omnivore;

public class Edge
{
    public string? Cursor { get; set; }
    public Node? Node { get; set; }
    
    public ArticleDetails ArticleDetails => new()
    {
        Id = Node?.Id,
        Title = Node?.Title,
        Slug = Node?.Slug,
        Url = Node?.Url,
        PageType = Node?.PageType,
        CreatedAt = Node?.CreatedAt ?? DateTime.MinValue,
        IsArchived = Node?.IsArchived ?? false,
        Author = Node?.Author,
        Image = Node?.Image,
        Description = Node?.Description,
        PublishedAt = Node?.PublishedAt,
        Labels = Node?.Labels,
        WordsCount = Node?.WordsCount ?? 0
    };
}