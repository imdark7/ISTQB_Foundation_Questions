using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using ISTQB_Foundation_Questions.Properties;

namespace ISTQB_Foundation_Questions
{
    public static class SqlHelper
    {
        private static SQLiteConnection NewSqLiteConnection()
        {
            var stringBuilder = new SQLiteConnectionStringBuilder
            {
                DataSource = "database.sqlite3"
            };
            var connection = new SQLiteConnection(stringBuilder.ConnectionString);
            if (!File.Exists($"./{stringBuilder.DataSource}"))
            {
                SQLiteConnection.CreateFile(stringBuilder.DataSource);
                connection.CreateDb();
            }
            return connection;
        }

        private static void CreateDb(this SQLiteConnection connection)
        {
            try
            {
                connection.Open();
                var command = new SQLiteCommand(Resources.createDb, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                throw;
            }
        }

        public static List<Question> ReadQuestions(string requestCondition = null)
        {
            var sqlConnection = NewSqLiteConnection();
            sqlConnection.Open();
            var command = requestCondition == null
                ? new SQLiteCommand("SELECT * FROM [Questions]", sqlConnection)
                : new SQLiteCommand($"SELECT * FROM [Questions] WHERE {requestCondition}", sqlConnection);
            var list = new List<Question>();
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var question = new Question
                    {
                        Id = reader.GetInt64(0),
                        EnglishText = reader.GetString(1),
                        RussianText = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Resource = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Theme = reader.IsDBNull(4) ? null : reader.GetString(4)
                    };
                    question.Answers = ReadAnswers(question.Id);
                    list.Add(question);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return list;
        }

        public static List<Answer> ReadAnswers(long questionId)
        {
            var sqlConnection = NewSqLiteConnection();
            sqlConnection.Open();
            var command = new SQLiteCommand($"SELECT * FROM [Answers] WHERE QuestionId = '{questionId}'", sqlConnection);
            var list = new List<Answer>();
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Answer
                    {
                        Id = reader.GetInt64(0),
                        QuestionId = reader.GetInt64(1),
                        EnglishText = reader.GetString(2),
                        RussianText = reader.IsDBNull(3) ? null : reader.GetString(3),
                        IsCorrect = reader.GetBoolean(4)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return list;
        }

        public static void UpdateQuestionTranslate(long questionId, string russianText)
        {
            var sqlConnection = NewSqLiteConnection();
            sqlConnection.Open();
            var command =
                new SQLiteCommand(
                    $"UPDATE [Questions] SET [RussianText] = '{russianText ?? "NULL"}' WHERE [Id] = '{questionId}'", sqlConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public static void UpdateAnswerTranslate(long answerId, string russianText)
        {
            var sqlConnection = NewSqLiteConnection();
            sqlConnection.Open();
            var russianString = string.IsNullOrWhiteSpace(russianText) ? "NULL" : $"'{russianText}'";
            var command =
                new SQLiteCommand(
                    $"UPDATE [Answers] SET [RussianText] = {russianString} WHERE [Id] = '{answerId}'", sqlConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public static long? InsertQuestion(Question question)
        {
            var sqlConnection = NewSqLiteConnection();
            sqlConnection.Open();
            var command =
                new SQLiteCommand($"INSERT INTO [TempQuestions] ([Id], [EnglishText]) VALUES ('{question.Id}', '{question.EnglishText}')",
                    sqlConnection);
            long? id = null;
            try
            {
                command.ExecuteNonQuery();
                id = sqlConnection.LastInsertRowId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return id;
        }

        public static void InsertAnswer(Answer answer)
        {
            var sqlConnection = NewSqLiteConnection();
            sqlConnection.Open();
            var command =
                new SQLiteCommand($"INSERT INTO [TempAnswers] ([EnglishText], [QuestionId], [IsCorrect]) VALUES ('{answer.EnglishText}', '{answer.QuestionId}', '{Convert.ToInt32(answer.IsCorrect)}')",
                    sqlConnection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
