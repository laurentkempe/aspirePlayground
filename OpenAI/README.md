# Omnivore, .NET Aspire, Azure OpenAI and Ollama

⚠️ Work in progress ⚠️

The original idea comes from the [omnivore](https://omnivore.app/) blog post, by [Jan Beck](https://github.com/jancbeck), [Using ChatGPT to automatically add annotations to your Omnivore saved articles](https://blog.omnivore.app/p/using-chatgpt-to-automatically-add). His idea was to use ChatGPT to automatically add annotations to your Omnivore saved articles.

I wanted to port the idea to .NET Aspire and see how it would work. And it worked very well 👍🏼 First implementation (OpenAI.AzureService) uses Azure OpenAI GPT-3.5-turbo-16k to generate the summary. Then, I added one implementation (OpenAI.OllamaService) using [Ollama](https://ollama.com/) to run the LLM locally [on my Windows machine](https://ollama.com/blog/windows-preview).

You can find Jan JavaScript implementation at [jancbeck/omnivore-ai-annotations](
https://github.com/jancbeck/omnivore-ai-annotations#readme).

# Azure OpenAI

## Setup and configuration for Azure OpenAI

You also need to create an [Azure OpenAI](https://learn.microsoft.com/en-us/azure/ai-services/openai/) gpt-35-turbo-16k deployment.

Configure `omnivoreAuthToken` and `omnivoreApiUrl` in the `appsettings.Development.json` project file `OpenAI.AzureService`.

Configure `openai` replacing `{account_name}` and `{account_key}` in the `appsettings.Development.json` of `OpenAI.AppHost` project.

## Testing it

You can use the web application to test.

Or, you can use the `omnivore.http` file to test the API. You will need to update it to specify: 
1. userId - your omnivore user id, needs to be set two times
2. id - the id of the article you want to annotate

# Ollama

## Setup and configuration for Ollama

Download the [Ollama for Windows](https://ollama.com/download/windows) adn run installer.

Let's pull the [phi-2 model](https://huggingface.co/microsoft/phi-2).

```bash
ollama pull phi
```

Then, run ollama with phi2 model.

```bash
ollama run phi
```

Ollama’s API automatically runs in the background, serving on http://localhost:11434, and you should see the following output:

> Ollama is running

## Testing it

You can use the web application to test.


# TODO

- [ ] Save the summary to the omnivore article
- [ ] Provide a way to deploy the service to Azure Container Apps usin azd
