﻿using clinical.BaseClasses;
using clinical.Pages;
using MahApps.Metro.IconPacks;
using Org.BouncyCastle.Tls;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Packaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Xps.Packaging;
using System.Xml;
using PdfSharp.Pdf;

namespace clinical
{
    class globals
    {
        public globals()
        {
        }

        public static User signedIn = null;
        public static Patient selectedPatient = null;
        public static DateTime CalculateBirthdate(int age)
        {
            DateTime currentDate = DateTime.Now;

            int birthYear = currentDate.Year - age;

            DateTime birthdate = new DateTime(birthYear, currentDate.Month, currentDate.Day);

            return birthdate;
        }

        public static int generateNewRecordID(int patientID)
        {
            DateTime dateTime = DateTime.Now;
            string s = "";
            s += patientID.ToString().Substring(0, 4);
            s += dateTime.DayOfYear.ToString();

            return int.Parse(s);

        }

        public static int generateNewAttendanceRecordID(int userID)
        {
            DateTime dateTime = DateTime.Now;
            string s = "";
            s += userID.ToString().Substring(0, 2);
            s += dateTime.DayOfYear.ToString();
            s += dateTime.Year.ToString().Substring(2, 2);

            return int.Parse(s);

        }
        public static int generateNewCalendarEventID(int userID)
        {
            DateTime dateTime = DateTime.Now;
            string s = "";
            s += userID.ToString().Substring(0, 2);
            s += dateTime.DayOfYear.ToString();
            s += dateTime.Year.ToString().Substring(2, 2);

            return int.Parse(s);

        }
        public static int generateNewPatientID(string phoneNumber)
        {
            DateTime dateTime = DateTime.Now;
            string s = dateTime.DayOfYear.ToString();
            s += dateTime.Minute.ToString();
            s += phoneNumber.Substring(3, 3);
            return int.Parse(s);

        }
        public static int generateNewDoctorID(string nid)
        {
            DateTime dateTime = DateTime.Now;
            string s = "2";
            s += nid[10];
            s += nid[11];
            s += nid[12];
            s += nid[13];
            return Convert.ToInt32(s);

        }
        public static int generateNewEmployeeID(string nid)
        {
            DateTime dateTime = DateTime.Now;
            string s = "3";
            s += nid[10];
            s += nid[11];
            s += nid[12];
            s += nid[13];
            return Convert.ToInt32(s);

        }
        public static string generateNewAdminID()
        {
            string s = "1";
            Random rand = new Random();
            s += rand.Next(100).ToString();
            return s;

        }
        public static int generateNewVisitID(int patID, DateTime time)
        {
            string s = patID.ToString().Substring(0, 3) + time.DayOfYear + time.Hour;
            return Convert.ToInt32(s);
        }
        public static int generateNewPaymentID(int patID, DateTime time)
        {
            string s = patID.ToString().Substring(2, 2) + new Random().Next(99).ToString() + time.DayOfYear.ToString() + time.Hour.ToString();
            return Convert.ToInt32(s);
        }
        public static int generateNewPrescriptionID(int visitID, DateTime time)
        {
            string s = visitID.ToString().Substring(0, 2) + time.Day + time.Minute + time.Second;
            return Convert.ToInt32(s);
        }
        public static int generateNewIssueExerciseID(int prescriptionID, int patientID)
        {
            string s = prescriptionID.ToString().Substring(0, 2) + new Random().Next(99).ToString() + patientID.ToString().Substring(0, 2) + new Random().Next(99).ToString();
            return Convert.ToInt32(s);
        }
        public static int generateNewTestFeedBackID(int visitID, int patientID)
        {
            string s = visitID.ToString().Substring(0, 2) + new Random().Next(99).ToString() + patientID.ToString().Substring(0, 2) + new Random().Next(99).ToString();
            return Convert.ToInt32(s);
        }
        public static int generateNewExerciseID()
        {
            string s = new Random().Next(99).ToString() + new Random().Next(99).ToString() + new Random().Next(81).ToString();
            return Convert.ToInt32(s);
        }
        public static int generateNewChatRoomID(int fID, int sID)
        {

            string s = fID.ToString().Substring(0, 3) + sID.ToString().Substring(0, 3);
            return Convert.ToInt32(s);
        }
        public static int generateNewChatMessageID(int senderID)
        {

            string s = senderID.ToString().Substring(0, 1) + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            return Convert.ToInt32(s);
        }


