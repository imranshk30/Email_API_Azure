using InboxEngine.Models;

namespace InboxEngine.Api.Models
{
    public class EmailResponse : Email
    {
        public int PriorityScore { get; set; }
    }
}
