using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserTestingApplication.Models
{
    [Table(nameof(Question))]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        //[ForeignKey(nameof(TestId))]
        //public int TestId {  get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
