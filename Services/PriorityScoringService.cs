using InboxEngine.Models;
using Microsoft.OpenApi.Any;

namespace InboxEngine.Services;

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

    public int CalculatePriorityScore(Email email, DateTime nowUtc)
    {
        // Validate input
        if (email == null)
            throw new ArgumentNullException(nameof(email));
        // Initialize score
        int score = 0;

        // VIP
        if (email.IsVip)
        {
            // Add 50 points for VIP
            score += 50;
        }

       
        if (!string.IsNullOrWhiteSpace(email.Subject))
        {
            // Check for urgency keywords in subject
            // Case-insensitive
            // Convert subject to lower case for comparison
            // Check if any urgency keyword is present
            var subjectLower = email.Subject.ToLowerInvariant();
            if (UrgencyKeywords.Any(s => subjectLower.Contains(s)))
            {
                score += 30;
            }
        }

        // Time decay: older = more urgent
        // +1 point for every hour passed since ReceivedAt
      
        var diff = nowUtc - email.ReceivedAt;
       if(diff.TotalHours>0)
        {
            score += (int)diff.TotalHours;
        }

        // Spam filter
        // -20 points if body contains spam keywords
        if (!string.IsNullOrWhiteSpace(email.Body))
        {
            var bodyLower = email.Body.ToLowerInvariant();
            if (SpamKeywords.Any(b => bodyLower.Contains(b)) )
            {
                score -= 20;
            }
        }

        // Clamp
        if (score > 100) score = 100;
        if (score < 0) score = 0;

        return score;
    }
}

