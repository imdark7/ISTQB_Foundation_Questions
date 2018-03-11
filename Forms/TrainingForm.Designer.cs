namespace ISTQB_Foundation_Questions.Forms
{
    partial class TrainingForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.answersGroupBox = new System.Windows.Forms.GroupBox();
            this.CheckAnswerButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.questionLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ShareTranslateButton = new System.Windows.Forms.Button();
            this.PreviousQuestion = new System.Windows.Forms.Button();
            this.GoToQuestion = new System.Windows.Forms.Button();
            this.NextQuestion = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.QuestionNumber = new System.Windows.Forms.TextBox();
            this.strategyComboBox = new System.Windows.Forms.ComboBox();
            this.test = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // answersGroupBox
            // 
            this.answersGroupBox.AutoSize = true;
            this.answersGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.answersGroupBox.Location = new System.Drawing.Point(16, 186);
            this.answersGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.answersGroupBox.Name = "answersGroupBox";
            this.answersGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.answersGroupBox.Size = new System.Drawing.Size(8, 24);
            this.answersGroupBox.TabIndex = 2;
            this.answersGroupBox.TabStop = false;
            // 
            // CheckAnswerButton
            // 
            this.CheckAnswerButton.Location = new System.Drawing.Point(393, 388);
            this.CheckAnswerButton.Margin = new System.Windows.Forms.Padding(4);
            this.CheckAnswerButton.Name = "CheckAnswerButton";
            this.CheckAnswerButton.Size = new System.Drawing.Size(161, 42);
            this.CheckAnswerButton.TabIndex = 3;
            this.CheckAnswerButton.Text = "Проверить ответ";
            this.CheckAnswerButton.UseVisualStyleBackColor = true;
            this.CheckAnswerButton.Click += new System.EventHandler(this.CheckAnswerButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(224, 388);
            this.NextButton.Margin = new System.Windows.Forms.Padding(4);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(161, 42);
            this.NextButton.TabIndex = 3;
            this.NextButton.Text = "Дальше >>";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.questionLabel.Location = new System.Drawing.Point(16, 52);
            this.questionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(86, 17);
            this.questionLabel.TabIndex = 0;
            this.questionLabel.Text = "questionLabel";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(224, 106);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 62);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // ShareTranslateButton
            // 
            this.ShareTranslateButton.Location = new System.Drawing.Point(16, 388);
            this.ShareTranslateButton.Margin = new System.Windows.Forms.Padding(4);
            this.ShareTranslateButton.Name = "ShareTranslateButton";
            this.ShareTranslateButton.Size = new System.Drawing.Size(161, 42);
            this.ShareTranslateButton.TabIndex = 3;
            this.ShareTranslateButton.Text = "Поправить перевод";
            this.ShareTranslateButton.UseVisualStyleBackColor = true;
            this.ShareTranslateButton.Click += new System.EventHandler(this.ShareTranslateButton_Click);
            // 
            // PreviousQuestion
            // 
            this.PreviousQuestion.Location = new System.Drawing.Point(133, 12);
            this.PreviousQuestion.Name = "PreviousQuestion";
            this.PreviousQuestion.Size = new System.Drawing.Size(44, 25);
            this.PreviousQuestion.TabIndex = 5;
            this.PreviousQuestion.Text = "◀";
            this.PreviousQuestion.UseVisualStyleBackColor = true;
            this.PreviousQuestion.Click += new System.EventHandler(this.PreviousQuestion_Click);
            // 
            // GoToQuestion
            // 
            this.GoToQuestion.Location = new System.Drawing.Point(318, 12);
            this.GoToQuestion.Name = "GoToQuestion";
            this.GoToQuestion.Size = new System.Drawing.Size(29, 25);
            this.GoToQuestion.TabIndex = 5;
            this.GoToQuestion.Text = "✅";
            this.GoToQuestion.UseVisualStyleBackColor = true;
            this.GoToQuestion.Click += new System.EventHandler(this.GoToQuestion_Click);
            // 
            // NextQuestion
            // 
            this.NextQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.NextQuestion.Location = new System.Drawing.Point(353, 12);
            this.NextQuestion.Name = "NextQuestion";
            this.NextQuestion.Size = new System.Drawing.Size(45, 25);
            this.NextQuestion.TabIndex = 5;
            this.NextQuestion.Text = "▶";
            this.NextQuestion.UseVisualStyleBackColor = true;
            this.NextQuestion.Click += new System.EventHandler(this.NextQuestion_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(178, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Вопрос №";
            // 
            // QuestionNumber
            // 
            this.QuestionNumber.Location = new System.Drawing.Point(255, 13);
            this.QuestionNumber.MaxLength = 4;
            this.QuestionNumber.Name = "QuestionNumber";
            this.QuestionNumber.Size = new System.Drawing.Size(57, 23);
            this.QuestionNumber.TabIndex = 7;
            // 
            // strategyComboBox
            // 
            this.strategyComboBox.FormattingEnabled = true;
            this.strategyComboBox.Items.AddRange(new object[] {
            "Случайный",
            "По порядку"});
            this.strategyComboBox.Location = new System.Drawing.Point(12, 12);
            this.strategyComboBox.Name = "strategyComboBox";
            this.strategyComboBox.Size = new System.Drawing.Size(109, 24);
            this.strategyComboBox.TabIndex = 8;
            this.strategyComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // test
            // 
            this.test.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.test.Location = new System.Drawing.Point(404, 12);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(36, 25);
            this.test.TabIndex = 9;
            this.test.Text = "sync";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // TrainingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(579, 444);
            this.Controls.Add(this.test);
            this.Controls.Add(this.strategyComboBox);
            this.Controls.Add(this.QuestionNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NextQuestion);
            this.Controls.Add(this.GoToQuestion);
            this.Controls.Add(this.PreviousQuestion);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ShareTranslateButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.CheckAnswerButton);
            this.Controls.Add(this.answersGroupBox);
            this.Controls.Add(this.questionLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TrainingForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TrainingForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox answersGroupBox;
        private System.Windows.Forms.Button CheckAnswerButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ShareTranslateButton;
        private System.Windows.Forms.Button PreviousQuestion;
        private System.Windows.Forms.Button GoToQuestion;
        private System.Windows.Forms.Button NextQuestion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox QuestionNumber;
        private System.Windows.Forms.ComboBox strategyComboBox;
        private System.Windows.Forms.Button test;
    }
}