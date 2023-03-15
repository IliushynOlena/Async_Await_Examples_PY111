using IOExtensions;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace _02_file_copy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = new ViewModel()
            {
                Sourse = "D:\\OneDrive - ITSTEP\\MyMaterials\\2 семестр\\ООП\\C++ -" +
               " Exceptions-20210623_094011-Meeting Recording.mp4",
                Destination = "C:\\Users\\helen\\Desktop\\Test",
                Progress = 0
            };
            this.DataContext = model;
        }

        private async void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            //type 1 - using File class
            //C:\Users\helen\Desktop\Test\SP_async_1.docx
            //C:\Users\helen\Desktop\Test
            string filename = Path.GetFileName(model.Sourse);
            string destFilename = Path.Combine(model.Destination, filename);
            //File.Copy(Sourse, destFilename, true);

            await CopyFileAsync(model.Sourse, destFilename);

            MessageBox.Show("Complete!");
        }
        private Task CopyFileAsync(string src, string dest)
        {
            //return Task.Run(() =>
            //{
            //    //type 2 - FileStream
            //    using FileStream srcStream = new FileStream(src, FileMode.Open, FileAccess.Read);//12
            //    using FileStream destStream = new FileStream(dest, FileMode.Create, FileAccess.Write);
            //    byte[] buffer = new byte[1024 * 8];//8Kb
            //    int bytes = 0;
            //    do
            //    {
            //        bytes = srcStream.Read(buffer, 0, buffer.Length);
            //        destStream.Write(buffer, 0, bytes);
            //        // % = received / total * 100
            //        float percent = destStream.Length / (srcStream.Length / 100);
            //        model.Progress = percent;

            //        //
            //    } while (bytes > 0);
            //});

            // type 3 FileTransferManager
        

            return FileTransferManager.CopyWithProgressAsync(src, dest, (pr) =>
            {
                model.Progress = pr.Percentage;

                //MessageBox.Show(progress.Percentage.ToString());
            }, false);
        }

        private void OpenSourseClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                model.Sourse = dialog.FileName;
            }
        }
        private void OpenDestClick(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                model.Destination = dialog.FileName;
            }
        }
    }
    [AddINotifyPropertyChangedInterface]
    class ViewModel
    {
        public string Sourse { get; set; }
        public string Destination { get; set; }
        public double Progress { get; set; }
        public bool IsWaiting => Progress == 0;
    }
}
