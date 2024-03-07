using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserTestingApplication.Models
{
    [Table(nameof(Answer))]
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        //[ForeignKey(nameof(QuestionId))]
        //public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
