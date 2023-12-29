using System.Text.Json.Serialization;

namespace DaprOutbox.Shared;

public sealed record CountEvent([property: JsonPropertyName("counter")] int Counter);