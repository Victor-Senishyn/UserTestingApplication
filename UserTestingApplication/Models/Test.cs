using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserTestingApplication.Models
{
    [Table(nameof(Test))]
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Question> Questions { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public string ApplicationUserId { get; set; }
        public bool IsCompleted { get; set; }
        public int? Score { get; set; }
    }
}
