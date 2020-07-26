using System;
using System.Windows.Forms;
using Winform_Example.Classes;

namespace Winform_Example
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            if (Guard.Register(username.Text, password.Text, email.Text, license.Text))
            {
                //Put code here of what you want to do after successful login
                MessageBox.Show("Register has been successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login loginfrm = new Login();
            loginfrm.Show();
        }
    }
}
