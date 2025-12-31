using System.Text.Json.Serialization;

namespace InboxEngine.Models;

public class Email
{
    [JsonPropertyName("sender")]
    public string? Sender { get; set; }

    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("receivedAt")]
    public DateTime ReceivedAt { get; set; }

    [JsonPropertyName("isVIP")]
    public bool IsVip { get; set; }


    //public int PriorityScore { get; set; }
}
