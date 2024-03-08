namespace UserTestingApplication.Models
{
    public class UserAnswer
    {
        public int TestId { get; set; }
        public string UserId { get; set; }
        public ICollection<int> QuestionIds { get; set; }
        public ICollection<int> SelectedAnswerIds { get; set; }
    }
}
