using System;
using System.IO;
using System.Windows.Forms;

namespace GTABrasil_Updater
{

    public partial class FirstScreen : Form
    {
        string filePath;

        public FirstScreen()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                filePath = Path.GetDirectoryName(openFileDialog1.FileName);

            DownloadScreen.DestinatePath = filePath;
            this.textBox1.Text = filePath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (filePath == null){
                MessageBox.Show("Você precisa selecionar um arquivo!");
                return;
            }
            this.Hide();
            DownloadScreen f1 = new DownloadScreen();
            f1.ShowDialog();
        }
    }
}
