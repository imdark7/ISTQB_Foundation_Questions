using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISTQB_Foundation_Questions.Models;

namespace ISTQB_Foundation_Questions.Helpers
{
    public static class DisplayDataHelper
    {

        public static string GetText(this Question question)
        {
            var newStrings = question.EnglishText.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var englishQuestion = new List<string>();
            foreach (var newString in newStrings)
            {
                englishQuestion.AddRange(GetStringsList(newString.Split(' '), 110));
            }
            if (question.RussianText != null)
            {
                var newRussianStrings = question.RussianText.Split(new[] { "\r\n" }, StringSplitOptions.None);
                var russianQuestion = new List<string>();
                foreach (var newString in newRussianStrings)
                {
                    russianQuestion.AddRange(GetStringsList(newString.Split(' '), 110));
                }
                int separatorLength;
                if (englishQuestion.Count > 1 || russianQuestion.Count > 1)
                {
                    separatorLength = 100;
                }
                else
                {
                    separatorLength = Math.Max(englishQuestion[0].Length, russianQuestion[0].Length);
                }
                var separator = "-";
                while (separator.Length < separatorLength)
                {
                    separator += "-";
                }
                englishQuestion.Add(separator);
                englishQuestion.AddRange(russianQuestion);
            }
            var result = "";
            for (var i = 0; i < englishQuestion.Count; i++)
            {
                result += englishQuestion[i];
                if (i != englishQuestion.Count - 1)
                {
                    result += "\r\n";
                }
            }
            return result;
        }

        public static string GetText(this Answer answer)
        {
            var englishAnswer = GetStringsList(answer.EnglishText.Split(' '), 100);
            if (answer.RussianText != null)
            {
                var russianAnswer = GetStringsList(answer.RussianText.Split(' '), 100);
                int separatorLength;
                if (englishAnswer.Count > 1 || russianAnswer.Count > 1)
                {
                    separatorLength = 100;
                }
                else
                {
                    separatorLength = Math.Max(englishAnswer[0].Length, russianAnswer[0].Length);
                }
                var separator = "-";
                while (separator.Length < separatorLength && separator.Length < 20)
                {
                    separator += "-";
                }
                englishAnswer.Add(separator);
                englishAnswer.AddRange(russianAnswer);
            }
            var result = "";
            foreach (var item in englishAnswer)
            {
                result += item;
                if (englishAnswer.Last() != item)
                {
                    result += "\n\r";
                }
            }
            return result;
        }



        private static List<string> GetStringsList(IEnumerable<string> words, int length)
        {
            var list = new List<string>();
            foreach (var word in words)
            {
                if (list.Count == 0)
                {
                    list.Add(word);
                    continue;
                }
                var lastElement = list.Last();
                if (lastElement.Length + 1 + word.Length < length)
                {
                    list[list.LastIndexOf(lastElement)] += " " + word;
                }
                else
                {
                    list.Add(word);
                }
            }
            return list;
        }

    }
}
