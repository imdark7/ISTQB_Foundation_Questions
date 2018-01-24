using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ISTQB_Foundation_Questions.Properties;

namespace ISTQB_Foundation_Questions
{
    public partial class MainForm : Form
    {
        private readonly List<Question> questions;
        private Question question;
        private readonly List<RadioButton> answerRadioButtons = new List<RadioButton>();
        private RadioButton CorrectAnswer;
        private readonly Random rnd = new Random();
        private bool IsRandomQuestionStrategy;
        private int questionIndex;

        public MainForm()
        {
            InitializeComponent();
            NextButton.Visible = false;
            pictureBox1.Visible = false;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            answersGroupBox.MinimumSize = new Size(400, 0);
            answersGroupBox.MaximumSize = new Size(800, 0);
            strategyComboBox.SelectedIndex = 0;

            questions = SqlHelper.ReadQuestions();
            GetNewQuestion();
        }

        private void GetNewQuestion()
        {
            try
            {
                SetNextIndex();
                question = questions[questionIndex];
                questionLabel.Text = GetText(question);
                answersGroupBox.Location = new Point(questionLabel.Location.X, 20 + questionLabel.Location.Y + questionLabel.Height);

                ShuffleAndPlaceAnswers();
                answersGroupBox.MinimumSize = new Size(Math.Max(400, questionLabel.Width), 0);
                Text = "Вопрос №" + question.Id;
                QuestionNumber.Text = question.Id.ToString();

                if (question.Resource != null)
                {
                    var rm = new ComponentResourceManager(typeof(Resources));
                    pictureBox1.Image = (Bitmap)rm.GetObject(question.Resource);
                    pictureBox1.Size = pictureBox1.Image.Size;
                    pictureBox1.Width += 10;
                    pictureBox1.Location = new Point(Math.Max(answersGroupBox.Width, questionLabel.Width) + answersGroupBox.Location.X + 20, 20);
                    pictureBox1.Visible = true;
                }
                else
                {
                    pictureBox1.Visible = false;
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e);
                Controls.Clear();
                Controls.Add(new PictureBox
                {
                    Image = Resources.goodJob,
                    Size = Resources.goodJob.Size
                });
                Text = @"Вопросы кончились";
            }

            CheckAnswerButton.Location = new Point
                (
                    answersGroupBox.Location.X + answersGroupBox.Width - CheckAnswerButton.Width,
                    answersGroupBox.Location.Y + 10 + answersGroupBox.Height
                );
            NextButton.Location = CheckAnswerButton.Location;
            ShareTranslateButton.Location = new Point
                (
                    answersGroupBox.Location.X,
                    answersGroupBox.Location.Y + 10 + answersGroupBox.Height
                );
        }

        private void SetNextIndex()
        {
            if (IsRandomQuestionStrategy)
            {
                questionIndex = rnd.Next(0, questions.Count);
            }
            else
            {
                if (questionIndex == questions.Count - 1)
                {
                    questionIndex = 0;
                }
                else
                {
                    if (question != null)
                    {
                        questionIndex = questions.FindIndex(q => q.Id == question.Id) + 1;
                    }
                    else
                    {
                        questionIndex = 0;
                    }
                }
            }
        }

        private void ShuffleAndPlaceAnswers()
        {
            answersGroupBox.Controls.Clear();
            answerRadioButtons.Clear();
            foreach (var answer in question.Answers)
            {
                var radioButton = new RadioButton
                {
                    Text = GetText(answer),
                    AutoSize = true,
                    Parent = answersGroupBox,
                    Location = new Point(0, 0)
                };
                answerRadioButtons.Add(radioButton);
                if (answer.IsCorrect)
                {
                    CorrectAnswer = radioButton;
                }
            }

            var indexes = new List<int>();
            for (var i = 0; i < answersGroupBox.Controls.Count; i++)
            {
                indexes.Add(i);
            }
            IEnumerable<int> shuffled = indexes.OrderBy(i => Guid.NewGuid());

            Control control = null;
            foreach (var i in shuffled)
            {
                answersGroupBox.Controls[i].Location = control == null ?
                    new Point(20, 20) :
                    new Point(control.Left, control.Top + control.Height + 20);
                control = answersGroupBox.Controls[i];
            }
        }

