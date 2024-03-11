using System.ComponentModel.DataAnnotations.Schema;

namespace UserTestingApplication.Repositories.Filters
{
    public class UserTestResultFilter
    {
        public int? Id { get; set; }
        public string? ApplicationUserId { get; set; }
        public int? TestId { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
