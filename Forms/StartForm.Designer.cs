namespace ISTQB_Foundation_Questions.Forms
{
    partial class StartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startTraining = new System.Windows.Forms.Button();
            this.startExam = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startTraining
            // 
            this.startTraining.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.startTraining.Location = new System.Drawing.Point(26, 27);
            this.startTraining.Name = "startTraining";
            this.startTraining.Size = new System.Drawing.Size(224, 68);
            this.startTraining.TabIndex = 0;
            this.startTraining.Text = "Тренировка";
            this.startTraining.UseVisualStyleBackColor = true;
            this.startTraining.Click += new System.EventHandler(this.startTraining_Click);
            // 
            // startExam
            // 
            this.startExam.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.startExam.Location = new System.Drawing.Point(26, 118);
            this.startExam.Name = "startExam";
            this.startExam.Size = new System.Drawing.Size(224, 68);
            this.startExam.TabIndex = 1;
            this.startExam.Text = "Экзамен";
            this.startExam.UseVisualStyleBackColor = true;
            this.startExam.Click += new System.EventHandler(this.startExam_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 212);
            this.Controls.Add(this.startExam);
            this.Controls.Add(this.startTraining);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startTraining;
        private System.Windows.Forms.Button startExam;
    }
}