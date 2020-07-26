using System;
using System.Windows.Forms;
using Winform_Example.Classes;

namespace Winform_Example
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            if (Guard.Login(username.Text, password.Text))
            {
                //Put code here of what you want to do after successful login
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 mainfrm = new Form1();
                mainfrm.Show();
                this.Hide();
            }
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register registerfrm = new Register();
            registerfrm.Show();
        }
    }
}
