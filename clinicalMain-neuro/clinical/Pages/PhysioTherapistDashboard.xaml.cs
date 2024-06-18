using HtmlAgilityPack;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpec.Shared.Models.Ontology;
using NeuroSpec.Shared.Services.OntologyServices;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace clinical.Pages
{
    /// <summary>
    /// Interaction logic for DoctorDashboard.xaml
    /// </summary>
    public partial class DoctorDashboard : Page
    {
        private DateTime currentDayIndex = DateTime.Now;
        private User Doctor;
        VisitService VisitService;
        public DoctorDashboard(User therapist)
        {
            InitializeComponent();
            if (therapist == null) throw new ArgumentNullException();
            VisitService = new VisitService();
            Doctor = therapist;
            UpdateDayBorders();
            leftSideFrame.NavigationService.Navigate(new DashBoardPage());
            signedInTB.Text = $"Welcome, Dr. {therapist.FirstName}";
            PopulateArticleDataGrid();


        }


        
        async Task updateDayAppointments()
        {
            todayAppointmentsStackPanel.Children.Clear();
            //List<Visit> visits = await VisitService.GetDoctorVisitsOnDate(Doctor.UserID, currentDayIndex);
            //numberOfAppointmentsTB.Text = visits.Count.ToString();

            //foreach (var i in visits)
            //{
            //    todayAppointmentsStackPanel.Children.Add(await globals.createAppointmentUIObject(i, viewVisit, viewPatient));
            //}
        }
        private void viewPatient(Patient patient)
        {

            if (patient != null)
            {
                NavigationService.Navigate(new patientViewMainPage(patient));
            }

        }
        private void viewVisit(Visit visit)
        {
            if (visit != null)
            {
                NavigationService.Navigate(new visit(visit));
            }
        }
        private void view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newAppointment(object sender, MouseButtonEventArgs e)
        {
            newAppointmentWindow window = new newAppointmentWindow();
            window.Show();
        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {

        }


        void createDayUI(DateTime date)
        {

            Border border = new Border
            {
                Style = (Style)Application.Current.Resources["theLinedBorder"],
                Background = (Brush)Application.Current.Resources["lighterColor"],
                Margin = new Thickness(5),
                Width = 45
            };
            if (date == currentDayIndex)
            {
                border.Background = (Brush)Application.Current.Resources["selectedColor"];
            }
            border.MouseDown += (sender, e) => selectDay(date);

            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35) });

            TextBlock dayTextBlock = new TextBlock
            {
                Text = date.ToString("ddd"),
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 13
            };

            TextBlock dateTextBlock = new TextBlock
            {
                Text = date.ToString("dd"),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 22
            };

            grid.Children.Add(dayTextBlock);
            grid.Children.Add(dateTextBlock);

            Grid.SetRow(dateTextBlock, 1);

            border.Child = grid;


            dayStack.Children.Add(border);
        }

        private void selectDay(DateTime date)
        {
            currentDayIndex = date;
            UpdateDayBorders();
        }

        private void goLeftDay(object sender, RoutedEventArgs e)
        {
            currentDayIndex = currentDayIndex.AddDays(-1);
            UpdateDayBorders();
        }

        private void goRightDay(object sender, RoutedEventArgs e)
        {
            currentDayIndex = currentDayIndex.AddDays(1);
            UpdateDayBorders();
        }
        PatientService patientService = new PatientService();
        private async void UpdateDayBorders()
        {

            List<Patient> patients = await patientService.GetPatientsByDoctorAsync(Doctor.UserID);

            if (allPanelCB.IsChecked == true && (currentDayIndex.DayOfYear != DateTime.Now.DayOfYear))
            {
                numberOfDoctorsTB.Text = patients.Count.ToString();
                if (patients.Count == 0)
                {
                    articlesTitleTB.Text = "General Articles";
                }
                else
                {
                    articlesTitleTB.Text = $"Articles Related To {currentDayIndex.ToString("M")}' Patients";
                }
            }
            else
            {
                articlesTitleTB.Text = "General Articles";
            }


            if (currentDayIndex.DayOfYear == DateTime.Now.DayOfYear)
            {
                selectedDayTB.Text = currentDayIndex.ToString("M") + ", Today";
            }
            else
            {
                selectedDayTB.Text = currentDayIndex.ToString("D");
            }
            if (dayStack != null)
                dayStack.Children.Clear();

            //DateTime time = currentDayIndex;
            for (int i = -2; i <= 2; i++)
            {
                createDayUI(currentDayIndex.AddDays(i));
            }
            await updateDayAppointments();

        }

        private void allPanelCBCheck(object sender, RoutedEventArgs e)
        {
            UpdateDayBorders();
        }



        private void dpDateChanged(object sender, SelectionChangedEventArgs e)
        {
            currentDayIndex = (DateTime)dp.SelectedDate;
            UpdateDayBorders();
        }

        private void newPatient(object sender, MouseButtonEventArgs e)
        {
            newPatientForm window = new newPatientForm(0);
            window.Show();
        }

        private void viewReciptionistProfile(object sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        /// article section
        /// </summary>

        //news-medical.net
        private async void PopulateArticleDataGrid()
        {
            articlesStackPanel.Children.Clear();

            string baseUrl = "https://www.news-medical.net/medical/search?q=Physiotherapy&t=health&page=1";

            List<Article> allArticles = await GetArticlesAsync(baseUrl);
            foreach (var i in allArticles)
            {
                articlesStackPanel.Children.Add(globals.createArticleUIObject(i));
            }
        }

        private async Task<List<Article>> GetArticlesAsync(string url)
        {
            List<Article> articles = new List<Article>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string htmlContent = await response.Content.ReadAsStringAsync();
                        HtmlDocument htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(htmlContent);

                        var resultNodes = htmlDocument.DocumentNode.SelectNodes("//li[@class='result']");

                        if (resultNodes != null)
                        {
                            foreach (var resultNode in resultNodes)
                            {
                                var titleNode = resultNode.SelectSingleNode(".//div[@class='resultTitle']/a");
                                var contentTypeNode = resultNode.SelectSingleNode(".//div[@class='resultContentTypeDate']/span[@class='contentType']");
                                var dateNode = resultNode.SelectSingleNode(".//div[@class='resultContentTypeDate']");
                                var snippetNode = resultNode.SelectSingleNode(".//div[@class='snippet']");

                                if (titleNode != null && contentTypeNode != null && dateNode != null && snippetNode != null)
                                {
                                    string title = titleNode.InnerText.Trim();
                                    string contentType = contentTypeNode.InnerText.Trim();
                                    string date = dateNode.InnerText.Trim().Replace(contentType, "").Trim();
                                    string snippet = snippetNode.InnerText.Trim();
                                    string link = titleNode.GetAttributeValue("href", "");

                                    articles.Add(new Article
                                    {
                                        Title = title,
                                        ContentType = contentType,
                                        Date = date,
                                        Snippet = snippet,
                                        Link = link
                                    });
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No articles found on the page.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return articles;
        }



        private async void articleSearchButton_Click(object sender, RoutedEventArgs e)
        {

            string query = articleSearchTB.Text;
            search(query);
        }

        private async void search(string query)
        {
            articlesStackPanel.Children.Clear();
            if (!string.IsNullOrEmpty(query))
            {
                string url = $"https://www.news-medical.net/medical/search?q={query}";
                List<Article> articles = await GetArticlesAsync(url);
                foreach (var i in articles)
                {
                    articlesStackPanel.Children.Add(globals.createArticleUIObject(i));
                }
            }
            else PopulateArticleDataGrid();
        }


        

        private void ontologySearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = ontologySearchTB.Text;
            _ = searchOntologyAsync(query);
        }
        SNOMEDOntologyService sNOMEDOntologyService = new SNOMEDOntologyService();
        private async Task searchOntologyAsync(string query)
        {
            ontologiesStackPanel.Children.Clear();
            List<SNOMEDOntology> terms = await sNOMEDOntologyService.SearchSNOMEDOntologyAsync(query);
            foreach (var i in terms)
            {
                ontologiesStackPanel.Children.Add(globals.CreateOntologyUIObject(i));
            }

        }
    }

    public class Article
    {
        public string Title { get; set; }
        public string ContentType { get; set; }
        public string Date { get; set; }
        public string Snippet { get; set; }
        public string Link { get; set; }
    }

}

