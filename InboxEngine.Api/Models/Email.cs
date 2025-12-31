using System;
using System.Text.Json.Serialization;

namespace InboxEngine.Api.Models;

public class Email
{
    [JsonPropertyName("sender")]
    public string Sender { get; set; } = string.Empty;

    [JsonPropertyName("subject")]
    public string Subject { get; set; }= string.Empty;

    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;

    [JsonPropertyName("receivedAt")]
    public DateTime ReceivedAt { get; set; }

    [JsonPropertyName("isVIP")]
    public bool IsVip { get; set; }


    //public int PriorityScore { get; set; }
}
