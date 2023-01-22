using GTABrasil_Updater.Internals;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.IO.Compression;
using System.Threading;

namespace GTABrasil_Updater
{
    public partial class DownloadScreen : Form
    {
        public static string DestinatePath { get; set; }
        public ProgressBar progressBar1;

        public DownloadScreen()
        {
            InitializeComponent();
            label1.Text = DestinatePath;
        }

        private async void DownloadButton_Click(object sender, EventArgs e)
        {
            string ArchiveName = "GTA-BRASIL";
            string personalAccessToken = "";
            string localPath = DestinatePath;
            string url = "https://codeload.github.com/pulse-club/gta-brasil/legacy.zip/refs/heads/main";

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Authorization", "Token " + personalAccessToken);

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);

                DownloadButton.Enabled = false;

                byte[] fileData = await client.DownloadDataTaskAsync(url);

                File.WriteAllBytes(Path.Combine(localPath, ArchiveName + ".zip"), fileData);
               
                OnDownloadCompleted();

                Console.WriteLine($"Repository '{ArchiveName}' downloaded successfully.");

            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            LoadingBar.Invoke((MethodInvoker)(() => LoadingBar.Value = e.ProgressPercentage));
            label2.Invoke((MethodInvoker)(() => label2.Text = e.ProgressPercentage.ToString() + "%"));
        }

        private void OnDownloadCompleted()
        {
            string zipFile = Path.Combine(DestinatePath, "GTA-BRASIL.ZIP");
            string extractPath = DestinatePath;
            using (ZipArchive archive = ZipFile.OpenRead(zipFile))
            {
                int totalCount = archive.Entries.Count;
                int count = 0;
                ZipFile.ExtractToDirectory(zipFile, DestinatePath + "/temp");
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    count++;
                    LoadingBar.Invoke((MethodInvoker)(() => LoadingBar.Value = (int)((count * 100) / totalCount)));
                    label2.Invoke((MethodInvoker)(() => label2.Text = LoadingBar.Value.ToString() + "%"));
                }
            }
            MessageBox.Show("Download and Unzip Completed!");
            MoveToGame();

        }

        public void MoveToGame()
        {
            string UnzipedFolder = DestinatePath + "/temp";
            string[] subFolders = Directory.GetDirectories(UnzipedFolder);
            string[] AllFiles = Directory.GetFiles(subFolders[0] + "/GTA Brasil/.(mod)");
            string Test = subFolders[0] + "/GTA Brasil/.(mod)";

            Directory.Move(subFolders[0] + "/GTA Brasil/.(mod)", DestinatePath + "/modloader/GTA-Brasil");
            File.Delete(UnzipedFolder);
            File.Delete(DestinatePath + "/gta-brasil.zip");

        }

    }
}