        public static void updateCalendarWithVisits(int DoctorID)
        {

        }



        ///////////////////////////////////////////
        /// creating ui
        //////////////////////////////////////////

        public static void makeItLookClickable(Border border)
        {
            Brush original = border.Background;
            border.MouseEnter += (sender, e) => border.Cursor = Cursors.Hand;
            border.MouseLeave += (sender, e) => border.Cursor = Cursors.Arrow;
            border.MouseLeave += (sender, e) => border.Background = original;
            border.MouseEnter += (sender, e) => border.Background = (Brush)Application.Current.FindResource("lighterColor");
        }
        public static void makeItLookClickableReverseColors(Border border)
        {
            Brush original=border.Background;
            border.MouseEnter += (sender, e) => border.Cursor = Cursors.Hand;
            border.MouseLeave += (sender, e) => border.Cursor = Cursors.Arrow;
            border.MouseLeave += (sender, e) => border.Background = original;
            border.MouseEnter += (sender, e) => border.Background = (Brush)Application.Current.FindResource("darkerColor");
        }
        public static void makeTextBlockLookLikeHyperLink(TextBlock tb)
        {
            tb.MouseEnter += (sender, e) => tb.Cursor = Cursors.Hand;
            tb.MouseLeave += (sender, e) => tb.Cursor = Cursors.Arrow;
            tb.MouseEnter += (sender, e) => tb.TextDecorations = TextDecorations.Underline;
            tb.MouseLeave += (sender, e) => tb.TextDecorations = null;
        }
        public static Border CreateOntologyUIObject(OntologyTerm oi)
        {
            Border border = new Border
            {
                Style = (Style)Application.Current.FindResource("theLinedBorder"),
                Margin = new Thickness(0, 0, 0, 5)
            };

            Grid grid = new Grid
            {
                Margin = new Thickness(5)
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            TextBlock DOID = new TextBlock
            {
                Text = $"{oi.Id}",
                Margin = new Thickness(5, 0, 5, 0),
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.FindResource("lightFontColor"),
                FontSize = 12
            };
            makeTextBlockLookLikeHyperLink(DOID);
            TextBlock ontologyTitle = new TextBlock
            {
                Text = $"{oi.Name}",
                Margin = new Thickness(5, 3, 5, 0),
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)Application.Current.FindResource("lightFontColor"),
                FontSize = 14,
            };
            //makeTextBlockLookLikeHyperLink(on)
            TextBlock ontologyDef = new TextBlock
            {
                Text = $"{oi.Def}",
                Margin = new Thickness(5, 2, 5, 0),
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.FindResource("lightFontColor"),
                FontSize = 12,
            };

            TextBlock ontologySynonyms = new TextBlock
            {
                Margin = new Thickness(5, 5, 5, 0),
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.FindResource("lightFontColor"),
                FontSize = 12,
            };
            if (oi.Synonyms != null && oi.Synonyms.Count > 0)
            {
                ontologySynonyms.Text = $"Synonyms: {oi.Synonyms},\n Parent: {oi.Parent}";

            }
            else if (oi.Parent != null && oi.Parent != "")
            {
                ontologySynonyms.Text = $"Parent: {oi.Parent}";
            }


            Grid.SetRow(DOID, 0);
            Grid.SetRow(ontologyTitle, 1);
            Grid.SetRow(ontologyDef, 2);
            Grid.SetRow(ontologySynonyms, 3);

            grid.Children.Add(DOID);
            grid.Children.Add(ontologyTitle);
            grid.Children.Add(ontologyDef);
            grid.Children.Add(ontologySynonyms);
            DOID.MouseDown += (sender, e) => ViewOntologyTermByDOID(oi);
            border.Child = grid;
            if (oi.Urls != null && oi.Urls.Count > 0)
            {
                makeItLookClickable(border);
                border.MouseDown += (sender, e) => ViewOntologyTerm(oi);
            }
            return border;
        }

