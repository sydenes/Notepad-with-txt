using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_NotDefteri
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public string usernameTxt;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string loginControl = txtUserName.Text.Trim() + "½"+ txtPassword.Text.Trim();
            StreamReader readUsers = new StreamReader("Users.txt");
            bool logIn = false;
            while (readUsers.Peek()>= 0)
            {
                string kontrol=readUsers.ReadLine();
                if (kontrol == loginControl)
                {
                    logIn = true;
                    MessageBox.Show("Giriş Başarılı");
                    usernameTxt = txtUserName.Text;
                    txtUserName.Clear();
                    txtPassword.Clear();
                    this.Hide();
                    childForm1.ShowDialog();
                    this.Show();
                }
            }
            if (logIn==false)
            {
                MessageBox.Show("Giriş Başarısız.");
            }
            readUsers.Close();
        }

        private void llblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            childRegister.ShowDialog();
            this.Show();
        }
        Register childRegister;
        Form1 childForm1;
        private void Login_Load(object sender, EventArgs e)
        {
            childRegister = new Register();
            childRegister.Owner = this;

            childForm1 = new Form1();
            childForm1.Owner = this;
        }
    }
}
