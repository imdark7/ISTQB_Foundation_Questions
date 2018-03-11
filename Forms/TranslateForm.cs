using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using ISTQB_Foundation_Questions.Helpers;
using ISTQB_Foundation_Questions.Models;

namespace ISTQB_Foundation_Questions.Forms
{
    public partial class TranslateForm : Form
    {
        private readonly Question _question;
        private readonly TrainingForm _parentForm;
        private readonly List<KeyValuePair<TextBox, TextBox>> _answers = new List<KeyValuePair<TextBox, TextBox>>();
        readonly Label _tempLabel = new Label { Visible = false, Location = new Point(1, 1), AutoSize = true };
        public TranslateForm(Question question, TrainingForm parentForm)
        {
            _question = question;
            _parentForm = parentForm;
            InitializeComponent();
            Controls.Add(_tempLabel);

            QuestionLabel.Text = question.EnglishText;
            QuestionLabel.Location = new Point(10, 20);

            QuestionTranslateTextBox.Text = question.RussianText;
            QuestionTranslateTextBox.Location = new Point(QuestionLabel.Location.X, QuestionLabel.Location.Y + QuestionLabel.Height + 10);

            _answers.AddRange(new List<KeyValuePair<TextBox, TextBox>>
            {
                new KeyValuePair<TextBox, TextBox>(AnswerLabel1, AnswerTextBox1),
                new KeyValuePair<TextBox, TextBox>(AnswerLabel2, AnswerTextBox2),
                new KeyValuePair<TextBox, TextBox>(AnswerLabel3, AnswerTextBox3),
                new KeyValuePair<TextBox, TextBox>(AnswerLabel4, AnswerTextBox4),
                new KeyValuePair<TextBox, TextBox>(AnswerLabel5, AnswerTextBox5)
            });
            for (var i = 0; i < question.Answers.Count; i++)
            {
                var key = _answers[i].Key;
                var value = _answers[i].Value;
                key.Text = question.Answers[i].EnglishText;
                value.Text = question.Answers[i].RussianText;
                key.Visible = true;
                value.Visible = true;
                if (i == 0)
                {
                    key.Location = new Point(QuestionTranslateTextBox.Location.X,
                        QuestionTranslateTextBox.Location.Y + QuestionTranslateTextBox.Height + 30);
                }
                else
                {
                    var previousElement = _answers[i - 1].Value;
                    key.Location = new Point(previousElement.Location.X, previousElement.Location.Y + previousElement.Height + 20);
                }
                value.Location = new Point(key.Location.X, key.Location.Y + key.Height + 10);
                if (i == question.Answers.Count - 1)
                {
                    var last = _answers[i].Value;
                    SaveButton.Location = new Point(last.Location.X + 300, last.Location.Y + last.Height + 20);
                }
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            _tempLabel.Text = ((TextBox)sender).Text;
            var caretPosition = ((TextBox)sender).SelectionStart;
            ((TextBox)sender).Size = new Size(_tempLabel.Width + 5, _tempLabel.Height + 7);
            ((TextBox)sender).SelectionStart = 0;
            ((TextBox)sender).ScrollToCaret();
            ((TextBox)sender).SelectionStart = caretPosition;
            if (_answers.Count > 0)
            {
                RefreshForm();
            }
        }

        private void QuestionLabel_SizeChanged(object sender, EventArgs e)
        {
            QuestionTranslateTextBox.Location = new Point(QuestionLabel.Location.X, QuestionLabel.Location.Y + QuestionLabel.Height + 10);
        }

        private void RefreshForm()
        {
            for (var i = 0; i < _question.Answers.Count; i++)
            {
                var key = _answers[i].Key;
                var value = _answers[i].Value;
                if (i == 0)
                {
                    key.Location = new Point(QuestionTranslateTextBox.Location.X,
                        QuestionTranslateTextBox.Location.Y + QuestionTranslateTextBox.Height + 30);
                }
                else
                {
                    var previousElement = _answers[i - 1].Value;
                    key.Location = new Point(previousElement.Location.X, previousElement.Location.Y + previousElement.Height + 20);
                }
                value.Location = new Point(key.Location.X, key.Location.Y + key.Height + 10);
                if (i == _question.Answers.Count - 1)
                {
                    var last = _answers[i].Value;
                    SaveButton.Location = new Point(last.Location.X + 300, last.Location.Y + last.Height + 20);
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (IsTranslateChanged())
            {
                UpdateTranslate();
                SendDataToGoogleSpreadsheets(
                    _question.Id, QuestionTranslateTextBox.Text,
                    _question.Answers[0].Id, _answers[0].Value.Text,
                    _question.Answers[1].Id, _answers[1].Value.Text,
                    _question.Answers[2].Id, _answers[2].Value.Text,
                    _question.Answers.Count > 3 ? _question.Answers[3]?.Id : null,
                    _question.Answers.Count > 3 ? _answers[3].Value.Text : null,
                    _question.Answers.Count > 4 ? _question.Answers[4]?.Id : null,
                    _question.Answers.Count > 4 ? _answers[4].Value.Text : null
                );
            }
            Close();
        }

        private static void SendDataToGoogleSpreadsheets(
            long questionNumber,
            string questionTranslate,
            long AnswerNumber1,
            string AnswerTranslate1,
            long AnswerNumber2,
            string AnswerTranslate2,
            long AnswerNumber3,
            string AnswerTranslate3,
            long? AnswerNumber4 = null,
            string AnswerTranslate4 = null,
            long? AnswerNumber5 = null,
            string AnswerTranslate5 = null
            )
        {
            var client = new HttpClient();
            var requestString =
                "https://docs.google.com/forms/d/e/1FAIpQLScFvIQhp6NaWK8kH1f7oQUJFazXlhX60KhYCNUDQZABGEsW1A/formResponse?" +
                $"entry.180058012={questionNumber}" + "&" +
                $"entry.445293845={questionTranslate}" + "&" +
                $"entry.1563159789={AnswerNumber1}" + "&" +
                $"entry.862288868={AnswerTranslate1}" + "&" +
                $"entry.959152022={AnswerNumber2}" + "&" +
                $"entry.524487442={AnswerTranslate2}" + "&" +
                $"entry.967136510={AnswerNumber3}" + "&" +
                $"entry.249557633={AnswerTranslate3}";
            if (AnswerNumber4 != null && AnswerTranslate4 != null)
            {
                requestString += "&" +
                    $"entry.1531334054={AnswerNumber4}" + "&" +
                    $"entry.1364966582={AnswerTranslate4}";
            }
            if (AnswerNumber5 != null && AnswerTranslate5 != null)
            {
                requestString += "&" +
                    $"entry.51961822={AnswerNumber5}" + "&" +
                    $"entry.1313525750={AnswerTranslate5}";
            }
            client.GetAsync(requestString).GetAwaiter().GetResult();
        }

        private bool IsTranslateChanged()
        {
            var result = false;
            if (IsTranslateChanged(_question.RussianText, QuestionTranslateTextBox.Text))
            {
                result = _question.RussianText != QuestionTranslateTextBox.Text;
            }
            if (IsTranslateChanged(_question.Answers[0].RussianText, AnswerTextBox1.Text))
            {
                result = AnswerTextBox1.Text != _question.Answers[0].RussianText;
            }
            if (IsTranslateChanged(_question.Answers[1].RussianText, AnswerTextBox2.Text))
            {
                result = AnswerTextBox2.Text != _question.Answers[1].RussianText;
            }
            if (IsTranslateChanged(_question.Answers[2].RussianText, AnswerTextBox3.Text))
            {
                result = AnswerTextBox3.Text != _question.Answers[2].RussianText;
            }
            if (_question.Answers.Count > 3 && IsTranslateChanged(_question.Answers[3].RussianText, AnswerTextBox4.Text))
            {
                result = AnswerTextBox4.Text != _question.Answers[3].RussianText;
            }
            if (_question.Answers.Count > 4 && IsTranslateChanged(_question.Answers[4].RussianText, AnswerTextBox5.Text))
            {
                result = AnswerTextBox5.Text != _question.Answers[4].RussianText;
            }
            return result;
        }

        private bool IsTranslateChanged(string a, string b)
        {
            if (!string.IsNullOrWhiteSpace(a) || !string.IsNullOrWhiteSpace(b))
            {
                return a != b;
            }
            return false;
        }

        private void UpdateTranslate()
        {
            if (IsTranslateChanged(_question.RussianText, QuestionTranslateTextBox.Text))
            {
                SqlHelper.UpdateQuestionTranslate(_question.Id, QuestionTranslateTextBox.Text);
            }
            if (IsTranslateChanged(_question.Answers[0].RussianText, AnswerTextBox1.Text))
            {
                SqlHelper.UpdateAnswerTranslate(_question.Answers[0].Id, AnswerTextBox1.Text);
            }
            if (IsTranslateChanged(_question.Answers[1].RussianText, AnswerTextBox2.Text))
            {
                SqlHelper.UpdateAnswerTranslate(_question.Answers[1].Id, AnswerTextBox2.Text);
            }
            if (IsTranslateChanged(_question.Answers[2].RussianText, AnswerTextBox3.Text))
            {
                SqlHelper.UpdateAnswerTranslate(_question.Answers[2].Id, AnswerTextBox3.Text);
            }
            if (_question.Answers.Count > 3 && IsTranslateChanged(_question.Answers[3].RussianText, AnswerTextBox4.Text))
            {
                SqlHelper.UpdateAnswerTranslate(_question.Answers[3].Id, AnswerTextBox4.Text);
            }
            if (_question.Answers.Count > 4 && IsTranslateChanged(_question.Answers[4].RussianText, AnswerTextBox5.Text))
            {
                SqlHelper.UpdateAnswerTranslate(_question.Answers[4].Id, AnswerTextBox5.Text);
            }
        }

        private void RefreshMainForm(object sender, FormClosingEventArgs e)
        {
            _parentForm.RefreshData();
        }
    }
}
