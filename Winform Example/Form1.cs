using System;
using System.Windows.Forms;
using Winform_Example.Classes;

namespace Winform_Example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Put variable name here with the name of the variable in your panel - https://i.imgur.com/W7yl3MH.png
            secret.Text = "Server Variable: " + Guard.Var("VARIABLENAME");
            //User Info
            username.Text = "Username: " + UserInfo.Username;
            email.Text = "Email: " + UserInfo.Email;
            hwid.Text = "HWID: " + UserInfo.HWID;
            level.Text = "Level: " + UserInfo.Level;
            ip.Text = "IP: " + UserInfo.IP;
            expiry.Text = "Expiry: " + UserInfo.Expires;
        }
    }
}
