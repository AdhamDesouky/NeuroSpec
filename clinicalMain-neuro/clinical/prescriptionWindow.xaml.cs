using NeuroSpec.Shared.Models.DTO;
using MahApps.Metro.IconPacks;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using PdfSharp.Pdf;
using System.Xml;
using System.Windows.Markup;
using clinical.Pages;
using System.Numerics;

namespace clinical
{
    /// <summary>
    /// Interaction logic for prescriptionWindow.xaml
    /// </summary>
    /// 
    internal class dataObj
    {
        internal string Notes { get; set; }
        internal string Test { get; set; }
        internal int TestId { get; set; }
        internal dataObj() { }
        internal dataObj(string notes, string test)
        {
            Notes = notes;
            Test = test;
        }
    }


    public partial class prescriptionWindow : Window
    {
        Patient currentPatient;
        Visit currentVisit;

        int prescriptionID;

        List<dataObj> ObjectsList = new List<dataObj>();
        public prescriptionWindow(Visit currVisit, Patient currPatient)
        {
            InitializeComponent();
            prescriptionID = globals.generateNewPrescriptionID(currVisit.VisitID, DateTime.Now);
            currentPatient = currPatient;
            currentVisit = currVisit;
            todayDatePicker.SelectedDate = DateTime.Now;
            patientNameTextBox.Text = currentPatient.FirstName + " " + currentPatient.LastName;
            patientAgeTextBox.Text = currentPatient.Age.ToString();
            mainStackPanel.Children.Clear();
        }
        public prescriptionWindow(Prescription prescription)
        {
            InitializeComponent();

            currentVisit = DB.GetVisitByID(prescription.VisitID);
            currentPatient = DB.GetPatientById(prescription.PatientID);
            todayDatePicker.SelectedDate = DateTime.Now;
            patientNameTextBox.Text = currentPatient.FirstName + " " + currentPatient.LastName;
            patientAgeTextBox.Text = currentPatient.Age.ToString();
            mainStackPanel.Children.Clear();
            List<IssueScan>IssuedScans= DB.GetAllIssueScansByPrescriptionID(prescription.PrescriptionID);
            foreach (IssueScan issue in IssuedScans)
            {
                CreateNewIssueExerciseUI(issue);
            }
        }

        
        private void CreateNewIssueExerciseUI()
        {


            Border border = new Border
            {
                Height = 100,
                Margin = new Thickness(5),
                CornerRadius = new CornerRadius(5),
                BorderBrush = (Brush)FindResource("selectedColor"),
                BorderThickness = new Thickness(5),
                Background = (Brush)FindResource("darkerColor")
            };

            Grid grid = new Grid
            {
                Margin = new Thickness(5)
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });


            PackIconMaterial packIcon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.MinusBox,
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = (Brush)FindResource("lightFontColor"),
                Margin = new Thickness(8, 0, 0, 0),
                Width = 25,
                Height = 25,
                VerticalAlignment = VerticalAlignment.Center
            };


            TextBlock textBlockExercise = new TextBlock
            {
                Text = "Test or Drug",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };

            Grid.SetRow(textBlockExercise, 0);
            Grid.SetColumn(textBlockExercise, 0);

            

            TextBlock textBlockNotes = new TextBlock
            {
                Text = "Notes",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };

            Grid.SetRow(textBlockNotes, 0);
            Grid.SetColumn(textBlockNotes, 2);

            TextBox notesTextBox = new TextBox
            {
                Name = "notesTXT",
                Margin = new Thickness(8)
            };
            Grid.SetRow(notesTextBox, 1);
            Grid.SetColumn(notesTextBox, 2);

            AutoCompleteBox exerciseAutoCompleteBox = new AutoCompleteBox
            {
                Name = "exerciseACT",
                Margin = new Thickness(8),
                ItemsSource = DB.GetAllScanTests(),
                FilterMode = AutoCompleteFilterMode.Contains

            };
            Grid.SetRow(exerciseAutoCompleteBox, 1);

            

            grid.Children.Add(textBlockExercise);
            grid.Children.Add(textBlockNotes);
            grid.Children.Add(notesTextBox);
            grid.Children.Add(exerciseAutoCompleteBox);
            grid.Children.Add(packIcon);


            border.Child = grid;

            dataObj obj = new dataObj();
            notesTextBox.TextChanged += (sender, e) => UpdateDataObject(notesTextBox, obj);
            exerciseAutoCompleteBox.TextChanged += (sender, e) => UpdateDataObject(exerciseAutoCompleteBox, obj);
            packIcon.MouseDown += (sender, e) => removeLast(obj);

            ObjectsList.Add(obj);


