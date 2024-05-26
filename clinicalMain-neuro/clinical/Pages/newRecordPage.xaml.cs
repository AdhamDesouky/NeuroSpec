using clinical.BaseClasses;
using MahApps.Metro.IconPacks;
using Microsoft.Win32;
using NuGet.Protocol.Plugins;
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tesseract;
//using System.Windows.Forms;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for newRecordPage.xaml
    /// </summary>
    public partial class newRecordPage : System.Windows.Controls.Page
    {
        Patient patient;   
        public newRecordPage(MedicalRecord medicalRecord)
        {
            InitializeComponent();
            this.patient = DB.GetPatientById(medicalRecord.PatientID);
            patientNameMainTxt.Text = patient.FirstName + " " + patient.LastName;
            reportTXT.IsEnabled = false;
            DoctorNotesTXT.IsEnabled = false;
            images = medicalRecord.Images;
            refresh();
            DoctorNotesTXT.Text = medicalRecord.DoctorNotes;
            reportTXT.Text=medicalRecord.Report;
        }
        public newRecordPage(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            patientNameMainTxt.Text = patient.FirstName+" "+patient.LastName;
            reportTXT.IsEnabled = false;
            DoctorNotesTXT.IsEnabled = false;
        }

        List<string> images = new List<string>();
        private string readImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select a record to upload";
            openFileDialog.Filter = "All Files (*.*)|*.*";

            openFileDialog.ShowDialog();

            string filePath = openFileDialog.FileName;
            if (filePath != null && filePath != "")
            {
                images.Add(filePath);
                return filePath;
            }
            return "";
        }
        private void refresh()
        {
            scannedScansStackPanel.Children.Clear();
            foreach(string s in images)
            {
                if (s != null)
                {
                    createImageObj(s,"");
                }
            }
        }
        private void createImageObj(string filePath,string type)
        {
            Grid mainGrid = new Grid();
            mainGrid.Margin = new Thickness(10);
            mainGrid.MinHeight = 400;

            RowDefinition row1 = new RowDefinition();
            row1.Height = new GridLength(50);
            RowDefinition row2 = new RowDefinition();
            row2.Height = new GridLength(1, GridUnitType.Star);

            mainGrid.RowDefinitions.Add(row1);
            mainGrid.RowDefinitions.Add(row2);

            Border border = new Border();
            border.Style = (Style)Application.Current.Resources["theBorder"];
            border.Margin = new Thickness(0);
            Grid.SetRowSpan(border, 2);


            Image image = new Image();
            if (Path.GetExtension(filePath)?.ToLower() == ".dcm") {
                image.Source = new BitmapImage(new Uri("C:\\Users\\TOP\\Desktop\\clinicalMain\\clinical\\images\\dicom.JPG"));////

            }
            else
            {
                image.Source = new BitmapImage(new Uri(filePath));

            }

            image.Margin = new Thickness(10);
            Grid.SetRow(image, 1);

            TextBlock textBlock = new TextBlock();
            textBlock.Text = type;
            textBlock.FontSize = 25;
            textBlock.FontWeight = FontWeights.Bold;
            textBlock.VerticalAlignment = VerticalAlignment.Top;
            textBlock.Foreground = (Brush)Application.Current.Resources["lightFontColor"];
            textBlock.Margin = new Thickness(20, 12, 20, 0);

            PackIconMaterial packIcon = new PackIconMaterial();
            packIcon.Kind = PackIconMaterialKind.CreditCardScan;
            packIcon.MouseDown += new MouseButtonEventHandler((s, e) => attemptOCR(s, e, filePath));
            packIcon.Foreground = (Brush)Application.Current.Resources["lightFontColor"];
            packIcon.Margin = new Thickness(20, 12, 10, 0);
            packIcon.HorizontalAlignment = HorizontalAlignment.Right;
            packIcon.Width = 30;
            packIcon.Height = 30;
            image.MouseDown += new MouseButtonEventHandler((s, e) => Image_MouseDown(s, e, filePath));


            PackIconMaterial deleteIcon= new PackIconMaterial();
            deleteIcon.Kind = PackIconMaterialKind.Delete;
            deleteIcon.MouseDown += new MouseButtonEventHandler((s, e) => deleteImage(s, e, filePath));
            deleteIcon.Foreground = (Brush)Application.Current.Resources["lightFontColor"];
            deleteIcon.Margin = new Thickness(20, 12, 60, 0);
            deleteIcon.HorizontalAlignment = HorizontalAlignment.Right;
            deleteIcon.Width = 30;
            deleteIcon.Height = 30;

            mainGrid.Children.Add(border);
            mainGrid.Children.Add(image);
            mainGrid.Children.Add(textBlock);
            mainGrid.Children.Add(packIcon);
            mainGrid.Children.Add(deleteIcon);



            scannedScansStackPanel.Children.Add(mainGrid);
        }

        private void deleteImage(object s, MouseButtonEventArgs e, string filePath)
        {
            images.Remove(filePath);
            refresh();
        }

        private void newXRAY(object sender, MouseButtonEventArgs e)
        {
            string filePath = readImage();
            string type = "Medical Record";
            if(filePath!="")createImageObj(filePath,type);
           
        }

        private void attemptOCR(object sender, MouseButtonEventArgs e,string filePath)
        {
            if (Path.GetExtension(filePath)?.ToLower()==".dcm") { return; }
            string resultText = PerformOCR(filePath);
            resultText = resultText.Trim();
            reportTXT.Text += resultText;
        }

       

        private void Image_MouseDown(object sender, MouseButtonEventArgs e, string filePath)
        {
            if (Path.GetExtension(filePath)?.ToLower() == ".dcm") {
                dicomViewer viewer=new dicomViewer(filePath);
                viewer.Show();
            }
            else {
                viewImage view = new viewImage(filePath);
                view.Show(); 
            }
            
        }

        string PerformOCR(string imagePath)
        {
            try
            {
                using (var engine = new TesseractEngine("C:\\Users\\TOP\\Desktop\\clinicalMain\\clinical\\tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            
                            return text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
                return "Cannot transcribe";
            }
        }

        private void editReportTXT(object sender, MouseButtonEventArgs e)
        {
            reportTXT.IsEnabled = !reportTXT.IsEnabled;

        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            int recID = globals.generateNewRecordID(patient.PatientID);
            MedicalRecord record = new MedicalRecord(
                recID,"Record",DateTime.Now,reportTXT.Text.Trim(),images,patient.PatientID,DoctorNotesTXT.Text.Trim());
            DB.InsertMedicalRecord(record);
            MessageBox.Show("Record saved, Record ID: " + recID);
            NavigationService.GoBack();

        }

        private void scanDoctorNotes(object sender, MouseButtonEventArgs e)
        {
            string filePath = readImage();
            string type = "Note";
            if (filePath != "")
            {
                createImageObj(filePath, type);
                string text = PerformOCR(filePath);
                DoctorNotesTXT.Text += text.Trim();
            }
            

        }

        private void newReport(object sender, MouseButtonEventArgs e)
        {
            string filePath = readImage();
            string type = "Report";
            if (filePath != "")
            {
                createImageObj(filePath, type);
                string text = PerformOCR(filePath);
                reportTXT.Text += text.Trim();
            }
        }

        private void goBackPage(object sender, MouseButtonEventArgs e)
        {
            if(NavigationService.CanGoBack)
            NavigationService.GoBack();

        }

        private void editDoctorNotes(object sender, MouseButtonEventArgs e)
        {
            DoctorNotesTXT.IsEnabled = !DoctorNotesTXT.IsEnabled;
        }
    }

}
