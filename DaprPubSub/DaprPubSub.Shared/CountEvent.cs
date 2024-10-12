using System.Text.Json.Serialization;

namespace DaprPubSub.Shared;

public sealed record CountEvent([property: JsonPropertyName("counter")] int Counter);