        private static void ViewOntologyTermByDOID(OntologyTerm oi)
        {
            oi.Id=oi.Id.Replace(":", "_");
            //string s1 = $"https://disease-ontology.org/?id={oi.Id}";
            string s2= $"https://www.ebi.ac.uk/ols/ontologies/doid/terms?iri=http%3A%2F%2Fpurl.obolibrary.org%2Fobo%2F{oi.Id}";
            //Process.Start(new ProcessStartInfo(s1) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(s2) { UseShellExecute = true });

        }

        private static void ViewOntologyTerm(OntologyTerm oi)
        {
            string s = oi.Urls[0];
            s=s.Replace("http\\:", "http:").Replace("https\\:", "https:");
            Process.Start(new ProcessStartInfo(s) { UseShellExecute = true });

        }


        public static Border CreatePrescriptionUI(Prescription prescription)
        {
            Border borderedGrid = new Border
            {
                Style = (Style)Application.Current.Resources["theLinedBorder"],
                Margin = new Thickness(5, 5, 5, 0)
            };

            Grid grid = new Grid();

            for (int i = 0; i < 4; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(72) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            TextBlock DoctorNameTextBlock = CreatePrescribedTextBlock("Doctor Name:", 0, 0, 5, 5);
            TextBlock timestampTextBlock = CreatePrescribedTextBlock("Timestamp:", 1, 0, 5, 5);
            TextBlock prescribedTextBlock = CreatePrescribedTextBlock("Prescribed:", 2, 0, 7.5, 5);
            string physName = DB.GetUserById(prescription.UserID).FullName;

            TextBlock DoctorNameTB = CreatePrescribedTextBlock($"Dr. {physName}", 0, 1, 0, 5);
            TextBlock timestampTB = CreatePrescribedTextBlock(prescription.TimeStamp.ToString("g"), 1, 1, 0, 5);

            StackPanel stackPanel = new StackPanel
            {
                Margin = new Thickness(5)
            };
            List<IssueScan> issues = DB.GetAllIssueScansByPrescriptionID(prescription.PrescriptionID);
            foreach (var i in issues)
            {
                TextBlock prescriptionItem = CreatePrescribedTextBlock($"-{DB.GetScanTestById(i.ScanTestID).ScanTestName}, {i.Notes}", 2, 1, 2.5, 0);
                stackPanel.Children.Add(prescriptionItem);
            }



            Grid.SetRow(stackPanel, 2);
            Grid.SetColumn(stackPanel, 1);
            Grid.SetRow(DoctorNameTextBlock, 0);
            Grid.SetColumn(DoctorNameTextBlock, 0);
            Grid.SetRow(timestampTextBlock, 1);
            Grid.SetColumn(timestampTextBlock, 0);
            Grid.SetRow(prescribedTextBlock, 2);
            Grid.SetColumn(prescribedTextBlock, 0);
            Grid.SetRow(DoctorNameTB, 0);
            Grid.SetColumn(DoctorNameTB, 1);
            Grid.SetRow(timestampTB, 1);
            Grid.SetColumn(timestampTB, 1);
            Grid.SetRow(stackPanel, 2);
            Grid.SetColumn(stackPanel, 1);


            if (!signedIn.isReciptionist)
            {
                Button viewPrescriptionBTN = new Button
                {
                    Content = "View Prescription",
                    Margin = new Thickness(5),
                    Background = (Brush)Application.Current.Resources["selectedColor"],
                    Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                    BorderBrush = (Brush)Application.Current.Resources["lightFontColor"],
                    BorderThickness = new Thickness(2),
                    Padding = new Thickness(5),
                };
                viewPrescriptionBTN.Click += (sender, e) => viewPrescription(prescription);

                Grid.SetRow(viewPrescriptionBTN, 3);
                Grid.SetColumnSpan(viewPrescriptionBTN, 2);
                grid.Children.Add(viewPrescriptionBTN);
            }




            // Add UI elements to Grid
            grid.Children.Add(DoctorNameTextBlock);
            grid.Children.Add(timestampTextBlock);
            grid.Children.Add(prescribedTextBlock);
            grid.Children.Add(DoctorNameTB);
            grid.Children.Add(timestampTB);
            grid.Children.Add(stackPanel);

            // Add Grid to Border
            borderedGrid.Child = grid;

            return borderedGrid;
        }
        static void viewPrescription(Prescription prescription)
        {
            new prescriptionWindow(prescription).Show();
        }

        public static TextBlock CreatePrescribedTextBlock(string text, int row, int column, double topMargin, double leftMargin)
        {
            TextBlock textBlock = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Text = text,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                Margin = new Thickness(leftMargin, topMargin, 0, 5),
                FontSize = 12,
                FontWeight = FontWeights.SemiBold
            };

            Grid.SetRow(textBlock, row);
            Grid.SetColumn(textBlock, column);

            return textBlock;
        }


