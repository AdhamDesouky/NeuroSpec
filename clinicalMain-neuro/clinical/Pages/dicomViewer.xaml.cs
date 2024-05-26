using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for dicomViewer.xaml
    /// </summary>
    public partial class dicomViewer : Window
    {
        private byte[] raw8BitBuffer;

        public dicomViewer(string filePath)
        {
            InitializeComponent();
            imgview.OpenImage(filePath);
        }
        private void Exit(object sender, RoutedEventArgs e)
        {

        }

        private void Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Dicom Files|*.dcm";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                
            }
        }

    }
}
