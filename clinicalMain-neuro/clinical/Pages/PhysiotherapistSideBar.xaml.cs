using HtmlAgilityPack;
using NeuroSpec.Shared.Models.DTO;
using NeuroSpec.Shared.Models.Ontology;
using NeuroSpec.Shared.Services.OntologyServices;
using NeuroSpecCompanion.Shared.Services.DTO_Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for DoctorSideBar.xaml
    /// </summary>
    public partial class DoctorSideBar : Page
    {
        public DoctorSideBar()
        {
            InitializeComponent();
            articlesBorder.Visibility = Visibility.Visible;
            hideAllBorders();
            
        }
        void hideAllBorders()
        {
            articlesBorder.Visibility = Visibility.Hidden;
            ontologyBorder.Visibility = Visibility.Hidden;
            dssBorder.Visibility = Visibility.Hidden;
            patientSearchBorder.Visibility = Visibility.Hidden;
            chatBotBorder.Visibility = Visibility.Hidden;
            recordSearchBorder.Visibility = Visibility.Hidden;
            globals.makeItLookClickable(articleButton);
            globals.makeItLookClickable(ontologyButton);
            //globals.makeItLookClickable(dssButton);
            globals.makeItLookClickable(patientSearchButton);
            //globals.makeItLookClickable(chatBotButton);
            globals.makeItLookClickable(recordSearchButton);

        }
        private void ontologyButtonClick(object sender, MouseButtonEventArgs e)
        {
            hideAllBorders();
            ontologyBorder.Visibility = Visibility.Visible;


        }

        private void articleButtonClick(object sender, MouseButtonEventArgs e)
        {
            hideAllBorders();

            articlesBorder.Visibility = Visibility.Visible;
            PopulateArticleDataGrid();


        }

        private void dssButtonClick(object sender, MouseButtonEventArgs e)
        {
            hideAllBorders();

            dssBorder.Visibility = Visibility.Visible;
            
            
        }

        private void patientSearchButtonClick(object sender, MouseButtonEventArgs e)
        {
            hideAllBorders();
            
            patientSearchBorder.Visibility = Visibility.Visible;
            initPatientSearch();

        }

        private void chatBotButtonClick(object sender, MouseButtonEventArgs e)
        {
            hideAllBorders();
            
            chatBotBorder.Visibility = Visibility.Visible;

        }

        private void recordSearchButtonClick(object sender, MouseButtonEventArgs e)
        {
            hideAllBorders();
            
            recordSearchBorder.Visibility = Visibility.Visible;
            initRecordSearch();

        }

        //patient search
        private ICollectionView PatientDataView;
        PatientService patientService = new PatientService();
        void initPatientSearch()
        {
            patientsDataGrid.ItemsSource = patientService.GetPatientsByDoctorAsync(globals.signedIn.UserID).Result;
        

            PatientDataView = CollectionViewSource.GetDefaultView(patientsDataGrid.ItemsSource);
            textBoxFilter.TextChanged += SearchTextBox_TextChanged;
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PatientDataView.Filter = item => FilterItem(item, textBoxFilter.Text);
        }
        private bool FilterItem(object item, string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return true; // No filter, show all items
            }

            foreach (var property in item.GetType().GetProperties())
            {
                var cellValue = property.GetValue(item);
                if (cellValue != null && cellValue.ToString().Contains(filterText, StringComparison.OrdinalIgnoreCase))
                {
                    return true; // Found a match in any column
                }
            }

            return false; // No match found
        }
        private void viewPatient(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient)patientsDataGrid.SelectedItem;
            if (selectedPatient != null)
            {
                // Open the PatientDetailsWindow and pass the selected patient
                patientView detailsWindow = new patientView(selectedPatient);
                detailsWindow.Show();
            }
        }


        //articles
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
            searchOntology(query);
        }
        SNOMEDOntologyService sNOMEDOntologyService = new SNOMEDOntologyService();
        private void searchOntology(string query)
        {
            ontologiesStackPanel.Children.Clear();
            List<SNOMEDOntology> terms = sNOMEDOntologyService.SearchSNOMEDOntologyAsync(query).Result;
            foreach (var i in terms)
            {
                ontologiesStackPanel.Children.Add(globals.CreateOntologyUIObject(i));
            }

        }


        //records
        ICollectionView RecordsDataView;
        MedicalRecordService medicalRecordService = new MedicalRecordService();
        void initRecordSearch()
        {
            List<Patient> patients = patientService.GetPatientsByDoctorAsync(globals.signedIn.UserID).Result;
            List<MedicalRecord> records = new List<MedicalRecord>();
            foreach (var i in patients)
            {
                records.AddRange(medicalRecordService.GetAllPatientRecordsAsync(i.PatientID).Result);
            }
            recordsDataGrid.ItemsSource = records;


            RecordsDataView = CollectionViewSource.GetDefaultView(recordsDataGrid.ItemsSource);
            recordTextBoxFilter.TextChanged += RecordsSearchTextBox_TextChanged;
        }
        private void RecordsSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RecordsDataView.Filter = item => FilterItem(item, recordTextBoxFilter.Text);
        }
       
        
        private void viewRecord(object sender, RoutedEventArgs e)
        {
            MedicalRecord record = (MedicalRecord)recordsDataGrid.SelectedItem;
            if (record != null)
            {
                patientView patientView = new patientView(record);
                patientView.Show();
            }
        }



        //dss
        void initDSS()
        {
            if (globals.selectedPatient == null)
            {
                MessageBox.Show("Please Navigate to a patient profile first");
                return;
            }


        }
    }
}
