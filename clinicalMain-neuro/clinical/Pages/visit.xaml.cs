using clinical.BaseClasses;
using MahApps.Metro.IconPacks;
using Org.BouncyCastle.Asn1.Crmf;
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
    /// Interaction logic for visit.xaml
    /// </summary>
    public partial class visit : Page
    {
        Visit currVisit;
        Patient currPatient;


        private List<TestItem> testItems = new List<TestItem>();
        public visit(Visit selectedVisit)
        {
            InitializeComponent();
            currVisit = selectedVisit;
            currPatient = DB.GetPatientById(selectedVisit.PatientID);
            heightTB.Text = currVisit.Height.ToString();
            weightTB.Text = currVisit.Weight.ToString();
            patientNameMainTxt.Text = currPatient.FullName;
            patientNameText.Text = currPatient.FullName;
            visitIdText.Text = currVisit.VisitID.ToString();
            visitDate.Text = currVisit.TimeStamp.ToString("g");
            DoctorName.Text = currPatient.DoctorName;
            hwborder.IsEnabled = false;
            
            noteTXT.Text = currVisit.TherapistNotes;
            List<Prescription>prescriptions = DB.GetAllPrescriptionsByVisitID(selectedVisit.VisitID);
            foreach(Prescription p in prescriptions)
            {
                prescriptionsStackPanel.Children.Add(globals.CreatePrescriptionUI(p));
            }
            
            List<EvaluationTestFeedBack> testFeedBacks = DB.GetFeedbackByVisit(selectedVisit.VisitID);

            if (currVisit.IsDone)
            {
                markDoneTXT.Text = "Mark visit Undone";
            }
            else
            {
                markDoneTXT.Text = "Mark visit Done";

            }

            foreach (EvaluationTestFeedBack fd in testFeedBacks)
            {
                TestItem item = new TestItem(DB.GetTestById(fd.TestID).TestName, fd.Notes, fd.Severity, fd.TestID);
                item.feedBackId = fd.TestFeedBackID;
                testItems.Add(item);
                CreateTestItem(item);

            }



        }

       

        private void addTest(object sender, MouseButtonEventArgs e)
        {
            CreateTestItem();
        }

        private void navigateBack(object sender, MouseButtonEventArgs e)
        {
            if(NavigationService.CanGoBack)NavigationService.GoBack();
        }

        private void newPrescription(object sender, MouseButtonEventArgs e)
        {
            prescriptionWindow window = new prescriptionWindow(currVisit, currPatient);
            window.Show();
        }

        private void notes_enable(object sender, MouseButtonEventArgs e)
        {
            noteTXT.IsEnabled = !noteTXT.IsEnabled;
        }

        




        /// <summary>
        /// feedback section
        /// </summary>

        private void CreateTestItem()
        {
            // Create an instance of the TestItem class
            TestItem newItem = new TestItem();
            // Add the TestItem to the list
            testItems.Add(newItem);
            // Create the Border
            Border border = new Border
            {
                Background = (Brush)FindResource("lightFontColor"),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(5, 10, 5, 0),
                Height = 200,
                BorderThickness = new Thickness(3),
                BorderBrush = (Brush)FindResource("darkerColor")
            };

            // Create the Grid
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(75) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });

            // Create and initialize TextBlocks
            TextBlock textBlock1 = new TextBlock { Text = "Test", FontSize = 20, FontWeight = FontWeights.Bold, VerticalAlignment = VerticalAlignment.Center, Foreground = (Brush)FindResource("darkerColor"), Margin = new Thickness(10) };
            TextBlock textBlock2 = new TextBlock { Text = "Score", FontSize = 20, FontWeight = FontWeights.Bold, VerticalAlignment = VerticalAlignment.Top, Foreground = (Brush)FindResource("darkerColor"), Margin = new Thickness(10, 16, 10, 0) };
            TextBlock textBlock3 = new TextBlock { Text = "Notes", FontSize = 20, FontWeight = FontWeights.Bold, VerticalAlignment = VerticalAlignment.Top, Foreground = (Brush)FindResource("darkerColor"), Margin = new Thickness(10, 12, 10, 0) };

            Grid.SetRow(textBlock2, 1);
            Grid.SetRow(textBlock3, 2);


            // Create and initialize TextBox
            TextBox notesTextBox = new TextBox { Name = "notesTXTBox", Margin = new Thickness(0, 15, 10, 15), Foreground = (Brush)FindResource("darkerColor") };
            Grid.SetColumn(notesTextBox, 1);
            Grid.SetRow(notesTextBox, 2);
            Grid.SetColumnSpan(notesTextBox, 2);


            // Create and initialize AutoCompleteBox
            AutoCompleteBox testAutoCompleteBox = new AutoCompleteBox { Margin = new Thickness(0, 18, 5, 18), ItemsSource = DB.GetAllTests(), FilterMode = AutoCompleteFilterMode.Contains };
            Grid.SetColumn(testAutoCompleteBox, 1);

            // Create and initialize Grid for RadioButtons
            Grid radioButtonGrid = new Grid { Margin = new Thickness(0, 2, 10, 0) };
            Grid.SetRow(radioButtonGrid, 1);
            Grid.SetColumn(radioButtonGrid, 1);
            Grid.SetColumnSpan(radioButtonGrid, 2);

            for (int i = 0; i < 10; i++)
            {
                radioButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                Viewbox viewbox = new Viewbox();
                RadioButton radioButton = new RadioButton { GroupName = $"feedBack {testItems.IndexOf(newItem)}" };
                TextBlock textBlock = new TextBlock { Text = $"{i + 1}", TextAlignment = TextAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, FontSize = 18 };
                if (i == 0) radioButton.IsChecked = true;
                int rbsev = i + 1;
                radioButton.Checked += (s, e) => UpdateTestItemRadioButton(newItem, rbsev);
                viewbox.Child = radioButton;
                radioButtonGrid.Children.Add(viewbox);
                radioButtonGrid.Children.Add(textBlock);
                Grid.SetColumn(viewbox, i);
                Grid.SetColumn(textBlock, i);
                Grid.SetRow(viewbox, 0);
                Grid.SetRow(textBlock, 1);
            }

            // Create and initialize MinusBox Icon
            PackIconMaterial minusBoxIcon = new PackIconMaterial { Name = "removeTest", Kind = PackIconMaterialKind.MinusBox, Foreground = (Brush)FindResource("darkerColor"), Margin = new Thickness(2, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center, Width = 30, Height = 30 };
            Grid.SetColumn(minusBoxIcon, 2);

            // Attach event handlers
            minusBoxIcon.MouseDown += RemoveTest;
            notesTextBox.TextChanged += (s, e) => UpdateTestItemNotes(newItem, notesTextBox.Text);
            testAutoCompleteBox.TextChanged += (s, e) => UpdateTestItemAutoCompleteBox(newItem, testAutoCompleteBox.Text);


            // Add controls to the Grid
            grid.Children.Add(textBlock1);
            grid.Children.Add(textBlock2);
            grid.Children.Add(textBlock3);
            grid.Children.Add(notesTextBox);
            grid.Children.Add(testAutoCompleteBox);
            grid.Children.Add(radioButtonGrid);
            grid.Children.Add(minusBoxIcon);

            // Add the Grid to the Border
            border.Child = grid;

            // Add the Border to the main UI
            feedbackStackPane.Children.Add(border);


        }

        private void CreateTestItem(TestItem testFeedBack)
        {

            // Create the Border
            Border border = new Border
            {
                Background = (Brush)FindResource("lightFontColor"),
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(5, 10, 5, 0),
                Height = 200,
                BorderThickness = new Thickness(3),
                BorderBrush = (Brush)FindResource("darkerColor")
            };

            // Create the Grid
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(75) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });

            // Create and initialize TextBlocks
            TextBlock textBlock1 = new TextBlock { Text = "Test", FontSize = 20, FontWeight = FontWeights.Bold, VerticalAlignment = VerticalAlignment.Center, Foreground = (Brush)FindResource("darkerColor"), Margin = new Thickness(10) };
            TextBlock textBlock2 = new TextBlock { Text = "Score", FontSize = 20, FontWeight = FontWeights.Bold, VerticalAlignment = VerticalAlignment.Top, Foreground = (Brush)FindResource("darkerColor"), Margin = new Thickness(10, 16, 10, 0) };
            TextBlock textBlock3 = new TextBlock { Text = "Notes", FontSize = 20, FontWeight = FontWeights.Bold, VerticalAlignment = VerticalAlignment.Top, Foreground = (Brush)FindResource("darkerColor"), Margin = new Thickness(10, 12, 10, 0) };

            Grid.SetRow(textBlock2, 1);
            Grid.SetRow(textBlock3, 2);
            // Create and initialize TextBox
            TextBox notesTextBox = new TextBox { Name = "notesTXTBox", Margin = new Thickness(0, 15, 10, 15), Foreground = (Brush)FindResource("darkerColor"), Text = testFeedBack.Notes };
            Grid.SetColumn(notesTextBox, 1);
            Grid.SetRow(notesTextBox, 2);
            Grid.SetColumnSpan(notesTextBox, 2);


            // Create and initialize AutoCompleteBox
            AutoCompleteBox testAutoCompleteBox = new AutoCompleteBox { Name = "testACT", Text = testFeedBack.TestName, Margin = new Thickness(0, 18, 5, 18), ItemsSource = DB.GetAllTests(), FilterMode = AutoCompleteFilterMode.Contains };
            Grid.SetColumn(testAutoCompleteBox, 1);

            // Create and initialize Grid for RadioButtons
            Grid radioButtonGrid = new Grid { Margin = new Thickness(0, 2, 10, 0) };
            Grid.SetRow(radioButtonGrid, 1);
            Grid.SetColumn(radioButtonGrid, 1);
            Grid.SetColumnSpan(radioButtonGrid, 2);

            int sev = testFeedBack.Severity;
            for (int i = 0; i < 10; i++)
            {
                radioButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                Viewbox viewbox = new Viewbox();
                RadioButton radioButton = new RadioButton { GroupName = $"feedBack {testItems.IndexOf(testFeedBack)}" };
                int rbsev = i + 1;
                radioButton.Checked += (s, e) => UpdateTestItemRadioButton(testFeedBack, rbsev);
                TextBlock textBlock = new TextBlock { Text = $"{i + 1}", TextAlignment = TextAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, FontSize = 18 };
                if (sev == i + 1)
                {
                    radioButton.IsChecked = true;
                }
                viewbox.Child = radioButton;
                radioButtonGrid.Children.Add(viewbox);
                radioButtonGrid.Children.Add(textBlock);
                Grid.SetColumn(viewbox, i);
                Grid.SetColumn(textBlock, i);
                Grid.SetRow(viewbox, 0);
                Grid.SetRow(textBlock, 1);
            }

            // Create and initialize MinusBox Icon
            PackIconMaterial minusBoxIcon = new PackIconMaterial { Name = "removeTest", Kind = PackIconMaterialKind.MinusBox, Foreground = (Brush)FindResource("darkerColor"), Margin = new Thickness(2, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center, Width = 30, Height = 30 };
            Grid.SetColumn(minusBoxIcon, 2);

            // Attach event handlers
            minusBoxIcon.MouseDown += RemoveTest;
            notesTextBox.TextChanged += (s, e) => UpdateTestItemNotes(testFeedBack, notesTextBox.Text);
            testAutoCompleteBox.TextChanged += (s, e) => UpdateTestItemAutoCompleteBox(testFeedBack, testAutoCompleteBox.Text.ToString());


            // Add controls to the Grid
            grid.Children.Add(textBlock1);
            grid.Children.Add(textBlock2);
            grid.Children.Add(textBlock3);
            grid.Children.Add(notesTextBox);
            grid.Children.Add(testAutoCompleteBox);
            grid.Children.Add(radioButtonGrid);
            grid.Children.Add(minusBoxIcon);

            // Add the Grid to the Border
            border.Child = grid;

            // Add the Border to the main UI
            feedbackStackPane.Children.Add(border);

        }


        private void RemoveTest(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                // Find the corresponding Border that contains the UI elements
                Border borderToRemove = FindParent<Border>(element);

                if (borderToRemove != null)
                {
                    // Find the index of the Border within the feedbackStackPane.Children collection
                    int index = feedbackStackPane.Children.IndexOf(borderToRemove);

                    if (index != -1)
                    {
                        // Remove the corresponding TestItem from the list
                        if (index < testItems.Count)
                        {
                            testItems.RemoveAt(index);

                            // Remove the Border from the UI
                            feedbackStackPane.Children.Remove(borderToRemove);
                        }
                    }
                }
            }
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            T parent = parentObject as T;
            return parent ?? FindParent<T>(parentObject);
        }

        private void UpdateTestItemNotes(TestItem item, string notes)
        {
            item.Notes = notes;
        }

        private void UpdateTestItemAutoCompleteBox(TestItem item, string value)
        {
            item.TestName = value;
        }

        private void UpdateTestItemRadioButton(TestItem item, int value)
        {
            item.Severity = value;
        }

        private class TestItem
        {
            public string Notes { get; set; }
            public string TestName { get; set; }
            public int Severity { get; set; }
            public int TestID { get; set; }
            public int feedBackId { get; set; }
            public TestItem(string tn, string notes, int se, int testID)
            {
                TestName = tn;
                this.Notes = notes;
                Severity = se;
                TestID = testID;
                feedBackId = 0;
            }
            public TestItem()
            {
                feedBackId = 0;
            }
        }

        private void saveFeedBacks(object sender, MouseButtonEventArgs e)
        {
            List<EvaluationTest> tests = DB.GetAllTests();

            bool pending = false;
            if (testItems.Count > 0)
            {
                int cnt = 0;
                foreach (TestItem obj in testItems)
                {
                    bool found = false;

                    foreach (EvaluationTest test in tests)
                    {
                        if (obj.TestName == null || obj.TestName.Length == 0) break;
                        if (test.TestName.Trim().ToLower().Equals(obj.TestName.Trim().ToLower()) || test.TestName.Trim().ToLower().Contains(obj.TestName.Trim().ToLower()))
                        {
                            found = true;
                            obj.TestID = test.TestID;
                            break;
                        }
                    }
                    if (!found)
                    {
                        MessageBoxResult result = MessageBox.Show(obj.TestName + " in cell " + (cnt + 1).ToString() + " was not found, do you want to add it to the Tests Library?", "New Test Detected",
                            MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            EvaluationTest ex = new EvaluationTest(new Random().Next(999), obj.TestName, " ");
                            obj.TestID = ex.TestID;
                            DB.InsertTest(ex);
                        }
                        else if (result == MessageBoxResult.No)
                        {
                            MessageBox.Show("Please refer to the test in cell " + (cnt + 1).ToString() + " and edit it or pick a test from the recommendations to be able to proceed.");
                            pending = true;
                            break;
                        }
                    }
                    cnt++;
                }
            }


            if (!pending)
            {
                DB.DeleteAllVisitFeedback(currVisit.VisitID);

                foreach (var test in testItems)
                {

                    int id = globals.generateNewTestFeedBackID(currVisit.VisitID, currPatient.PatientID);
                    EvaluationTestFeedBack newFeedBack = new(id, test.Severity, currVisit.VisitID, currPatient.PatientID, test.TestID, test.Notes, currVisit.TimeStamp);
                    DB.InsertFeedback(newFeedBack);

                }
                MessageBox.Show("FeedBacks Saved");

            }
        }

        private void editHeightAndWeight(object sender, MouseButtonEventArgs e)
        {
            hwborder.IsEnabled = !hwborder.IsEnabled;
        }

        private void updateHeightAndWeight(object sender, MouseButtonEventArgs e)
        {
            double h = Double.Parse(heightTB.Text);
            double w = Double.Parse(weightTB.Text);
            currVisit.Height = h;
            currVisit.Weight = w;
            currPatient.Weight = w;
            currPatient.Height = w;
            hwborder.IsEnabled = false;

            DB.UpdateVisit(currVisit);
            Visit mostRecent= DB.GetMostRecentVisitByPatientID(currPatient.PatientID);
            if (mostRecent == null || mostRecent.VisitID==currVisit.VisitID)
            {
                DB.UpdatePatient(currPatient);
            }
        }

        private void markDoneUnDone(object sender, MouseButtonEventArgs e)
        {
            currVisit.IsDone = !currVisit.IsDone;
            if (currVisit.IsDone)
            {
                markDoneTXT.Text = "Mark visit Undone";
            }
            else
            {
                markDoneTXT.Text = "Mark visit Done";

            }
            DB.UpdateVisit(currVisit);
        }

        
    }
}