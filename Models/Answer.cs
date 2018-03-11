namespace ISTQB_Foundation_Questions.Models
{
    public class Answer
    {
        public long Id;
        public long QuestionId;
        public string EnglishText;
        public string RussianText;
        public bool IsCorrect;
    }
}