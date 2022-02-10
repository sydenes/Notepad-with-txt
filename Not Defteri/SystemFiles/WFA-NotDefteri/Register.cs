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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        string[] users = new string[0];
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtNewPass.Text==txtNewPass2.Text)
            {
                string newUsername = txtNewUser.Text;
                string newPassword = txtNewPass.Text;
                string newUser = $"{newUsername}½{newPassword}";
                Array.Resize(ref users, users.Length + 1);
                users[users.Length - 1] = newUser;
                StreamWriter yeniKayit = new StreamWriter("Users.txt", true);
                foreach (string item in users)
                {
                    yeniKayit.WriteLine(item);
                }
                yeniKayit.Close();
                UserFile();
                MessageBox.Show("Kayıt Başarılı !");
                this.Close();
            }
            else
            {
                MessageBox.Show("Şifreler Uyuşmuyor !");
            }           
        }
        private void UserFile()
        {
            StreamWriter newNote = new StreamWriter($"{txtNewUser.Text}.txt", true);
            newNote.Close();
        }
        private void Register_Load(object sender, EventArgs e)
        {
            Login login = this.Owner as Login;
        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            Array.Resize(ref users, 0);
            txtNewPass.Clear();
            txtNewPass2.Clear();
            txtNewUser.Clear();
        }
    }
}
