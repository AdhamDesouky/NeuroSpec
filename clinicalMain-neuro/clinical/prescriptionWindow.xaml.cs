using MahApps.Metro.IconPacks;
using NeuroSpec.Shared.Globals;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpec.Shared.Services.OntologyServices;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace clinical
{
    /// <summary>
    /// Interaction logic for prescriptionWindow.xaml
    /// </summary>
    /// 
    internal class dataObj
    {
        internal int freq { get; set;}
        internal string period { get; set; }
        internal string Notes { get; set; }
        internal string Test { get; set; }
        internal int TestId { get; set; }
        internal bool IsSNOMED { get; set; }

        internal dataObj() { }
        internal dataObj(string notes, string test, bool isSNOMED)
        {
            Notes = notes;
            Test = test;
            this.IsSNOMED = isSNOMED;
        }
    }


    public partial class prescriptionWindow : Window
    {
        /// <summary>
        /// Services
        /// </summary>
        VisitService _visitService;
        PatientService _patientService;
        PrescriptionService _prescriptionService;
        IssueSNOMEDService _issueService;
        IssueDrugService _issueDrugService;

        Patient currentPatient;
        Visit currentVisit;

        int prescriptionID;

        List<dataObj> ObjectsList = new List<dataObj>();
        public prescriptionWindow(Visit currVisit, Patient currPatient)
        {
            InitializeComponent();
            _visitService = new VisitService();
            _patientService = new PatientService();
            _prescriptionService = new PrescriptionService();
            _issueService = new ();
            _issueDrugService = new IssueDrugService();



            prescriptionID = IDGeneration.generateNewPrescriptionID(currVisit.VisitID, DateTime.Now);
            currentPatient = currPatient;
            currentVisit = currVisit;
            todayDatePicker.SelectedDate = DateTime.Now;
            patientNameTextBox.Text = currentPatient.FirstName + " " + currentPatient.LastName;
            patientAgeTextBox.Text = StaticFunctions.CalculateAge(currentPatient.DateOfBirth).ToString();
            mainStackPanel.Children.Clear();
        }
        public prescriptionWindow(Prescription prescription)
        {
            InitializeComponent();
            _visitService = new VisitService();
            _patientService = new PatientService();
            _prescriptionService = new PrescriptionService();
            _issueService = new ();
            _issueDrugService = new IssueDrugService();

            prescriptionID = prescription.PrescriptionID;

            InitializeAsync(prescription);
        }

        private async void InitializeAsync(Prescription prescription)
        {
            try
            {
                currentVisit = await _visitService.GetVisitByIDAsync(prescription.VisitID);
                currentPatient = await _patientService.GetPatientByIdAsync(prescription.PatientID);
                todayDatePicker.SelectedDate = DateTime.Now;
                patientNameTextBox.Text = $"{currentPatient.FirstName} {currentPatient.LastName}";
                patientAgeTextBox.Text = StaticFunctions.CalculateAge(currentPatient.DateOfBirth).ToString();
                mainStackPanel.Children.Clear();
                List<IssueDrug> IssuedDrugs = await _issueDrugService.GetAllIssueDrugsByPrescriptionIDAsync(prescription.PrescriptionID);
                List<IssueSNOMED> IssuedSNom = await _issueService.GetAllIssuesByPrescriptionIDAsync(prescription.PrescriptionID);

                foreach (IssueDrug issue in IssuedDrugs)
                {
                    CreateNewIssueDrugUI(issue);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as appropriate
                Console.WriteLine(ex.Message);
            }
        }


        private void CreateNewIssueDrugUI()
        {
            dataObj obj = new dataObj();

            Border border = new Border
            {
                Height = 150,
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
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(55) });


            var packIcon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.MinusBox,
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = (Brush)FindResource("lightFontColor"),
                Margin = new Thickness(8, 0, 0, 0),
                Width = 25,
                Height = 25,
                VerticalAlignment = VerticalAlignment.Center
            };
            packIcon.MouseDown += (sender, e) => removeLast(obj);


            TextBlock textBlockExercise = new TextBlock
            {
                Text = "Drug",
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



            TextBlock textBlockFrequency = new TextBlock
            {
                Text = "Frequency",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };
            Grid.SetRow(textBlockFrequency, 0);
            Grid.SetColumn(textBlockFrequency, 1);


            Grid innerGrid = new Grid
            {
                Margin = new Thickness(0)
            };
            Grid.SetRow(innerGrid, 1);
            Grid.SetColumn(innerGrid, 1);

            innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
            innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });

            TextBox numberTB = new TextBox
            {
                Name = "numberTB",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(8)
            };
            Grid.SetRow(numberTB, 0);
            Grid.SetColumn(numberTB, 0);

            TextBlock textBlockTimesPer = new TextBlock
            {
                Text = " times per ",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };
            Grid.SetRow(textBlockTimesPer, 0);
            Grid.SetColumn(textBlockTimesPer, 1);

            ComboBox freqCB = new ComboBox
            {
                Name = "freqCB",
                Margin = new Thickness(8),
                ItemsSource = new List<string> { "Day", "Week", "Month", "Year" }
            };
            Grid.SetRow(freqCB, 0);
            Grid.SetColumn(freqCB, 2);

            freqCB.SelectionChanged += (sender, e) => UpdateDataObject(freqCB, obj);

            innerGrid.Children.Add(numberTB);
            innerGrid.Children.Add(textBlockTimesPer);
            innerGrid.Children.Add(freqCB);

            TextBlock textBlockNotes = new TextBlock
            {
                Text = "Notes",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)FindResource("lightFontColor")
            };
            Grid.SetRow(textBlockNotes, 2);
            Grid.SetColumn(textBlockNotes, 0);

            TextBox notesTextBox = new TextBox
            {
                Name = "notesTXT",
                Margin = new Thickness(60, 8, 8, 8)
            };
            Grid.SetRow(notesTextBox, 2);
            Grid.SetColumn(notesTextBox, 0);
            Grid.SetColumnSpan(notesTextBox, 2);

            AutoCompleteBox exerciseAutoCompleteBox = new AutoCompleteBox
            {
                Name = "exerciseACT",
                Margin = new Thickness(8),
                FilterMode = AutoCompleteFilterMode.Contains
            };
            Grid.SetRow(exerciseAutoCompleteBox, 1);



            grid.Children.Add(packIcon);
            grid.Children.Add(textBlockExercise);
            grid.Children.Add(textBlockFrequency);
            grid.Children.Add(innerGrid);
            grid.Children.Add(textBlockNotes);
            grid.Children.Add(notesTextBox);
            grid.Children.Add(exerciseAutoCompleteBox);



            border.Child = grid;

            numberTB.TextChanged += (sender, e) => UpdateDataObject(numberTB, obj);
            notesTextBox.TextChanged += (sender, e) => UpdateDataObject(notesTextBox, obj);

            exerciseAutoCompleteBox.TextChanged += async (sender, e) => await DrugAutoCompleteBox_TextChangedAsync(sender, obj);
            obj.IsSNOMED = true;

            ObjectsList.Add(obj);


            mainStackPanel.Children.Add(border);
        }


        //private void CreateNewIssueDrugUI(IssueDrug issue)
        //{
            
        //}

        // Event handler for text change in the AutoCompleteBox
        private async Task ExerciseAutoCompleteBox_TextChangedAsync(object sender, RoutedEventArgs e)
        {
            //wait for 1 second
            AutoCompleteBox autoCompleteBox = sender as AutoCompleteBox;
            if (autoCompleteBox != null && !string.IsNullOrWhiteSpace(autoCompleteBox.Text))
            {
                await Task.Delay(2000);

                var service = new SNOMEDOntologyService();
                var results = await service.SearchSNOMEDOntologyAsync(autoCompleteBox.Text);
                autoCompleteBox.ItemsSource = results.Select(r => r.SNOMEDName).ToList();
            }
        }
        private void CreateNewIssueSNOMEDUI(dataObj elObj)
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
                Text = "Test or Exercise",
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
            packIcon.MouseDown += (sender, e) => removeLast(elObj);
            exerciseAutoCompleteBox.TextChanged += async (sender, e) => await ExerciseAutoCompleteBox_TextChangedAsync(sender, e);
            elObj.IsSNOMED = true;
            


            mainStackPanel.Children.Add(border);
        }


        //create the same four above functions for IssueDrug

        private async Task DrugAutoCompleteBox_TextChangedAsync(object sender, dataObj dataObject)
        {
            //wait for 1 second
            AutoCompleteBox autoCompleteBox = sender as AutoCompleteBox;
            if (autoCompleteBox != null && !string.IsNullOrWhiteSpace(autoCompleteBox.Text))
            {
                await Task.Delay(2000);
                var service = new DrugOntologyService();
                var results = await service.SearchDrugOntologyAsync(autoCompleteBox.Text);
                autoCompleteBox.ItemsSource = results.Select(r => r.Name).ToList();
            }
            dataObject.Test = autoCompleteBox.Text;

        }

        private void CreateNewIssueDrugUI(IssueDrug issue)
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
                Text = issue.Frequency+" Notes:"+issue.Notes
            };
            Grid.SetRow(notesTextBox, 1);
            Grid.SetColumn(notesTextBox, 1);

            AutoCompleteBox exerciseAutoCompleteBox = new AutoCompleteBox
            {
                Name = "exerciseACT",
                Margin = new Thickness(8),
                FilterMode = AutoCompleteFilterMode.Contains,

            };
            Grid.SetRow(exerciseAutoCompleteBox, 1);
            Grid.SetColumn(exerciseAutoCompleteBox, 0);
            exerciseAutoCompleteBox.Text = issue.Name;

            // Attach event handler for text change

            grid.Children.Add(textBlockExercise);
            grid.Children.Add(textBlockNotes);
            grid.Children.Add(notesTextBox);
            grid.Children.Add(exerciseAutoCompleteBox);
            grid.Children.Add(packIcon);

            border.Child = grid;

            dataObj obj = new dataObj();
            obj.Test = exerciseAutoCompleteBox.Text;
            obj.TestId = issue.IssueID;
            obj.Notes = issue.Notes;
            notesTextBox.TextChanged += (sender, e) => UpdateDataObject(notesTextBox, obj);
            exerciseAutoCompleteBox.TextChanged += async (sender, e) => await DrugAutoCompleteBox_TextChangedAsync(exerciseAutoCompleteBox, obj);
            packIcon.MouseDown += (sender, e) => removeLast(obj);
            obj.IsSNOMED = false;

            ObjectsList.Add(obj);

            mainStackPanel.Children.Add(border);
        }

        private void UpdateDataObject(TextBox textBox, dataObj dataObject)
        {
            if (textBox.Name == "numberTB")
            {
                //reject last input if not a number
                if (!int.TryParse(textBox.Text, out int n))
                {
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                    return;
                }
                dataObject.freq = int.Parse( textBox.Text);
            }
            else if (textBox.Name == "notesTXT")
            {
                dataObject.Notes = textBox.Text;
            }
            

        }
        private void UpdateDataObject(ComboBox textBox, dataObj dataObject)
        {
            dataObject.period = textBox.Text;
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

        private async void save(object sender, MouseButtonEventArgs e)
        {
            if (ObjectsList.Count == 0)
            {
                MessageBox.Show("Can't save an empty prescription.");
                return;
            }

            Prescription prescription = new Prescription
            {
                PrescriptionID = prescriptionID,
                TimeStamp = DateTime.Now,
                PatientID = currentPatient.PatientID,
                DoctorID = currentVisit.DoctorID,
                VisitID = currentVisit.VisitID,

            };
            await _prescriptionService.InsertPrescriptionAsync(prescription);
            foreach (dataObj obj in ObjectsList)
            {
                IssueDrug issueDrug = new IssueDrug
                {
                    PrescriptionID = prescriptionID,
                    IssueID = obj.TestId,
                    PatientID = currentPatient.PatientID,
                    DoctorID = currentVisit.DoctorID,
                    VisitID = currentVisit.VisitID,
                    Notes = obj.Notes!=null?obj.Notes:"",
                    Name = obj.Test != null ? obj.Test : "",
                    Frequency= $"{obj.freq} times per {obj.period}",
                };
                await _issueDrugService.InsertIssueDrugAsync(issueDrug);

            }
            MessageBox.Show("Prescription Saved, ID: " + prescriptionID.ToString());
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

        private void newDrug(object sender, MouseButtonEventArgs e)
        {
            CreateNewIssueDrugUI();
        }

        

        private void removeLast(dataObj obj)
        {
            ObjectsList.Remove(obj);
            mainStackPanel.Children.Clear();
            foreach (dataObj dataObj in ObjectsList)
            {
                CreateNewIssueSNOMEDUI(dataObj);
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
