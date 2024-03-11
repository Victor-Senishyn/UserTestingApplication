namespace UserTestingApplication.DTOs
{
    public class TestDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<QuestionDTO> Questions { get; set; }
    }
}
