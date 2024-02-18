// ReSharper disable ClassNeverInstantiated.Global
namespace OpenAI.Shared.Omnivore;

public sealed record ArticleRootObject(ArticleResult Article);

public sealed record ArticleResult(Article Article);

public sealed record Article(string Title,  string Content,  Labels[] Labels);

public sealed record Labels(string Name);

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
