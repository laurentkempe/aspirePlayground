namespace OpenAI.Shared.Omnivore;

public class Node
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public string? Url { get; set; }
    public string? PageType { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsArchived { get; set; }
    public string? Author { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
    public DateTime? PublishedAt { get; set; }
    public List<Label>? Labels { get; set; }
    public int WordsCount { get; set; }
}