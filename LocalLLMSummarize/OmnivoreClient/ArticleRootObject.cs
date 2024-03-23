namespace OmnivoreClient;

public sealed record ArticleRootObject(ArticleResult Article);

public sealed record ArticleResult(Article Article);

/* Sample JSON response from the Omnivore GraphQL API
{
  "data": {
    "article": {
      "article": {
        "title": "Resilience and chaos engineering - .NET Blog",
        "content": " Feb.",
        "labels": [
          {
            "name": "Rss"
          },
          {
            "name": "RSS"
          }
        ]
      }
    }
  }
}
*/