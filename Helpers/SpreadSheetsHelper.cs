using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ISTQB_Foundation_Questions.Properties;

namespace ISTQB_Foundation_Questions.Helpers
{
    public static class SpreadSheetsHelper
    {

        private static SpreadsheetsResource.ValuesResource.GetRequest GetData()
        {
            string[] scopes = { SheetsService.Scope.Spreadsheets };
            var aplicationName = "istqb data translate";

            var credential = GoogleCredential.FromJson(Resources.credentials).CreateScoped(scopes);
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = aplicationName
            });
            var spreadsheetId = "1AaMpnux39BVqEAdjqGJ2bfRQowTmBvEvtR3LmgGUvoA";
            return service.Spreadsheets.Values.Get(spreadsheetId, "A2:M1060");
        }
        public static void UpdateTranslateData()
        {
            var response = GetData().ExecuteAsync();
            var questionsTask = Task.Factory.StartNew(() => SqlHelper.ReadQuestions());
            var sortedList = new List<IList<object>>();

            foreach (var row in response.GetAwaiter().GetResult().Values)
            {
                var sortedListRow = sortedList.Find(list => list[1].ToString() == row[1].ToString());
                if (sortedListRow == null)
                {
                    sortedList.Add(row);
                }
                else
                {
                    if (DateTime.Parse(sortedListRow[0].ToString()).CompareTo(DateTime.Parse(row[0].ToString())) < 0)
                    {
                        var index = sortedList.IndexOf(sortedListRow);
                        sortedList.RemoveAt(index);
                        sortedList.Insert(index, row);
                    }
                }
            }
            foreach (var sortedRow in sortedList)
            {
                var question = questionsTask.GetAwaiter().GetResult().Find(q => q.Id.ToString().Equals(sortedRow[1].ToString()));
                if (question != null)
                {
                    if (!string.IsNullOrEmpty(sortedRow[2].ToString()))
                    {
                        SqlHelper.UpdateQuestionTranslate(question.Id, sortedRow[2].ToString().Replace("\n", "\r\n"));
                    }

                    var answer1 = question.Answers.Find(answer => answer.Id.ToString().Equals(sortedRow[3].ToString()));
                    SqlHelper.UpdateAnswerTranslate(answer1.Id, sortedRow[4].ToString().Replace("\n", "\r\n"));

                    var answer2 = question.Answers.Find(answer => answer.Id.ToString().Equals(sortedRow[5].ToString()));
                    SqlHelper.UpdateAnswerTranslate(answer2.Id, sortedRow[6].ToString().Replace("\n", "\r\n"));

                    var answer3 = question.Answers.Find(answer => answer.Id.ToString().Equals(sortedRow[7].ToString()));
                    SqlHelper.UpdateAnswerTranslate(answer3.Id, sortedRow.Count > 8 ? sortedRow[8].ToString().Replace("\n", "\r\n") : "");

                    if (sortedRow.Count > 9)
                    {
                        var answer4 =
                            question.Answers.Find(answer => answer.Id.ToString().Equals(sortedRow[9].ToString()));
                        SqlHelper.UpdateAnswerTranslate(answer4.Id, sortedRow.Count > 10 ? sortedRow[10].ToString().Replace("\n", "\r\n") : "");
                    }

                    if (sortedRow.Count > 11)
                    {
                        var answer5 =
                            question.Answers.Find(answer => answer.Id.ToString().Equals(sortedRow[11].ToString()));
                        SqlHelper.UpdateAnswerTranslate(answer5.Id, sortedRow.Count > 12 ? sortedRow[12].ToString().Replace("\n", "\r\n") : "");
                    }
                }
            }
        }
    }
}