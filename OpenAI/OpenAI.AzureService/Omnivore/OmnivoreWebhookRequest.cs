// ReSharper disable ClassNeverInstantiated.Global

namespace OpenAI.AzureService.Omnivore;

/// <summary>
/// Represents a request object received from the Omnivore webhook.
/// Read more about it at https://docs.omnivore.app/integrations/webhooks.html
/// </summary>
public sealed class OmnivoreWebhookRequest
{
    [JsonPropertyName("action")]
    public string Action { get; set; }

    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("page")]
    public required Page Page { get; set; }
}

public sealed class Page
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("slug")]
    public string Slug { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("author")]
    public string Author { get; set; }

    [JsonPropertyName("originalUrl")]
    public string OriginalUrl { get; set; }

    [JsonPropertyName("itemType")]
    public string ItemType { get; set; }
    
    [JsonPropertyName("textContentHash")]
    public string TextContentHash { get; set; }

    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; }

    [JsonPropertyName("publishedAt")]
    public DateTime PublishedAt { get; set; }

    [JsonPropertyName("readingProgressTopPercent")]
    public decimal ReadingProgressTopPercent { get; set; }

    [JsonPropertyName("readingProgressHighestReadAnchor")]
    public decimal ReadingProgressHighestReadAnchor { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("savedAt")]
    public DateTime SavedAt { get; set; }

    [JsonPropertyName("siteName")]
    public string SiteName { get; set; }

    [JsonPropertyName("itemLanguage")]
    public string ItemLanguage { get; set; }

    [JsonPropertyName("siteIcon")]
    public string SiteIcon { get; set; }

    [JsonPropertyName("wordCount")]
    public int WordCount { get; set; }

    [JsonPropertyName("archivedAt")]
    public object ArchivedAt { get; set; }
}