using System;
using System.Drawing;
using System.Windows.Forms;

namespace ISTQB_Foundation_Questions.Forms
{
    public partial class StartForm : Form
    {
        private readonly Color _buttonBackColor;
        public StartForm()
        {
            InitializeComponent();
            _buttonBackColor = startExam.BackColor;
        }

        private void startTraining_Click(object sender, EventArgs e)
        {
            var button = ((Button) sender);
            var text = button.Text;
            button.Text = @"Открываю ...";
            button.BackColor = Color.FromArgb(58, 180, 115);
            Enabled = false;
            var form = new TrainingForm(this);
            Hide();
            form.Show();
            form.Activate();
            button.Text = text;
            button.BackColor = _buttonBackColor;
            Enabled = true;
        }

        private void startExam_Click(object sender, EventArgs e)
        {
            var button = ((Button)sender);
            var text = button.Text;
            button.Text = @"Открываю ...";
            button.BackColor = Color.FromArgb(58, 180, 115);
            Enabled = false;
            var form = new ExamForm(this);
            Hide();
            form.Show();
            form.Activate();
            button.Text = text;
            button.BackColor = _buttonBackColor;
            Enabled = true;
        }
    }
}