        public static Border createAppointmentUIObject(Visit visit, Action<Visit> viewVisit, Action<Patient> viewPatient)
        {
            if (visit == null) { return null; }

            Patient patient = DB.GetPatientById(visit.PatientID);
            User Doctor = DB.GetUserById(visit.DoctorID);
            if (patient == null) { return null; }
            Border border = new Border
            {
                Margin = new Thickness(10),
                BorderBrush = Brushes.AliceBlue,
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(10),
                Background = (Brush)Application.Current.Resources["lighterColor"],
                Height = 188
            };
            Grid grid = new Grid
            {
                Margin = new Thickness(10)
            };

            for (int i = 0; i < 5; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            TextBlock visitTime = new TextBlock
            {
                Text = visit.TimeStamp.Date.ToString("D") + ", " + visit.TimeStamp.ToString("HH:mm tt"),
                Margin = new Thickness(10, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 16
            };

            TextBlock patientName = new TextBlock
            {
                Text = $"{patient.FirstName} {patient.LastName}",
                Margin = new Thickness(10, 5, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 20
            };

            StackPanel visitTypePanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10, 0, 0, 0)
            };

            TextBlock visitType = new TextBlock
            {
                Text = $"{visit.Type} with ",
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 14
            };

            TextBlock DoctorName = new TextBlock
            {
                Text = $"DR. {Doctor.FirstName} {Doctor.LastName}",
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 14
            };

            visitTypePanel.Children.Add(visitType);
            visitTypePanel.Children.Add(DoctorName);

            StackPanel phonePanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10, 10, 0, 0)
            };

            PackIconMaterial phoneIcon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.Phone,
                Margin = new Thickness(0),
                Foreground = (Brush)Application.Current.Resources["lightFontColor"]
            };

            TextBlock patientPhone = new TextBlock
            {
                Text = patient.PhoneNumber,
                Margin = new Thickness(5, 0, 5, 0),
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 14
            };

            phonePanel.Children.Add(phoneIcon);
            phonePanel.Children.Add(patientPhone);

