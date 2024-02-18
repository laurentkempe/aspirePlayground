using GraphQL;
using GraphQL.Client.Abstractions;

namespace OpenAI.Shared.Omnivore;

public static class OmnivoreClient
{
  public static async Task<Article?> GetOmnivoreArticleContent(string articleSlug, IGraphQLClient graphQLClient)
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
}