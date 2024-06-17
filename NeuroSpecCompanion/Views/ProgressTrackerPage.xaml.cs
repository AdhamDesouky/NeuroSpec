using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Views
{
    public partial class ProgressTrackerPage : ContentPage
    {
        public ProgressTrackerPage()
        {
            InitializeComponent();

            // Simulate progress updates
            UpdateProgressBars();
        }

        private void UpdateProgressBars()
        {
            // Simulated progress data
            Random random = new Random();
            double progress1 = random.NextDouble();
            double progress2 = random.NextDouble();
            double progress3 = random.NextDouble();
            double progress4 = random.NextDouble();

            // Update progress bar widths based on simulated data
            progress1Indicator.WidthRequest = progress1 * 300; // Multiply by max width you want
            progress2Indicator.WidthRequest = progress2 * 300;
            progress3Indicator.WidthRequest = progress3 * 300;
            progress4Indicator.WidthRequest = progress4 * 300;
        }
    }
}
