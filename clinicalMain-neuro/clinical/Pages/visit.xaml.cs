using MahApps.Metro.IconPacks;
using NeuroSpec.Shared.Globals;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for visit.xaml
    /// </summary>
    public partial class visit : Page
    {
        Visit currVisit;
        Patient currPatient;

        VisitService VisitService = new VisitService();
        UserService UserService = new UserService();
        PatientService PatientService = new PatientService();
        PrescriptionService PrescriptionService = new PrescriptionService();
        EvaluationTestFeedbackService EvaluationTestFeedbackService = new EvaluationTestFeedbackService();

        private List<TestItem> testItems = new List<TestItem>();
        public visit(Visit selectedVisit)
        {
            InitializeComponent();
            currVisit = selectedVisit;
            initAsync();
            heightTB.Text = currVisit.Height.ToString();
            weightTB.Text = currVisit.Weight.ToString();
            visitIdText.Text = currVisit.VisitID.ToString();
            visitDate.Text = currVisit.TimeStamp.ToString("g");
            hwborder.IsEnabled = false;

            noteTXT.Text = currVisit.TherapistNotes;

        }
        private async void initAsync()
        {
            DoctorName.Text = (await UserService.GetUserByIdAsync(currVisit.DoctorID)).FullName;
            currPatient = await PatientService.GetPatientByIdAsync(currVisit.PatientID);
            patientNameMainTxt.Text = currPatient.FirstName;
            patientNameText.Text = currPatient.FirstName;
            List<Prescription> prescriptions = await PrescriptionService.GetAllPrescriptionsByVisitIDAsync(currVisit.VisitID);
            foreach (Prescription p in prescriptions)
            {
                prescriptionsStackPanel.Children.Add(await globals.CreatePrescriptionUI(p));
            }

            List<EvaluationTestFeedBack> testFeedBacks = await EvaluationTestFeedbackService.GetFeedbackByVisitAsync(currVisit.VisitID);

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
                TestItem item = new TestItem(fd.TestName, fd.Notes, fd.Severity);
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
            if (NavigationService.CanGoBack) NavigationService.GoBack();
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
            AutoCompleteBox testAutoCompleteBox = new AutoCompleteBox { Margin = new Thickness(0, 18, 5, 18), FilterMode = AutoCompleteFilterMode.Contains };
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
            AutoCompleteBox testAutoCompleteBox = new AutoCompleteBox { Name = "testACT", Text = testFeedBack.TestName, Margin = new Thickness(0, 18, 5, 18), FilterMode = AutoCompleteFilterMode.Contains };
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
            public int feedBackId { get; set; }
            public TestItem(string tn, string notes, int se)
            {
                TestName = tn;
                this.Notes = notes;
                Severity = se;
                feedBackId = 0;
            }
            public TestItem()
            {
                feedBackId = 0;
            }
        }

        private async void saveFeedBacks(object sender, MouseButtonEventArgs e)
        {

            if (testItems.Count == 0)
            {
                MessageBox.Show("No Tests to save");
                return;
            }

            await EvaluationTestFeedbackService.DeleteAllVisitFeedbackAsync(currVisit.VisitID);


            for (int i = 0; i < testItems.Count; i++)
            {

                int id = IDGeneration.generateNewTestFeedBackID(currVisit.VisitID, currPatient.PatientID);
                EvaluationTestFeedBack newFeedBack = new EvaluationTestFeedBack
                {
                    TestFeedBackID = id,
                    Severity = testItems[i].Severity,
                    VisitID = currVisit.VisitID,
                    PatientID = currPatient.PatientID,
                    TestName = testItems[i].TestName,
                    Notes = testItems[i].Notes,
                    TimeStamp = currVisit.TimeStamp
                };
                await EvaluationTestFeedbackService.InsertFeedbackAsync(newFeedBack);
            }
            MessageBox.Show("FeedBacks Saved");

        }

        private void editHeightAndWeight(object sender, MouseButtonEventArgs e)
        {
            hwborder.IsEnabled = !hwborder.IsEnabled;
        }

        private async void updateHeightAndWeight(object sender, MouseButtonEventArgs e)
        {
            double h = Double.Parse(heightTB.Text);
            double w = Double.Parse(weightTB.Text);
            currVisit.Height = h;
            currVisit.Weight = w;
            currPatient.Weight = w;
            currPatient.Height = w;
            hwborder.IsEnabled = false;

            await VisitService.UpdateVisitAsync(currVisit.VisitID, currVisit);

            await PatientService.UpdatePatientAsync(currPatient);

        }

        private async void markDoneUnDone(object sender, MouseButtonEventArgs e)
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
            await VisitService.UpdateVisitAsync(currVisit.VisitID, currVisit);
        }


    }
}