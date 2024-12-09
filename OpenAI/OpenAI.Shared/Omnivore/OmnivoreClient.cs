using GraphQL;
using GraphQL.Client.Abstractions;

namespace OpenAI.Shared.Omnivore;

public static class OmnivoreClient
{
  public static async Task<Article?> GetArticle(string articleSlug, IGraphQLClient graphQLClient)
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

    var articleRoot = await graphQLClient.SendQueryAsync<ArticleRootObject>(articleQueryRequest);

    if (articleRoot.Errors is not null) return null;

    return articleRoot.Data.Article.Article;
  }

  public static async Task<IEnumerable<ArticleDetails>?> GetTopArticles(IGraphQLClient graphQLClient, int count = 5)
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
          first = count
      }
    };

    var articleRoot = await graphQLClient.SendQueryAsync<Root>(articleQueryRequest);

    if (articleRoot.Errors is null)
        return articleRoot.Data.Search.Edges.Select(edge => edge.ArticleDetails);
    
    return [];
  }
}