using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpEncoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string code;
        public MainWindow()
        {
            InitializeComponent();
            code = tbInput.Text;
        }
        string FileIn;
        string FileOut;
        OpenFileDialog openFile;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           openFile = new OpenFileDialog();
            openFile.ShowDialog();
            FileIn = File.ReadAllText(openFile.FileName);
            lblPath.Content = openFile.FileName;
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           var tsk = new Task(EncFunc);
            tsk.Start();
            tsk.Wait();
           

           
        }


        public void EncFunc()
        {
           
            int counter = 0;
            for (int i = 0; i < FileIn.Length-1; i++)
            {
                FileOut += Convert.ToChar(FileIn[i] ^ code[counter]);
                if (counter == code.Length - 1)
                    counter = 0;
                else
                    counter++;

            }
            MessageBox.Show(FileOut);
            string path = openFile.FileName.Remove(openFile.FileName.Length - 4, 4) + "(Encode).txt";

            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.WriteLine(FileOut);
            }
            Process.Start(path);
           // FileOut = null;

        }

        public void DEncFunc()
        {

            int counter = 0;
            for (int i = 0; i < FileIn.Length; i++)
            {
                FileOut += Convert.ToChar(FileIn[i] ^ code[counter]);
                if (counter == code.Length - 1)
                    counter = 0;
                else
                    counter++;

            }
            string path = openFile.FileName.Remove(openFile.FileName.Length - 9, 9) + ".txt";
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.WriteLine(FileOut);
            }
            Process.Start(path);
           // FileOut = null;

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
          var tsk = new Task(DEncFunc);
            tsk.Start();

          
        }
    }
}
