using Microsoft.Win32;
using System;
using System.Windows;

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
