using Microsoft.AspNetCore.Mvc;
using InboxEngine.Api.Models;
using InboxEngine.Api.Services;

using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;

namespace InboxEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InboxController : ControllerBase
{
    private readonly IPriorityScoringService _scoringService;
    private readonly ILogger<InboxController> _logger;

    public InboxController(IPriorityScoringService scoringService, ILogger<InboxController> logger)
    {
        _scoringService = scoringService;
        _logger = logger;
    }
    
    [HttpPost("sort")]
    public IActionResult SortEmails([FromBody] List<Email> emails)
    {
        _logger.LogInformation("Received {count} emails for sorting", emails.Count);
        // TODO: Implement the endpoint logic:
        // 1. Validate the input (check for null or empty list)
        // 2. Calculate priority score for each email using _scoringService
        // 3. Sort emails by PriorityScore (highest first)
        // 4. Return the sorted list
  
   if(emails == null || emails.Count ==0)
        {
            return BadRequest("Email list cannot be null or empty.");
        }
        var nowUtc = DateTime.UtcNow;
        var scoredemails = emails.Select(e => new EmailResponse
        {
            Sender = e.Sender,
            Subject = e.Subject,
            Body = e.Body,
            ReceivedAt = e.ReceivedAt,
            IsVip = e.IsVip,
            PriorityScore = _scoringService.CalculatePriorityScore(e, nowUtc)
        }).OrderByDescending(e => e.PriorityScore).ToList();
        _logger.LogInformation("Returning {Count} sorted emails", emails.Count);
        return Ok(scoredemails);

        //////////testing purpose//////////
        ///////////make sure ci/cd works//////
        throw new NotImplementedException("Sort endpoint not yet implemented");
    }
}
