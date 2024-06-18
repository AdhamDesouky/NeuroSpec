using clinical.userControls;
using FontAwesome.WPF;
using NeuroSpec.Shared.Globals;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        string[] months = {"January", "February", "March", "April", "May", "June", "July",
            "August", "September", "October", "November", "December" };

        private Button selectedYrButton = null;
        private Button selectedMnthButton = null;
        VisitService visitService = new VisitService();
        CalendarEventService calendarEventService = new CalendarEventService();
        PatientService patientService = new PatientService();
        AppointmentTypeService appointmentTypeService = new AppointmentTypeService();

        int leftestYr = DateTime.Now.Year - 2, rightestYr = DateTime.Now.Year + 2;

        DateTime selectedDay;
        public CalendarPage()
        {
            InitializeComponent();

            selectedDay = DateTime.Now;
            Refresh();

            refreshYears();

            for (int i = 1; i <= 12; i++)
            {
                int m = i;
                Button mnthBtn = new Button
                {
                    Content = m.ToString(),
                    Style = (Style)FindResource("calendarButtonMonth")
                };
                mnthBtn.Click += (sender, e) => switchToMnth(sender as Button, m);

                if (m == DateTime.Now.Month)
                {
                    selectedMnthButton = mnthBtn;
                    mnthBtn.Style = (Style)FindResource("calendarButtonMonthBig");
                }

                mnthStack.Children.Add(mnthBtn);
            }


            List<int> hours = new List<int>();
            List<int> minutes = new List<int>();
            for (int i = 0; i <= 23; i++)
            {
                hours.Add(i);
            }
            for (int i = 0; i <= 59; i++)
            {
                minutes.Add(i);
            }
            fromHrCB.ItemsSource = hours;
            toHrCB.ItemsSource = hours;
            fromMinCB.ItemsSource = minutes;
            toMinCB.ItemsSource = minutes;

            fromHrCB.SelectedIndex = 0;
            toHrCB.SelectedIndex = 0;
            fromMinCB.SelectedIndex = 0;
            toMinCB.SelectedIndex = 0;




        }

        void refreshYears()
        {
            yrStack.Children.Clear();
            for (int i = leftestYr; i <= rightestYr; i++)
            {

                int yr = i;
                Button yrBtn = new Button
                {
                    Content = i.ToString(),
                    Style = (Style)FindResource("calendarButton")
                };

                yrBtn.Click += (sender, e) => switchToYr(sender as Button, yr);

                if (yr == DateTime.Now.Year)
                {
                    selectedYrButton = yrBtn;
                    yrBtn.Style = (Style)FindResource("calendarButtonBig");
                }
                yrStack.Children.Add(yrBtn);
            }
        }
        async void Refresh()
        {
            string lab = months[selectedDay.Month - 1] + " " + selectedDay.Year.ToString();
            monthMainLBL.Text = lab;

            dayTB.Text = selectedDay.Day.ToString();

            monthTB.Text = months[selectedDay.Month - 1];
            dayOfTheWeekTB.Text = selectedDay.DayOfWeek.ToString();
            calendar.DisplayDate = selectedDay;


            DateTime dateTime = new DateTime(selectedDay.Year, selectedDay.Month, selectedDay.Day);

            List<Visit> todayVisits = new List<Visit>();
            List<CalendarEvent> calendarEvents = new List<CalendarEvent>();


            if (globals.signedIn.isReciptionist)
            {
                todayVisits = await visitService.GetVisitsByDateAsync(dateTime);
            }
            else
            {
                todayVisits = await visitService.GetDoctorVisitsOnDateAsync(globals.signedIn.UserID, dateTime);
            }
            calendarEvents = await calendarEventService.GetCalendarEventsByUserIDAndDateAsync(globals.signedIn.UserID, dateTime);


            int collectedSize = todayVisits.Count + calendarEvents.Count;
            int doneTotal = 0;

            visitsStackPanel.Children.Clear();

            int i = 0, j = 0;
            while (i < todayVisits.Count || j < calendarEvents.Count)
            {
                if (i < todayVisits.Count && (j == calendarEvents.Count || todayVisits[i].TimeStamp < calendarEvents[j].EventStartTime))
                {
                    Visit visit1 = todayVisits[i];
                    Item itemControl = new Item();
                    itemControl.Title = $"{(await patientService.GetPatientByIdAsync(visit1.PatientID)).FirstName}";
                    itemControl.Description = $"{(await appointmentTypeService.GetAppointmentTypeByIDAsync(visit1.AppointmentTypeID)).Name}";
                    itemControl.IconBell = FontAwesomeIcon.Bell;
                    if (visit1.IsDone)
                    {
                        doneTotal++;
                        itemControl.Icon = FontAwesomeIcon.UserPlus;
                        itemControl.Color = (SolidColorBrush)FindResource("darkerColor");
                    }
                    else
                    {
                        itemControl.Icon = FontAwesomeIcon.UserMd;
                        itemControl.Color = (SolidColorBrush)FindResource("lightFontColor");
                    }

                    itemControl.Time = visit1.TimeStamp.ToString("HH:mm");
                    itemControl.MarkDoneCommand = new RelayCommand(() => markVisitDone(visit1));

                    visitsStackPanel.Children.Add(itemControl);
                    i++;
                }
                else if (j < calendarEvents.Count)
                {
                    CalendarEvent ev = calendarEvents[j];
                    Item itemControl = new Item();
                    itemControl.Title = ev.EventName;
                    itemControl.Description = ev.EventText;
                    itemControl.IconBell = FontAwesomeIcon.Bell;

                    if (ev.IsDone)
                    {
                        doneTotal++;
                        itemControl.Icon = FontAwesomeIcon.CheckCircle;
                        itemControl.Color = (SolidColorBrush)FindResource("darkerColor");
                    }
                    else
                    {
                        itemControl.Icon = FontAwesomeIcon.CalendarCheckOutline;
                        itemControl.Color = (SolidColorBrush)FindResource("lightFontColor");
                    }


                    itemControl.Time = ev.EventStartTime.ToString("HH:mm") + " - " + ev.EventEndTime.ToString("HH:mm");
                    itemControl.MarkDoneCommand = new RelayCommand(() => markEventDone(ev));
                    itemControl.DeleteCommand = new RelayCommand(() => deleteEvent(ev));

                    visitsStackPanel.Children.Add(itemControl);
                    j++;
                }
                else
                {
                    break;
                }

            }
            todayTaskCnt.Text = $"{collectedSize} tasks - {doneTotal} done - {collectedSize - doneTotal} left";


        }

        async void deleteEvent(CalendarEvent ev)
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to drop task {ev.EventName}? This action cannot be undone.", "Delete Task", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                await calendarEventService.DeleteCalendarEventAsync(ev.EventID);
                Refresh();
            }
        }

        async void markVisitDone(Visit visit)
        {
            visit.IsDone = !visit.IsDone;
            await visitService.UpdateVisitAsync(visit);
            Refresh();
        }

        async void markEventDone(CalendarEvent ev)
        {
            ev.IsDone = !ev.IsDone;
            await calendarEventService.UpdateCalendarEventAsync(ev.EventID, ev);
            Refresh();
        }

        private void lblNote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtTitle.Focus();
        }

        private void lblTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtText.Focus();
        }

        private void txtNote_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTitle.Text) && txtTitle.Text.Length > 0)
                lblTitle.Visibility = Visibility.Collapsed;
            else
                lblTitle.Visibility = Visibility.Visible;
        }

        private void txtTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtText.Text) && txtText.Text.Length > 0)
                lblText.Visibility = Visibility.Collapsed;
            else
                lblText.Visibility = Visibility.Visible;
        }

        private void selectedDayChanged(object sender, SelectionChangedEventArgs e)
        {

            selectedDay = calendar.SelectedDate ?? DateTime.MinValue;
            Refresh();
        }

        void switchToYr(Button sender, int year)
        {
            if (selectedYrButton != null)
            {
                selectedYrButton.Style = (Style)FindResource("calendarButton");
            }

            sender.Style = (Style)FindResource("calendarButtonBig");
            selectedYrButton = sender;
            DateTime now = new DateTime(year, selectedDay.Month, selectedDay.Day);
            selectedDay = now;

            Refresh();
        }

        void switchToMnth(Button sender, int month)
        {
            if (selectedMnthButton != null)
            {
                selectedMnthButton.Style = (Style)FindResource("calendarButtonMonth");
            }

            sender.Style = (Style)FindResource("calendarButtonMonthBig");
            selectedMnthButton = sender;
            DateTime now = new DateTime(selectedDay.Year, month, selectedDay.Day);
            selectedDay = now;

            Refresh();
        }

        void goLeftYr(object sender, RoutedEventArgs e)
        {
            leftestYr--;
            rightestYr--;
            refreshYears();
        }
        private void goRightYr(object sender, RoutedEventArgs e)
        {
            leftestYr++;
            rightestYr++;
            refreshYears();
        }

        private void prevDay(object sender, RoutedEventArgs e)
        {
            DateTime now = new DateTime(selectedDay.Year, selectedDay.Month, selectedDay.Day - 1);
            selectedDay = now;

            Refresh();
        }

        private void nextDay(object sender, RoutedEventArgs e)
        {
            DateTime now = new DateTime(selectedDay.Year, selectedDay.Month, selectedDay.Day + 1);
            selectedDay = now;

            Refresh();
        }

        private async void addNote(object sender, MouseButtonEventArgs e)
        {
            DateTime fromDT = new DateTime(selectedDay.Year, selectedDay.Month, selectedDay.Day, (int)fromHrCB.SelectedItem, (int)fromMinCB.SelectedItem, 0);
            DateTime toDT = new DateTime(selectedDay.Year, selectedDay.Month, selectedDay.Day, (int)toHrCB.SelectedItem, (int)toMinCB.SelectedItem, 0);
            string title = txtTitle.Text;
            string desc = txtText.Text;
            CalendarEvent ev = new CalendarEvent
            {
                EventID = IDGeneration.generateNewCalendarEventID(globals.signedIn.UserID),
                UserID = globals.signedIn.UserID,
                EventName = title,
                EventText = desc,
                EventStartTime = fromDT,
                EventEndTime = toDT,
                IsDone = false
            };
            await calendarEventService.InsertCalendarEventAsync(ev);
            Refresh();
        }

    }
}
