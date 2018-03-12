using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ISTQB_Foundation_Questions.Helpers;
using ISTQB_Foundation_Questions.Models;
using ISTQB_Foundation_Questions.Properties;

namespace ISTQB_Foundation_Questions.Forms
{
    public partial class TrainingForm : Form
    {
        private readonly Form _parentForm;
        private readonly List<Question> questions;
        private Question question;
        private readonly List<RadioButton> answerRadioButtons = new List<RadioButton>();
        private RadioButton CorrectAnswer;
        private readonly Random rnd = new Random();
        private bool IsRandomQuestionStrategy;
        private int questionIndex;

        public TrainingForm(Form parentForm)
        {
            _parentForm = parentForm;
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
                questionLabel.Text = question.GetText();
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
                    Text = answer.GetText(),
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

        public void RefreshData()
        {
            question = SqlHelper.ReadQuestions($"[Id] = '{question.Id}'").First();

            questionLabel.Text = question.GetText();
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
            else if (((ComboBox) sender).SelectedIndex == 1)
            {
                IsRandomQuestionStrategy = false;
                PreviousQuestion.Enabled = true;
                NextQuestion.Enabled = true;
            }
            else
            {
                
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
            Enabled = false;
            SpreadSheetsHelper.UpdateTranslateData();
            RefreshData();
            Enabled = true;
        }

        private void TrainingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parentForm.Show();
            _parentForm.Activate();
        }
    }
}
