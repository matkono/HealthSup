using System.Collections.Generic;

namespace HealthSup.Domain.Entities
{
    public class Question
    {
        public Question(
            int id,
            int code,
            string title,
            QuestionType questionType,
            bool isInitial,
            List<PossibleAnswer> possibleAnswers
        )
        {
            Id = id;
            Code = code;
            Title = title;
            QuestionType = questionType;
            IsInitial = isInitial;
            PossibleAnswers = possibleAnswers;
        }

        public Question() { }

        public int Id { get; private set; }

        public int Code { get; private set; }

        public string Title { get; private set; }

        public QuestionType QuestionType { get; private set; }

        public bool IsInitial { get; private set; }

        public List<PossibleAnswer> PossibleAnswers { get; private set; } = new List<PossibleAnswer>();

        public void SetQuestionType
        (
            QuestionType questionType
        )
        {
            QuestionType = questionType;
        }
    }
}
