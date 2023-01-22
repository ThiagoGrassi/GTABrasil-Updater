using GTABrasil_Updater.Internals;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.IO.Compression;

namespace GTABrasil_Updater
{
    public partial class Form1 : Form
    {
        public static string DestinatePath { get; set; }
        public ProgressBar progressBar1;

        public Form1()
        {
            InitializeComponent();
            label1.Text = DestinatePath;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string ArchiveName = "GTA-BRASIL";
            string personalAccessToken = "";
            string localPath = DestinatePath;
            string url = $"https://codeload.github.com/pulse-club/gta-brasil/legacy.zip/refs/heads/main";

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Authorization", "Token " + personalAccessToken);

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);

                button1.Enabled = false;

                byte[] fileData = await client.DownloadDataTaskAsync(url);

                File.WriteAllBytes(Path.Combine(localPath, ArchiveName + ".zip"), fileData);
               
                OnDownloadCompleted();

                Console.WriteLine($"Repository '{ArchiveName}' downloaded successfully.");

            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar2.Invoke((MethodInvoker)(() => progressBar2.Value = e.ProgressPercentage));
            label2.Invoke((MethodInvoker)(() => label2.Text = e.ProgressPercentage.ToString() + "%"));
        }

        private async void OnDownloadCompleted()
        {
            MessageBox.Show("Download Completed!");

            string zipFile = Path.Combine(DestinatePath, "GTA-BRASIL.ZIP");
            string extractPath = DestinatePath;

            ZipFile.ExtractToDirectory(zipFile, extractPath);
        }
    }
}
