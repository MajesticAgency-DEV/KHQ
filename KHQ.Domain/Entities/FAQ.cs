namespace KHQ.Domain.Entities
{
    public class FAQ
    {
        public Guid Id { get; set; }
        public string QuestionEn { get; set; }
        public string QuestionAr { get; set; }
        public string AnswerEn { get; set; }
        public string AnswerAr { get; set; }
    }
}
