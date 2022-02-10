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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] notes = new string[0]; //kaydedilen ve txt'den okunan tüm notların tutulduğu dizi
        bool kontrol = false;
        Login login;
        //"Notes.txt"yi okuyup her bir satırı dizi elemanı olarak atama
        private void Form1_Load(object sender, EventArgs e)
        {
            txtContent.Clear();
            txtTitle.Clear();
            ShowBox();
            login = (Login)this.Owner;

            StreamReader readNotes = new StreamReader($"{login.usernameTxt}.txt");
            int satirSayisi = 0;
            while (readNotes.Peek() >= 0)
            {
                Array.Resize(ref notes, notes.Length + 1);
                notes[satirSayisi] = readNotes.ReadLine();
                satirSayisi++;
            }
            readNotes.Close();
            TittleList();
        }

        //title ve content box'larının içeriğini birleştirip string dizisine eleman olarak atama
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContent.Text) || string.IsNullOrEmpty(txtTitle.Text) )
            {
                MessageBox.Show("Başlık veya içerik boş bırakılamaz !");
            }
            else
            {
                if (LstTitles.SelectedIndex >= 0)
                {
                    string title = txtTitle.Text;
                    string content = txtContent.Text;
                    for (int i = 0; i < notes.Length; i++)
                    {
                        if (i == LstTitles.SelectedIndex)
                        {
                            notes[i] = $"{title}½{content}";
                        }
                    }
                    SaveNote();
                }
                else
                {
                    string title = txtTitle.Text;
                    string content = txtContent.Text;
                    Array.Resize(ref notes, notes.Length + 1);
                    notes[notes.Length - 1] = $"{title}½{content}";
                    SaveNote();
                }
                kontrol = false;
                ShowBox();
            }   
        }
        //save_click ile alınan dizinin elemanlarını "Notes.txt" dosyasına yazma
        private void SaveNote()
        {
            FileStream fileNote = new FileStream($"{login.usernameTxt}.txt", FileMode.Truncate, FileAccess.Write);
            StreamWriter writeTxt = new StreamWriter(fileNote);
            foreach (string item in notes)
            {
                if (!string.IsNullOrEmpty(item) && !string.IsNullOrWhiteSpace(item))
                {
                    writeTxt.WriteLine(item);
                }   
            }
            writeTxt.Close();
            fileNote.Close();
            txtTitle.Clear();
            txtContent.Clear();
            TittleList();
        }
        //"notes" dizisinin elemanlarını alıp '-' den split'leyerek listeye ekler
        private void TittleList()
        {
            try
            {
                LstTitles.Items.Clear();
                foreach (string item in notes)
                {
                    string[] listTitle = item.Split('½');
                    LstTitles.Items.Add(listTitle[0]);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            
        }

        //1 eksik elemanlı dizi oluşturup listeden seçilen başlığın indexine göre bu elemanı atlayıp kalanları yeni diziye atar, yeni diziyi notes'a eşitler, sonrada çağırılan metot ile değişmiş diziyi txt'ye baştan yazar.
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LstTitles.SelectedIndex>=0)
            {
                int index = LstTitles.SelectedIndex;
                string[] newNotes = new string[notes.Length - 1];
                int sayac = 0;
                foreach (string item in notes)
                {
                    if (item == notes[index])
                    {

                    }
                    else
                    {
                        newNotes[sayac] = item;
                        sayac++;
                    }
                }
                notes = newNotes;
                kontrol = false;
                ShowBox();
                SaveNote();
            }
            else
            {
                MessageBox.Show("Silinecek notu seçiniz.");
            }
            
        }
        //Listeden seçilen title'ın indexine göre "notes" dizisinden o metin elemanı seçip görüntüler
        private void LstTitles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = LstTitles.SelectedIndex;
                if (selectedIndex >= 0)
                {
                    string selectedNote = notes[selectedIndex];
                    string[] selected = notes[selectedIndex].Split('½');
                    txtTitle.Text = selected[0];
                    txtContent.Text = selected[1];
                    kontrol = true;
                    ShowBox();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        //yeni not oluşturmak için kutucukları görünüp kılıp içeriğini temizler
        private void btnNewNote_Click(object sender, EventArgs e)
        {
            LstTitles.SelectedIndex = -1;
            txtTitle.Clear();
            txtContent.Clear();
            kontrol = true;
            ShowBox();
        }

        //kutucukların visible özelliğini açıp kapama metodu
        private void ShowBox()
        {

            if (kontrol == true)
            {
                txtTitle.Visible = true;
                txtContent.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
            }
            else
            {
                txtTitle.Visible = false;
                txtContent.Visible = false;
                label1.Visible = false;
                label2.Visible = false;

            }
        }

        //Form kapatılırken yeniden açıldığında notes dizisinde hata almamak için sıfırladık
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Array.Resize(ref notes, 0);
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            string contentControl = txtContent.Text;
            foreach (char item in contentControl)
            {
                if (item == '\n')
                {
                    MessageBox.Show("Alt satır özelliği desteklememekte!\nLütfen 'space' tuşundan faydalanınız.");
                    string content = txtContent.Text;
                    txtContent.Text = content.Trim('\n');
                }
            }
        }
    }
}
