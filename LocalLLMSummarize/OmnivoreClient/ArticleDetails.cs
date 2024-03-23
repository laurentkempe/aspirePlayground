using System.Text.Json.Serialization;

namespace OmnivoreClient;

public record ArticleDetailsRoot(
    [property: JsonPropertyName("search")] Search Search
);

public record Search(
    [property: JsonPropertyName("edges")] Edge[] Edges
);

public record Edge(
    [property: JsonPropertyName("cursor")] string Cursor,
    [property: JsonPropertyName("node")] ArticleDetails ArticleDetails
);

public record ArticleDetails(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("pageType")] string PageType,
    [property: JsonPropertyName("createdAt")] string CreatedAt,
    [property: JsonPropertyName("isArchived")] bool IsArchived,
    [property: JsonPropertyName("author")] string Author,
    [property: JsonPropertyName("image")] string Image,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("publishedAt")] string? PublishedAt,
    [property: JsonPropertyName("labels")] Label[] Labels,
    [property: JsonPropertyName("wordsCount")] int WordsCount
);

public record Label(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("color")] string Color
);

/* Sample JSON response from the Omnivore GraphQL API
{
"data": {
    "search": {
        "edges": [
        {
            "cursor": "5",
            "node": {
                "id": "4f5b3ce6-d1ac-11ee-9a1b-0fdc30ed5d6b",
                "title": "👉 3 tips for effective prompting with GitHub Copilot 👈",
                "slug": "3-tips-for-effective-prompting-with-git-hub-copilot-18dd1fa2896",
                "url": "https://omnivore.app/no_url?q=167950d7-cee6-421f-a7fb-52aeb6a5865f",
                "pageType": "UNKNOWN",
                "createdAt": "2024-02-22T18:00:49.000Z",
                "isArchived": false,
                "author": "GitHub",
                "image": "https://proxy-prod.omnivore-image-cache.app/600x0,saWHd3yZvLLs2SpSFs1LxzPyIt-3APZcGTVDKd7qGRfo/https://images.github.media/EloquaImages/clients/GitHubInc/%7Bd1be3463-5b88-4ae9-8839-401e08b5fd70%7D_gh-insider-feb-2-5.png",
                "description": "",
                "publishedAt": null,
                "labels": [
                {
                    "id": "a7206a10-c703-11ec-8c12-c32bf745c970",
                    "name": "Newsletter",
                    "color": "#07D2D1"
                }
                ],
                "wordsCount": 935
            }
        },
*/