# Omnivore, .NET Aspire and Azure OpenAI

The original idea comes from the [omnivore](https://omnivore.app/) blog post, by [Jan Beck](https://github.com/jancbeck), [Using ChatGPT to automatically add annotations to your Omnivore saved articles](https://blog.omnivore.app/p/using-chatgpt-to-automatically-add). His idea was to use ChatGPT to automatically add annotations to your Omnivore saved articles.

I wanted to port the idea to .NET Aspire and see how it would work. And it worked very well 👍🏼

You can find Jan JavaScript implementation at [jancbeck/omnivore-ai-annotations](
https://github.com/jancbeck/omnivore-ai-annotations#readme).

# TODO

- [ ] Save the summary to the omnivore article
- [ ] Provide a way to deploy the service to Azure Container Apps usin azd

⚠️ Work in progress ⚠️

# Setup and configuration

You also need to create an Azure Open AI gpt-35-turbo-16k deployment.

Configure `omnivoreAuthToken` and `omnivoreApiUrl` in the `appsettings.Development.json` project file `OpenAI.ApiService`.

# Testing it

You can use the `omnivore.http` file to test the API. You will need to update it to specify: 
1. userId - your omnivore user id, needs to be set two times
2. id - the id of the article you want to annotate