        private string GetText(Answer answer)
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

        private string GetText(Question question)
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

        private void CheckAnswerButton_Click(object sender, EventArgs e)
        {
            CorrectAnswer.BackColor = Color.LimeGreen;
            if (!CorrectAnswer.Checked && answerRadioButtons.Exists(rb => rb.Checked))
            {
                answerRadioButtons.First(rb => rb.Checked).BackColor = Color.Crimson;
            }
            CheckAnswerButton.Visible = false;
            NextButton.Visible = true;
            if (CorrectAnswer.Checked)
            {
                questions.Remove(question);
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            for (var i = answersGroupBox.Controls.Count - 1; i >= 0; i--)
            {
                answersGroupBox.Controls.RemoveAt(i);
            }
            NextButton.Visible = false;
            CheckAnswerButton.Visible = true;
            GetNewQuestion();
            RefreshData();
        }

        private void ShareTranslateButton_Click(object sender, EventArgs e)
        {
            new TranslateForm(question, this).Show();
        }

        public void RefreshData( )
        {
            question = SqlHelper.ReadQuestions($"[Id] = '{question.Id}'").First();

            questionLabel.Text = GetText(question);
            answersGroupBox.Location = new Point(questionLabel.Location.X, 20 + questionLabel.Location.Y + questionLabel.Height);

            ShuffleAndPlaceAnswers();
            answersGroupBox.MinimumSize = new Size(Math.Max(400, questionLabel.Width), 0);

            Text = "Вопрос №" + question.Id;
            QuestionNumber.Text = question.Id.ToString();

            if (question.Resource != null)
            {
                var rm = new ComponentResourceManager(typeof(Resources));
                pictureBox1.Image = (Bitmap)rm.GetObject(question.Resource);
                pictureBox1.Size = pictureBox1.Image.Size;
                pictureBox1.Width += 10;
                pictureBox1.Location = new Point(Math.Max(answersGroupBox.Width, questionLabel.Width) + answersGroupBox.Location.X + 20, 20);
                pictureBox1.Visible = true;
            }
            else
            {
                pictureBox1.Visible = false;
            }
            CheckAnswerButton.Location = new Point
                (
                    answersGroupBox.Location.X + answersGroupBox.Width - CheckAnswerButton.Width,
                    answersGroupBox.Location.Y + 10 + answersGroupBox.Height
                );
            NextButton.Location = CheckAnswerButton.Location;
            NextButton.Visible = false;
            CheckAnswerButton.Visible = true;
            ShareTranslateButton.Location = new Point
                (
                    answersGroupBox.Location.X,
                    answersGroupBox.Location.Y + 10 + answersGroupBox.Height
                );
        }

        private void PreviousQuestion_Click(object sender, EventArgs e)
        {
            var idx = questions.FindIndex(q => q.Id == question.Id);
            
            question = idx == 0 ? questions.Last() : questions.Last(q => q.Id < question.Id);
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsRandomQuestionStrategy = ((ComboBox) sender).SelectedIndex == 0;
            if (((ComboBox) sender).SelectedIndex == 0)
            {
                IsRandomQuestionStrategy = true;
                PreviousQuestion.Enabled = false;
                NextQuestion.Enabled = false;
            }
            else
            {
                IsRandomQuestionStrategy = false;
                PreviousQuestion.Enabled = true;
                NextQuestion.Enabled = true;
            }
        }

        private void NextQuestion_Click(object sender, EventArgs e)
        {
            var idx = questions.FindIndex(q => q.Id == question.Id);

            if (idx == questions.Count - 1)
            {
                question = questions.First();
            }
            else
            {
                question = questions.First(q => q.Id > question.Id);
            }
            RefreshData();
        }

        private void GoToQuestion_Click(object sender, EventArgs e)
        {
            long parsedId;
            long.TryParse(QuestionNumber.Text, out parsedId);
            if (long.TryParse(QuestionNumber.Text, out parsedId) && questions.Any(q => q.Id == parsedId))
            {
                question = questions.First(q => q.Id == parsedId);
                RefreshData();
            }
        }

        private void test_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            SpreadSheetsHelper.UpdateTranslateData();
            this.Enabled = true;
        }
    }
}
