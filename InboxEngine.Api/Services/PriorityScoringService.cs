using InboxEngine.Api.Models;
using Microsoft.OpenApi.Any;
using System;
using System.Linq;

namespace InboxEngine.Api.Services;

/// <summary>
/// TODO: Implement the priority scoring logic according to the requirements:
/// - VIP Status: +50 points if isVip is true
/// - Urgency Keywords: +30 points if Subject contains "Urgent", "ASAP", or "Error" (case-insensitive)
/// - Time Decay: +1 point for every hour passed since ReceivedAt
/// - Spam Filter: -20 points if Body contains "Unsubscribe" or "Newsletter"
/// - Clamping: Final score must be between 0 and 100 (inclusive)
/// </summary>
public class PriorityScoringService : IPriorityScoringService
{

    // TODO: Implement the scoring logic here
    // Start with a base score of 0
    // Apply all the rules from the requirements
    // Return the clamped score (0-100)

    private static readonly string[] UrgencyKeywords = { "urgent", "asap", "error" };
    private static readonly string[] SpamKeywords = { "unsubscribe", "newsletter" };

    // Ensure the method signature exactly matches the interface definition
    public int CalculatePriorityScore(Email email, DateTime nowUtc)
    {
        if (email == null)
            throw new ArgumentNullException(nameof(email));
        int score = 0;

        if (email.IsVip)
        {
            score += 50;
        }

        if (!string.IsNullOrWhiteSpace(email.Subject))
        {
            var subjectLower = email.Subject.ToLowerInvariant();
            if (UrgencyKeywords.Any(s => subjectLower.Contains(s)))
            {
                score += 30;
            }
        }

        var diff = nowUtc - email.ReceivedAt;
        if (diff.TotalHours > 0)
        {
            score += (int)diff.TotalHours;
        }

        if (!string.IsNullOrWhiteSpace(email.Body))
        {
            var bodyLower = email.Body.ToLowerInvariant();
            if (SpamKeywords.Any(b => bodyLower.Contains(b)))
            {
                score -= 20;
            }
        }

        if (score > 100) score = 100;
        if (score < 0) score = 0;

        return score;
    }
}

