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

            Form1.DestinatePath = filePath;
            this.textBox1.Text = filePath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void FirstScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
