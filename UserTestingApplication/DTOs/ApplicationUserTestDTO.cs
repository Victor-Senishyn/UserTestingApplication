using System.ComponentModel.DataAnnotations.Schema;

namespace UserTestingApplication.DTOs
{
    public class ApplicationUserTestDTO
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public int Score { get; set; }
        public string ApplicationUserId { get; set; }
        public int TestId { get; set; }
    }
}
