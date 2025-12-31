using InboxEngine.Api.Models;
using System;

namespace InboxEngine.Api.Services;

public interface IPriorityScoringService
{
    int CalculatePriorityScore(Email email, DateTime nowUtc);
}
