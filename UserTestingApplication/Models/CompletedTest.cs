using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserTestingApplication.Models
{
    [Table(nameof(CompletedTest))]
    public class CompletedTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(TestId))]
        public int TestId { get; set; }
        public int Score { get; set; }
    }
}
