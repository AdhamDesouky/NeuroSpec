
using LiveCharts;
using LiveCharts.Wpf;
using NeuroSpec.Shared.Globals;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpec.Shared.Services.DTO_Services;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using PdfSharp.Charting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for patientViewMainPage.xaml
    /// </summary>
    public partial class patientViewMainPage : Page
    {
        private ObservableCollection<MedicalRecord> medicalRecords;
        Patient currPatient;
        EvaluationTestFeedbackService evaluationTestFeedbackService = new EvaluationTestFeedbackService();
        VisitService visitService = new VisitService();
        PatientChronicService patientChronicService = new PatientChronicService();
        MedicalRecordService medicalRecordService = new MedicalRecordService();
        public patientViewMainPage(Patient patient)
        {
            InitializeComponent();
            globals.selectedPatient = patient;
            this.currPatient = patient;
            patientIDTxt.Text = patient.PatientID.ToString();
            patientNameMainTxt.Text = patient.FirstName + " " + patient.LastName;
            patientNameTxt.Text = patient.FirstName + " " + patient.LastName;
            ageTxt.Text = StaticFunctions.CalculateAge(patient.DateOfBirth).ToString();
            contactInfoTxt.Text = patient.PhoneNumber;
            referringTxt.Text = patient.ReferringDoctor;
            noteTXT.Text = patient.Email;
            heightTxt.Text = patient.Height.ToString();
            weightTxt.Text = patient.Weight.ToString();

            LoadChart();
            initAsync();

            

        }

        async void initAsync()
        {
            List<PatientChronic> patientChronics = await patientChronicService.GetPatientChronicsByPatientIDAsync(currPatient.PatientID);
            foreach (PatientChronic ch in patientChronics)
            {
                CreateChronicBorder(ch);
            }



            foreach (var i in await visitService.GetVisitsByPatientIDAsync(currPatient.PatientID))
            {
                prevVisitsStackPanel.Children.Add(globals.createAppointmentUIObject(i, viewVisit));
            }

            medicalRecordsDataGrid.ItemsSource = await medicalRecordService.GetAllPatientRecordsAsync(currPatient.PatientID);
        }

        private void viewRecord_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecord record = (MedicalRecord)medicalRecordsDataGrid.SelectedItem;
            NavigationService.Navigate(new newRecordPage(record));
        }

        private void viewVisit(Visit vis)
        {
            if (vis != null) NavigationService.Navigate(new visit(vis));

        }

        private void newMedicalRecord(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new newRecordPage(currPatient));

        }

        private void enableNotes(object sender, MouseButtonEventArgs e)
        {
            noteTXT.IsEnabled = !noteTXT.IsEnabled;
        }

        private void editPersonalInfo(object sender, MouseButtonEventArgs e)
        {

        }



        //private void LoadChart()
        //{

        //        List<EvaluationTestFeedBack> feedBacks = DB.GetFeedbackByPatient(currPatient.PatientID);
        //        SeriesCollection = new LiveCharts.SeriesCollection();

        //        var distinctTestIds = feedBacks.Select(feedback => feedback.TestID).Distinct();

        //        foreach (var testId in distinctTestIds)
        //        {
        //            var feedbacksForTestId = feedBacks.Where(feedback => feedback.TestID == testId);

        //            // Group feedbacks by month
        //            var groupedByMonth = feedbacksForTestId
        //                .GroupBy(feedback => feedback.TimeStamp.Month)
        //                .OrderBy(group => group.Key);

        //            LineSeries lineSeries = new LineSeries
        //            {
        //                Title = DB.GetTestById(testId).TestName,
        //                Values = new ChartValues<int>(groupedByMonth.SelectMany(group => group.Select(feedback => feedback.Severity))),
        //                //PointGeometry = null // This removes the point marker
        //            };

        //            SeriesCollection.Add(lineSeries);
        //        }

        //        // Set the X-axis labels dynamically based on the months present in the data and sort them
        //        Labels = feedBacks.Select(feedback => feedback.TimeStamp.ToString("MMM")).Distinct().OrderBy(month => DateTime.ParseExact(month, "MMM", CultureInfo.InvariantCulture).Month).ToArray();
        //        YFormatter = value => value;

        //        DataContext = this;

        //}

        private bool displayMonths = true; // Initial display is months

        private void LoadChart()
        {
            UpdateChart();
        }

        private async void UpdateChart()
        {
            List<EvaluationTestFeedBack> feedBacks = await evaluationTestFeedbackService.GetFeedbackByPatientAsync(currPatient.PatientID);
            List<Visit>visits=await visitService.GetVisitsByPatientIDAsync(currPatient.PatientID);

            SeriesCollection = new LiveCharts.SeriesCollection();

            var distinctTestNames= feedBacks.Select(feedback => feedback.TestName).Distinct();

            foreach (var testName in distinctTestNames)
            {
                var feedbacksForTestId = feedBacks.Where(feedback => feedback.TestName == testName);

                IEnumerable<IGrouping<int, EvaluationTestFeedBack>> groupedData;

                if (displayMonths)
                {
                    // Group feedbacks by month
                    groupedData = feedbacksForTestId
                        .GroupBy(feedback => feedback.TimeStamp.Month)
                        .OrderBy(group => group.Key);
                }
                else
                {
                    // Group feedbacks by week
                    groupedData = feedbacksForTestId
                        .GroupBy(feedback => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(feedback.TimeStamp, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
                        .OrderBy(group => group.Key);
                }
                LineSeries lineSeries = new LineSeries
                {
                    
                    Title = testName,
                    Values = new ChartValues<int>(groupedData.SelectMany(group => group.Select(feedback => feedback.Severity))),
                    PointGeometry = null // This removes the point marker
                };

                SeriesCollection.Add(lineSeries);
            }

            // Create series for weight progress
            var weightSeries = new LineSeries
            {
                Title = "Weight Progress",
                Values = new ChartValues<double>(visits.Select(visit => visit.Weight)),
                PointGeometry = null
            };
            SeriesCollection.Add(weightSeries);

            // Create series for height progress
            var heightSeries = new LineSeries
            {
                Title = "Height Progress",
                Values = new ChartValues<double>(visits.Select(visit => visit.Height)),
                PointGeometry = null
            };
            SeriesCollection.Add(heightSeries);

            // Set the X-axis labels dynamically based on the selected time unit
            Labels = displayMonths
                ? feedBacks.Select(feedback => feedback.TimeStamp.ToString("MMM")).Distinct().OrderBy(month => DateTime.ParseExact(month, "MMM", CultureInfo.InvariantCulture).Month).ToArray()
                : feedBacks.Select(feedback => $"Week {CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(feedback.TimeStamp, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)}").Distinct().OrderBy(week => int.Parse(week.Substring(5))).ToArray();

            YFormatter = value => value;

            // Bind the SeriesCollection to the Legend
            BindingOperations.SetBinding(mainChart, LiveCharts.Wpf.CartesianChart.SeriesProperty, new Binding("SeriesCollection"));

            // Set the DataContext
            DataContext = this;
        }



        private void monthlyWeekly(object sender, MouseButtonEventArgs e)
        {
            displayMonths = !displayMonths;
            UpdateChart();
            //if (displayMonths) monthlyWeeklyTB.Text = "Monthly";
            //else monthlyWeeklyTB.Text = "Weekly";

        }

        public LiveCharts.SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, double> YFormatter { get; set; }


        public void CreateChronicBorder(PatientChronic chronic)
        {
            Border border = new Border
            {
                Background = new SolidColorBrush(Colors.FloralWhite),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(10, 10, 0, 0)
            };

            TextBlock textBlock = new TextBlock
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Text = chronic.ChronicName,
                Style = (Style)Application.Current.Resources["titleText"],
                TextWrapping = TextWrapping.Wrap
            };

            double maxBorderWidth = chronicWrapPanel.ActualWidth;

            double desiredWidth = MeasureTextWidth(chronic.ChronicName, textBlock.FontSize) + 100;

            border.MinWidth = Math.Min(desiredWidth, maxBorderWidth);

            border.Child = textBlock;

            border.MinWidth += 60;
            border.MinHeight += 30;
            chronicWrapPanel.Children.Add(border);
        }



        private double MeasureTextWidth(string text, double fontSize)
        {
            FormattedText formattedText = new(
                text,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface("Arial"),
                fontSize,
                Brushes.Black
            );

            return formattedText.Width;
        }


        private void notesUpdated(object sender, TextChangedEventArgs e)
        {
            currPatient.Email = noteTXT.Text;
        }

        private void navigateBack(object sender, MouseButtonEventArgs e)
        {
            if (NavigationService != null && NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }

}