            mainStackPanel.Children.Add(border);
        }
        private void CreateNewIssueExerciseUI(IssueScan issue)
        {


            Border border = new Border
            {
                Height = 100,
                Margin = new Thickness(5),
                CornerRadius = new CornerRadius(5),
                BorderBrush = (Brush)FindResource("selectedColor"),
                BorderThickness = new Thickness(5),
                Background = (Brush)FindResource("darkerColor")
            };

            Grid grid = new Grid
            {
                Margin = new Thickness(5)
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });


            PackIconMaterial packIcon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.MinusBox,
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = (Brush)FindResource("lightFontColor"),
                Margin = new Thickness(8, 0, 0, 0),
                Width = 25,
                Height = 25,
                VerticalAlignment = VerticalAlignment.Center
            };


            TextBlock textBlockExercise = new TextBlock
            {
                Text = "Exercise",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };

            Grid.SetRow(textBlockExercise, 0);
            Grid.SetColumn(textBlockExercise, 0);

            

            TextBlock textBlockNotes = new TextBlock
            {
                Text = "Notes",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };

            Grid.SetRow(textBlockNotes, 0);
            Grid.SetColumn(textBlockNotes, 1);

            TextBox notesTextBox = new TextBox
            {
                Name = "notesTXT",
                Margin = new Thickness(8),
                Text = issue.Notes
            };
            Grid.SetRow(notesTextBox, 1);
            Grid.SetColumn(notesTextBox, 1);

            AutoCompleteBox exerciseAutoCompleteBox = new AutoCompleteBox
            {
                Name = "exerciseACT",
                Margin = new Thickness(8),
                ItemsSource = DB.GetAllScanTests(),
                FilterMode = AutoCompleteFilterMode.Contains,
                Text = DB.GetScanTestById(issue.ScanTestID).ScanTestName

            };
            Grid.SetRow(exerciseAutoCompleteBox, 1);

            grid.Children.Add(textBlockExercise);
            grid.Children.Add(textBlockNotes);
            grid.Children.Add(notesTextBox);
            grid.Children.Add(exerciseAutoCompleteBox);
            grid.Children.Add(packIcon);

            border.Child = grid;

            dataObj obj = new dataObj();
            obj.Test = exerciseAutoCompleteBox.Text;
            obj.TestId = issue.ScanTestID;
            obj.Notes = issue.Notes;
            notesTextBox.TextChanged += (sender, e) => UpdateDataObject(notesTextBox, obj);
            exerciseAutoCompleteBox.TextChanged += (sender, e) => UpdateDataObject(exerciseAutoCompleteBox, obj);
            packIcon.MouseDown += (sender, e) => removeLast(obj);

            ObjectsList.Add(obj);


            mainStackPanel.Children.Add(border);
        }
        private void CreateNewIssueExerciseUI(dataObj elObj)
        {


            Border border = new Border
            {
                Height = 100,
                Margin = new Thickness(5),
                CornerRadius = new CornerRadius(5),
                BorderBrush = (Brush)FindResource("selectedColor"),
                BorderThickness = new Thickness(5),
                Background = (Brush)FindResource("darkerColor")
            };

            Grid grid = new Grid
            {
                Margin = new Thickness(5)
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });


            PackIconMaterial packIcon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.MinusBox,
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = (Brush)FindResource("lightFontColor"),
                Margin = new Thickness(8, 0, 0, 0),
                Width = 25,
                Height = 25,
                VerticalAlignment = VerticalAlignment.Center
            };


            TextBlock textBlockExercise = new TextBlock
            {
                Text = "Test or Drug",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };

            Grid.SetRow(textBlockExercise, 0);
            Grid.SetColumn(textBlockExercise, 0);

            

            TextBlock textBlockNotes = new TextBlock
            {
                Text = "Notes",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };

            Grid.SetRow(textBlockNotes, 0);
            Grid.SetColumn(textBlockNotes, 2);

            TextBox notesTextBox = new TextBox
            {
                Name = "notesTXT",
                Margin = new Thickness(8),
                Text = elObj.Notes
            };
            Grid.SetRow(notesTextBox, 1);
            Grid.SetColumn(notesTextBox, 2);

            AutoCompleteBox exerciseAutoCompleteBox = new AutoCompleteBox
            {
                Name = "exerciseACT",
                Margin = new Thickness(8),
                ItemsSource = DB.GetAllScanTests(),
                FilterMode = AutoCompleteFilterMode.Contains,
                Text = elObj.Test

            };
            Grid.SetRow(exerciseAutoCompleteBox, 1);

            

            grid.Children.Add(textBlockExercise);
            grid.Children.Add(textBlockNotes);
            grid.Children.Add(notesTextBox);
            grid.Children.Add(exerciseAutoCompleteBox);
            grid.Children.Add(packIcon);

            border.Child = grid;


            notesTextBox.TextChanged += (sender, e) => UpdateDataObject(notesTextBox, elObj);
            exerciseAutoCompleteBox.TextChanged += (sender, e) => UpdateDataObject(exerciseAutoCompleteBox, elObj);
            packIcon.MouseDown += (sender, e) => removeLast(elObj);


            mainStackPanel.Children.Add(border);
        }


        private void UpdateDataObject(TextBox textBox, dataObj dataObject)
        {
            if (textBox.Name == "notesTXT")
            {
                dataObject.Notes = textBox.Text;
            }
        }
        private void UpdateDataObject(AutoCompleteBox textBox, dataObj dataObject)
        {
            if (textBox.Name == "exerciseACT")
            {
                dataObject.Test = textBox.Text;
            }
            
        }

        private void print(object sender, MouseButtonEventArgs e)
        {
            // Create a PrintDialog
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                // Print the visual representation of the current page
                printDialog.PrintVisual(this, "Print Job");
            }
            //// Create a PrintDialog
            //PrintDialog printDialog = new PrintDialog();

            //if (printDialog.ShowDialog() == true)
            //{
            //    // Create a Grid to host a copy of the current UI
            //    Grid printGrid = new Grid();
            //    printGrid.Background = Brushes.White;
            //    printGrid.Children.Add((UIElement)Content);

            //    // Set the layout size to the printed area
            //    printGrid.Measure(new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));
            //    printGrid.Arrange(new Rect(new Point(0, 0), printGrid.DesiredSize));

            //    // Print the UI
            //    printDialog.PrintVisual(printGrid, "Print Job");
            //}
        }

        private UIElement CloneVisual(UIElement element)
        {
            // Clone the visual element to avoid the "already the logical child" error
            if (element is FrameworkElement frameworkElement)
            {
                // Use XamlLoader to clone the element
                string elementXaml = XamlWriter.Save(frameworkElement);
                StringReader stringReader = new StringReader(elementXaml);
                XmlReader xmlReader = XmlReader.Create(stringReader);
                UIElement clonedElement = (UIElement)XamlReader.Load(xmlReader);
                return clonedElement;
            }
            return null;
        }
        private void save(object sender, MouseButtonEventArgs e)
        {
            bool pending = false;
            if (ObjectsList.Count > 0)
            {
                int cnt = 0;
                foreach (dataObj obj in ObjectsList)
                {
                    bool found = false;

                    foreach (ScanTest ex in DB.GetAllScanTests())
                    {
                        if (obj.Test == null || obj.Test.Length == 0) break;
                        if (ex.ScanTestName.Trim().ToLower().Equals(obj.Test.Trim().ToLower()) || ex.ScanTestName.Trim().ToLower().Contains(obj.Test.Trim().ToLower()))
                        {
                            found = true;
                            obj.TestId = ex.ScanTestID;
                            break;
                        }
                    }
                    if (!found)
                    {
                        MessageBoxResult result = MessageBox.Show(obj.Test + " in cell " + (cnt + 1).ToString() + " was not found, do you want to add it to the Test/Drug Library?", "New Test/Drug Detected",
                            MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            ScanTest sc=new ScanTest(new Random().Next(999), obj.Test," "," ");
                            obj.TestId = sc.ScanTestID;
                            DB.InsertScanTest(sc);
                        }
                        else if (result == MessageBoxResult.No)
                        {
                            MessageBox.Show("Please refer to the Test/Drug in cell " + (cnt + 1).ToString() + " and edit it or pick a Test/Drug from the recommendations to be able to proceed.");
                            pending = true;
                            break;
                        }
                    }
                    cnt++;
                }
            }
            else
            {
                MessageBox.Show("Can't save an empty prescription.");
                pending = true;
            }

            if (!pending)
            {
                Prescription prescription = new Prescription(prescriptionID, DateTime.Now, currentPatient.PatientID, currentVisit.DoctorID, currentVisit.VisitID);
                DB.InsertPrescription(prescription);
                foreach (dataObj obj in ObjectsList)
                {
                    IssueScan issue = new IssueScan(globals.generateNewIssueExerciseID(prescriptionID, currentPatient.PatientID), obj.TestId, prescriptionID, currentPatient.PatientID, obj.Notes);
                    DB.InsertIssueScan(issue);
                }
                MessageBox.Show("Prescription Saved, ID: " + prescriptionID.ToString());

            }
        }

        private void closeBTN(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();

        }

        private void minimizeBTN(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void newExcersice(object sender, MouseButtonEventArgs e)
        {


            CreateNewIssueExerciseUI();

        }

        private void newTreatment(object sender, MouseButtonEventArgs e)
        {

        }

        private void removeLast(dataObj obj)
        {
            ObjectsList.Remove(obj);
            mainStackPanel.Children.Clear();
            foreach (dataObj dataObj in ObjectsList)
            {
                CreateNewIssueExerciseUI(dataObj);
            }
        }





        ///printing
        /////////////////////////////////////////////////
        ///






        private void PrintCurrentPage_Click(object sender, RoutedEventArgs e)
        {
            
        }




    }
}
