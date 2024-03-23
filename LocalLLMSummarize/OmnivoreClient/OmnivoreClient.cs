using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace OmnivoreClient;

public class OmnivoreClient
{
    private readonly GraphQLHttpClient _graphQLClient;

    public OmnivoreClient(string omnivoreUrl, string authToken)
    {
        _graphQLClient = new GraphQLHttpClient(omnivoreUrl, new SystemTextJsonSerializer());
        _graphQLClient.HttpClient.DefaultRequestHeaders.Add("Cookie", $"auth={authToken};");
    }

    public async Task<Article?> GetArticleContent(string articleSlug)
  {
    var articleQueryRequest = new GraphQLRequest
    {
      Query = $$"""
                query Article {
                   article(
                     slug: "{{articleSlug}}"
                     username: "."
                     format: "markdown"
                     ) {
                       ... on ArticleSuccess {
                         article {
                           title
                           author
                           slug
                           url
                           content
                           labels {
                             name
                           }
                         }
                       }
                     }
                   }
                """
    };

    var articleRoot = await _graphQLClient.SendQueryAsync<ArticleRootObject>(articleQueryRequest);

    if (articleRoot.Errors is not null) return null;

    return articleRoot.Data.Article.Article;
  }

  public async Task<IEnumerable<ArticleDetails>?> GetTopArticles(string? after = "0", int first = 5, string query = "in:inbox type:article")
  {
    var articleQueryRequest = new GraphQLRequest
    {
      Query = """
              query Search($after: String, $first: Int, $query: String) {
                  search(first: $first, after: $after, query: $query) {
                      ... on SearchSuccess {
                          edges {
                              cursor
                              node {
                                  id
                                  title
                                  slug
                                  url
                                  pageType
                                  createdAt
                                  isArchived
                                  author
                                  image
                                  description
                                  publishedAt
                                  labels {
                                      id
                                      name
                                      color
                                  }
                                  wordsCount
                              }
                          }
                      }
                      ... on SearchError {
                          errorCodes
                      }
                  }
              }
              """,
      Variables = new {
          after = after,
          first = first,
          query = query
      }
    };

    var articleRoot = await _graphQLClient.SendQueryAsync<ArticleDetailsRoot>(articleQueryRequest);

    if (articleRoot.Errors is null)
        return articleRoot.Data.Search.Edges.Select(edge => edge.ArticleDetails);
    
    return [];
  }
}