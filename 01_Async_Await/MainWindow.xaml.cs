using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace _01_Async_Await
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random rand = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //1
            //int value = GenerateValue(); //Freez
            //list.Items.Add(value);
            //2
            //Task<int> task = Task.Run(GenerateValue);
            //
            //list.Items.Add(task.Result);///Freez
            //task.Wait();//Freez
            //async - allow method to use await keywords
            //await - wait task without freezing

            //int value =  await task; //task.Wait();//Freez
            //int value =  await Task.Run(GenerateValue);
            int value =  await GenerateValueAsync();
            list.Items.Add(value);
            //list.Items.Add(await GenerateValueAsync());

           // FileStream file = new FileStream();
            //await file.DisposeAsync();

            //MessageBox.Show("Completed!!!");
        }
        int GenerateValue()
        {
            Thread.Sleep(5000);
           
            return rand.Next(1000);
        }
        Task<int> GenerateValueAsync()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(5000);

                return rand.Next(1000);
            });
           
        }
    }
}
