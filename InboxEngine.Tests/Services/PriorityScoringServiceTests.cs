using Xunit;
using InboxEngine.Api.Models;
using InboxEngine.Api.Services;

namespace InboxEngine.Tests.Services;

public class PriorityScoringServiceTests
{
    private readonly IPriorityScoringService _service;
    private readonly DateTime _fixedNowUtc ;

    public PriorityScoringServiceTests()
    {
        _service = new PriorityScoringService();
        _fixedNowUtc = DateTime.UtcNow;
    }

    [Fact]
    public void TestSetup_ShouldPass()
    {
        // This is a dummy test to verify the test infrastructure is working.
        // If this test fails after you add your own tests, you know it's something
        // in your code that broke the build.
        Xunit.Assert.True(true);
    }
    [Fact]
    public void CalculatePriorityScore_VIPEmail_whenEmailIsNull()
    {
       Xunit.Assert.Throws<ArgumentNullException>(() => _service.CalculatePriorityScore(null, _fixedNowUtc)); 
    }

    [Fact]
    public void CalculatePriorityScore_VIPEmail_ShouldReturnHighScore()
    {
        // Arrange
        var email = new Email { IsVip = true, Subject = "Very Important Meeting", Body = " Please attend the meeting at 11 am"};
        // Act
        var score = _service.CalculatePriorityScore(email, _fixedNowUtc);
        // Assert
        Xunit.Assert.True(score > 50, "VIP emails should have a high priority score.");
    }

    [Fact]
    public void CalculatePriorityScore_NonVIPEmail_ShouldReturnLowScore()
    {
        // Arrange
        var email = new Email 
        {
            IsVip = false,
            Subject = "Newsletter",
            Body = "This is the monthly newsletter.",
            ReceivedAt = _fixedNowUtc.AddHours(-1)
        };
        // Act
        var score = _service.CalculatePriorityScore(email, _fixedNowUtc);
        // Assert
        Xunit.Assert.True(score < 50);// "Non-VIP emails should have a low priority score.");
    }

    [Fact]
    public void CalculatePriorityScore_EmptyEmail_ShouldReturnZeroScore()
    {
        // Arrange
        var email = new Email
        {
            IsVip = false,
            Subject = string.Empty,
            Body = string.Empty,
            ReceivedAt = _fixedNowUtc
        };
        // Act
        var score = _service.CalculatePriorityScore(email, _fixedNowUtc);
        // Assert
        Xunit.Assert.Equal(0, score);
    }

    [Fact]
    public void CalculatePriorityScore_UrgentEmail_ShouldReturnHighScore()
    {
        // Arrange
        var email = new Email
        {
            IsVip = false,
            Subject = "Urgent: Server Down",
            Body = "The main server is down. Please fix it ASAP."
        };
        // Act
        var score = _service.CalculatePriorityScore(email, _fixedNowUtc);
        // Assert
        Xunit.Assert.True(score >= 30, "Urgent emails should have a high priority score.");
    }

    [Fact]
    public void CalculatepriorityScore_ShouldClampScoreBetween0And100()
    {
        // Arrange
        var email = new Email
        {
            IsVip = true,
            Subject = "Urgent: Critical Error",
            Body = "This is a critical error. Unsubscribe if you want."
        };
        // Act
        var score = _service.CalculatePriorityScore(email, _fixedNowUtc.AddHours(100));
        // Assert
        Xunit.Assert.InRange(score, 0, 100);
    }
}

