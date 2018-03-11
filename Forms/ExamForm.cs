using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ISTQB_Foundation_Questions.Helpers;
using ISTQB_Foundation_Questions.Models;
using ISTQB_Foundation_Questions.Properties;
using Timer = System.Windows.Forms.Timer;

namespace ISTQB_Foundation_Questions.Forms
{
    public partial class ExamForm : Form
    {
        private readonly Form _parentForm;
        private readonly List<Question> questions;
        private readonly List<Label> labels;
        private Question question;
        private int questionIndex;
        private readonly List<RadioButton> answerRadioButtons = new List<RadioButton>();
        private Answer[] correctAnswers = new Answer[40];
        private Answer[] checkedAnswers = new Answer[40];
        private bool testIsOver;
        private Timer timer = new Timer();
        private DateTime endTime;


        public ExamForm(Form parentForm)
        {
            _parentForm = parentForm;
            InitializeComponent();
            labels = new List<Label>
            {
                label1, label2, label3, label4, label5, label6, label7, label8, label9, label10,
                label11, label12, label13, label14, label15, label16, label17, label18, label19, label20,
                label21, label22, label23, label24, label25, label26, label27, label28, label29, label30,
                label31, label32, label33, label34, label35, label36, label37, label38, label39, label40
            };
            pictureBox1.Visible = false;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            answersGroupBox.MinimumSize = new Size(400, 0);
            answersGroupBox.MaximumSize = new Size(800, 0);

            questions = SqlHelper.ReadRandomQuestions(40);

            label41.Hide();
            questionNumberLabel.Hide();
            answersGroupBox.Hide();
            questionLabelsGroupBox.Enabled = false;
            endExamButton.Enabled = false;
            NextButton.Enabled = false;
            resultLabel.Hide();
            questionLabel.Text = "Вам предлагается решить тест из 40 случайных вопросов.\r\n" +
                                 "Время экзамена - 60 минут.\r\n" +
                                 "Проходной балл - 65% или 26 правильных ответов\r\n\r\n" +
                                 "Переходить по вопросам можно по кнопке 'Дальше'\r\n" +
                                 "или кликая на номер в блоке 'Вопросы'";

        }

        private void startExamButton_Click(object sender, EventArgs e)
        {
            ((Button)sender).Hide();
            DisplayQuestion(0);
            label41.Show();
            questionNumberLabel.Show();
            answersGroupBox.Show();
            questionLabelsGroupBox.Enabled = true;
            endExamButton.Enabled = true;
            NextButton.Enabled = true;
            const int minutes = 60;
            timerLabel.Text = $@"Осталось {minutes} мин";
            endTime = DateTime.UtcNow.AddMinutes(minutes);
            timer.Interval = 10000;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var remainingTime = endTime - DateTime.UtcNow;
            if (remainingTime < TimeSpan.Zero)
            {
                timerLabel.Text = @"Время вышло!";
                timer.Enabled = false;
                EndExam();
            }
            else
            {
                timerLabel.Text = $@"Осталось {remainingTime.Minutes + 1} мин";
            }
        }

        private void DisplayQuestion(int index)
        {
            questionIndex = index;
            question = questions[index];
            var label = labels[index];
            label.Font = new Font("Calibri", 10, FontStyle.Bold);
            label.Text = $@">{label.Text.TrimEnd('<').TrimStart('>')}<";
            questionLabel.Text = question.GetText();
            answersGroupBox.Location = new Point(questionLabel.Location.X, 20 + questionLabel.Location.Y + questionLabel.Height);
            ShuffleAndPlaceAnswers();
            answersGroupBox.MinimumSize = new Size(Math.Max(400, questionLabel.Width), 0);
            Text = "Вопрос №" + question.Id;
            questionNumberLabel.Text = (index + 1).ToString();
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

        private void ExamForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parentForm.Show();
            _parentForm.Activate();
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
                    Location = new Point(0, 0),
                    Font = new Font("Calibri", 10)
                };
                radioButton.Click += (sender, args) => checkedAnswers[questionIndex] = answer;
                if (checkedAnswers[questionIndex] != null && checkedAnswers[questionIndex] == answer)
                {
                    radioButton.Checked = true;
                }
                answerRadioButtons.Add(radioButton);
                if (answer.IsCorrect)
                {
                    correctAnswers[questionIndex] = answer;
                    if (testIsOver)
                    {
                        radioButton.BackColor = Color.LimeGreen;
                    }
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

        private void NextButton_Click(object sender, EventArgs e)
        {
            GoToNextQuestion(questionIndex == 39 ? 0 : questionIndex + 1);
        }

        private void label_Click(object sender, EventArgs e)
        {
            var text = ((Label)sender).Text.TrimEnd('<').TrimStart('>');
            GoToNextQuestion(int.Parse(text) - 1);
        }

        private void GoToNextQuestion(int index)
        {
            var label = labels[questionIndex];
            label.ResetFont();
            label.Text = label.Text.TrimStart('>').TrimEnd('<');
            if (!testIsOver)
            {
                if (answerRadioButtons.All(button => button.Checked == false))
                {
                    label.BackColor = Color.Gold;
                }
                else
                {
                    if (label.BackColor != Color.LightSkyBlue)
                    {
                        label.BackColor = Color.LightSkyBlue;
                    }
                }
            }
            DisplayQuestion(index);
            if (checkedAnswers.All(answer => answer != null))
            {
                endExamButton.BackColor = Color.LimeGreen;
            }
        }

        private void EndExamButton_Click(object sender, EventArgs e)
        {
            if (checkedAnswers.Any(answer => answer == null) && MessageBox.Show
                (this,
                @"Вы ответили не на все вопросы. Все равно хотите завершить?",
                @"Завершение теста",
                MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            EndExam();
        }

        private void EndExam()
        {
            double rightAnswersCount = 0;
            for (var i = 0; i < 40; i++)
            {
                if (checkedAnswers[i] != null && checkedAnswers[i] == correctAnswers[i])
                {
                    labels[i].BackColor = Color.LimeGreen;
                    rightAnswersCount++;
                }
                else
                {
                    labels[i].BackColor = Color.Red;
                }
            }
            endExamButton.BackColor = SystemColors.Control;
            //endExamButton.Enabled = false;
            testIsOver = true;
            DisplayQuestion(questionIndex);
            timer.Stop();

            var IsSucces = rightAnswersCount >= 26;
            resultLabel.Text = IsSucces ? $"Успешно\r\n" : "Не успешно\r\n";
            resultLabel.Text+= $@"{Math.Round(rightAnswersCount/40*100)}% ({rightAnswersCount} из 40)";
            resultLabel.BackColor = IsSucces ? Color.LimeGreen : Color.Red;
            resultLabel.Show();
        }
    }
}