            StackPanel buttonsPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10, 5, 0, 0)
            };

            Border patientProfileButton = new Border
            {
                Style = (Style)Application.Current.Resources["theBorder"],
                BorderBrush = (Brush)Application.Current.Resources["lightFontColor"],
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(0),
                Background = (Brush)Application.Current.Resources["darkerColor"]
            };

            TextBlock patientProfileText = new TextBlock
            {
                Text = "Patient Profile",
                TextWrapping = TextWrapping.Wrap,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(10, 5, 10, 5)
            };
            makeItLookClickable(patientProfileButton);

            Border viewVisitButton = new Border
            {
                Name = "viewVisitBTN",
                Style = (Style)Application.Current.Resources["theBorder"],
                BorderBrush = (Brush)Application.Current.Resources["lightFontColor"],
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(5, 0, 5, 0),
                Background = (Brush)Application.Current.Resources["darkerColor"]
            };

            TextBlock viewVisitText = new TextBlock
            {
                Text = "Appointment Details",
                TextWrapping = TextWrapping.Wrap,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.Bold
            };
            viewVisitButton.Child = viewVisitText;
            makeItLookClickable(viewVisitButton);

            patientProfileButton.MouseDown += (sender, e) => viewPatient(patient);
            viewVisitButton.MouseDown += (sender, e) => viewVisit(visit); ;
            buttonsPanel.Children.Add(viewVisitButton);

            patientProfileButton.Child = patientProfileText;


            buttonsPanel.Children.Add(patientProfileButton);

            Grid.SetRow(visitTime, 0);
            Grid.SetRow(patientName, 1);
            Grid.SetRow(visitTypePanel, 2);
            Grid.SetRow(phonePanel, 3);
            Grid.SetRow(buttonsPanel, 4);


            grid.Children.Add(visitTime);
            grid.Children.Add(patientName);
            grid.Children.Add(visitTypePanel);
            grid.Children.Add(phonePanel);
            grid.Children.Add(buttonsPanel);

            border.Child = grid;

            return border;
        }


        public static Border createAppointmentUIObject(Visit visit, Action<Visit> viewVisit)
        {
            if (visit == null) { return null; }

            Patient patient = DB.GetPatientById(visit.PatientID);
            User Doctor = DB.GetUserById(visit.DoctorID);
            if (patient == null) { return null; }
            Border border = new Border
            {
                Margin = new Thickness(10),
                BorderBrush = Brushes.AliceBlue,
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(10),
                Background = (Brush)Application.Current.Resources["lighterColor"],
                Height = 188
            };
            Grid grid = new Grid
            {
                Margin = new Thickness(10)
            };

            for (int i = 0; i < 5; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            TextBlock visitTime = new TextBlock
            {
                Text = visit.TimeStamp.Date.ToString("D") + ", " + visit.TimeStamp.ToString("HH:mm tt"),
                Margin = new Thickness(10, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 16
            };

            TextBlock patientName = new TextBlock
            {
                Text = $"{patient.FirstName} {patient.LastName}",
                Margin = new Thickness(10, 5, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 20
            };

            StackPanel visitTypePanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10, 0, 0, 0)
            };

            TextBlock visitType = new TextBlock
            {
                Text = $"{visit.Type} with ",
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 14
            };

            TextBlock DoctorName = new TextBlock
            {
                Text = $"DR. {Doctor.FirstName} {Doctor.LastName}",
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 14
            };

            visitTypePanel.Children.Add(visitType);
            visitTypePanel.Children.Add(DoctorName);

            StackPanel phonePanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10, 10, 0, 0)
            };

            PackIconMaterial phoneIcon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.Phone,
                Margin = new Thickness(0),
                Foreground = (Brush)Application.Current.Resources["lightFontColor"]
            };

            TextBlock patientPhone = new TextBlock
            {
                Text = patient.PhoneNumber,
                Margin = new Thickness(5, 0, 5, 0),
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                FontSize = 14
            };

            phonePanel.Children.Add(phoneIcon);
            phonePanel.Children.Add(patientPhone);

            StackPanel buttonsPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10, 5, 0, 0)
            };



            Border viewVisitButton = new Border
            {
                Name = "viewVisitBTN",
                Style = (Style)Application.Current.Resources["theBorder"],
                BorderBrush = (Brush)Application.Current.Resources["lightFontColor"],
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(5, 0, 5, 0),
                Background = (Brush)Application.Current.Resources["darkerColor"]
            };

            TextBlock viewVisitText = new TextBlock
            {
                Text = "Appointment Details",
                TextWrapping = TextWrapping.Wrap,
                Foreground = (Brush)Application.Current.Resources["lightFontColor"],
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.Bold
            };
            makeItLookClickable(viewVisitButton);
            viewVisitButton.Child = viewVisitText;

            viewVisitButton.MouseDown += (sender, e) => viewVisit(visit); ;
            buttonsPanel.Children.Add(viewVisitButton);



            Grid.SetRow(visitTime, 0);
            Grid.SetRow(patientName, 1);
            Grid.SetRow(visitTypePanel, 2);
            Grid.SetRow(phonePanel, 3);
            Grid.SetRow(buttonsPanel, 4);


            grid.Children.Add(visitTime);
            grid.Children.Add(patientName);
            grid.Children.Add(visitTypePanel);
            grid.Children.Add(phonePanel);
            grid.Children.Add(buttonsPanel);

            border.Child = grid;

            return border;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static Border createArticleUIObject(Article article)
        {

            Border border = new Border
            {
                Style = (Style)Application.Current.FindResource("theLinedBorder"),
                Margin = new Thickness(0, 0, 0, 5)
            };

            Grid grid = new Grid
            {
                Margin = new Thickness(5)
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            TextBlock title = new TextBlock
            {
                Text = $"{article.Title}",
                Margin = new Thickness(5, 5, 5, 0),
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)Application.Current.FindResource("lightFontColor"),
                FontSize = 14
            };
            border.MouseLeftButtonDown += (sender, e) => viewArticle(article);
            //make when mouse enter the cursor changes to pointer
            makeItLookClickable(border);


            TextBlock typeAndDate = new TextBlock
            {
                Text = $"{article.ContentType} {article.Date}",
                Margin = new Thickness(5, 3, 5, 3),
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.FindResource("lightFontColor"),
                FontSize = 12
            };
            Grid.SetRow(typeAndDate, 1);

            TextBlock snippet = new TextBlock
            {
                Text = article.Snippet,
                Margin = new Thickness(5, 5, 5, 0),
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.SemiBold,
                Foreground = (Brush)Application.Current.FindResource("lightFontColor"),
                FontSize = 12,

            };
            Grid.SetRow(snippet, 2);

            grid.Children.Add(title);
            grid.Children.Add(typeAndDate);
            grid.Children.Add(snippet);

            border.Child = grid;

            return border;

        }
        static string mainUrl = "https://www.news-medical.net";

        static void viewArticle(Article article)
        {
            if (!string.IsNullOrEmpty(article.Link))
            {
                Process.Start(new ProcessStartInfo(mainUrl + article.Link) { UseShellExecute = true });
            }
        }


        //printing
        public static void PrintPage(Page page)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                // Set content properties
                page.Measure(new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));
                page.Arrange(new Rect(new Point(0, 0), page.DesiredSize));

                // Print the visual content of the Page directly
                printDialog.PrintVisual(page, "Print Document");
            }
        }


        //private static void SaveAsPdf(FixedDocument fixedDoc, string outputPath)
        //{
        //    using (FileStream fs = new FileStream(outputPath, FileMode.Create))
        //    {
        //        PdfDocument pdfDocument = new PdfDocument();
        //        pdfDocument.Info.Title = "Printed Document";

        //        // Iterate through each page of the FixedDocument and add it to the PdfDocument
        //        foreach (PageContent pageContent in fixedDoc.Pages)
        //        {
        //            var page = new PdfPage();
        //            var content = new PdfPageContent();
        //            var stream = new MemoryStream();

        //            var xpsDocument = new XpsDocument(pageContent.SourceDocument, FileAccess.Read);
        //            var package = System.IO.Packaging.Package.Open(stream, FileMode.Create, FileAccess.ReadWrite);
        //            var xpsPackage = (IXpsFixedDocumentSequenceReader)XpsDocument.OpenXpsDocumentReader(xpsDocument).FixedDocumentSequenceReader;

        //            var paginator = xpsPackage.FixedDocuments[0].Paginator;
        //            paginator.SaveAsXaml(XmlWriter.Create(package.GetPart(new Uri("/Documents/1/FixedDoc.fdoc", UriKind.Relative), CompressionOption.Maximum).GetStream(FileMode.Create, FileAccess.Write)));

        //            package.Close();
        //            stream.Seek(0, SeekOrigin.Begin);

        //            page.AddImage(XImage.FromStream(stream));
        //            pdfDocument.Pages.Add(page);
        //        }

        //        // Save the PdfDocument to the specified path
        //        pdfDocument.Save(fs);
        //    }
        //}


        ///////////////////////////////////////////////
        ///Scheduling part
        ///////////////////////////////////////////////
        ///


        public static void ScheduleVisit(Visit newVisit)
        {
            if (CanBookVisit(newVisit))
            { DB.InsertVisit(newVisit); }
            else
            {
                MessageBox.Show("Can't Book in this time slot, change slot or select first available slot.");
            }


        }

        static bool CanBookVisit(Visit visit)
        {
            // Generate time slots for the proposed date and time
            Visit fakeVisit = visit;
            List<DateTime> proposedSlots = GenerateTimeSlots(visit.TimeStamp, fakeVisit.TimeStamp.AddMinutes(DB.GetAppointmentTypeByID(visit.AppointmentTypeID).TimeInMinutes), TimeSpan.FromMinutes(DB.GetSlotDuration()));

            List<Visit> existingVisits = DB.GetFutureDoctorVisits(visit.DoctorID);


            // Check for conflicts with existing visits
            List<DateTime> unavailableSlots = existingVisits
                .SelectMany(visit => GenerateTimeSlots(visit.TimeStamp, visit.TimeStamp.AddMinutes(DB.GetAppointmentTypeByID(visit.AppointmentTypeID).TimeInMinutes), TimeSpan.FromMinutes(DB.GetSlotDuration())))
                .ToList();

            // Check if there are any common elements (conflicts)
            bool hasConflicts = proposedSlots.Intersect(unavailableSlots).Any();

            // If there are no conflicts, return true; otherwise, return false
            return !hasConflicts;
        }
        public static DateTime FindFirstFreeSlot(int DoctorID, DateTime when)
        {
            List<DateTime> availableSlots = FindAvailableTimeSlots(DoctorID, when);

            while (availableSlots.Count == 0)
            {
                when = when.AddDays(7);
                availableSlots = FindAvailableTimeSlots(DoctorID, when);
            }
            return availableSlots[0];
        }

        static List<DateTime> FindAvailableTimeSlots(int DoctorID, DateTime when)
        {
            List<DateTime> allSlots = GenerateAllPossibleTimeSlots(when);
            List<Visit> existingVisits = DB.GetFutureDoctorVisits(DoctorID);

            List<DateTime> unavailableSlots = existingVisits
                .SelectMany(visit => GenerateTimeSlots(visit.TimeStamp, visit.TimeStamp.AddMinutes(DB.GetAppointmentTypeByID(visit.AppointmentTypeID).TimeInMinutes), TimeSpan.FromMinutes(DB.GetSlotDuration())))
                .ToList();

            List<DateTime> availableSlots = allSlots.Except(unavailableSlots).ToList();

            return availableSlots;
        }

        public static List<string> GetAvailableTimeSlotsOnDay(DateTime selectedDay, int DoctorID)
        {
            // Generate time slots for the selected day
            DateTime dayStartTime = selectedDay.Date.AddHours(DB.GetOpeningTime());
            DateTime dayEndTime = selectedDay.Date.AddHours(DB.GetClosingTime());
            TimeSpan slotDuration = TimeSpan.FromMinutes(DB.GetSlotDuration());

            List<DateTime> allSlots = GenerateTimeSlots(dayStartTime, dayEndTime, slotDuration);
            List<Visit> existingVisits = DB.GetFutureDoctorVisits(DoctorID);

            // Check for conflicts with existing visits
            List<DateTime> unavailableSlots = existingVisits
                .Where(visit => visit.TimeStamp.Date == selectedDay.Date)
                .SelectMany(visit => GenerateTimeSlots(visit.TimeStamp, visit.TimeStamp.AddMinutes(DB.GetAppointmentTypeByID(visit.AppointmentTypeID).TimeInMinutes), TimeSpan.FromMinutes(DB.GetSlotDuration())))
                .ToList();

            // Find available slots by removing occupied slots
            List<DateTime> availableSlots = allSlots.Except(unavailableSlots).ToList();
            List<string> toReturn = new List<string>();
            foreach (var i in availableSlots)
            {
                toReturn.Add(i.ToString("HH:mm"));
            }
            return toReturn;
        }

        static List<DateTime> GenerateAllPossibleTimeSlots(DateTime when)
        {
            // Generate a list of time slots for multiple days
            DateTime startDate = when;
            DateTime endDate = when.AddDays(7); // Schedule for the next 7 days
            TimeSpan slotDuration = TimeSpan.FromMinutes(DB.GetSlotDuration());

            List<DateTime> allSlots = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                DateTime clinicStartTime = date.AddHours(DB.GetOpeningTime());
                DateTime clinicEndTime = date.AddHours(DB.GetClosingTime());

                for (DateTime time = clinicStartTime; time < clinicEndTime; time += slotDuration)
                {
                    allSlots.Add(time);
                }
            }

            return allSlots;
        }

        static List<DateTime> GenerateTimeSlots(DateTime startTime, DateTime endTime, TimeSpan slotDuration)
        {
            // Generate a list of time slots for a given time range
            List<DateTime> slots = new List<DateTime>();

            for (DateTime time = startTime; time < endTime; time += slotDuration)
            {
                slots.Add(time);
            }

            return slots;
        }


    }
}
