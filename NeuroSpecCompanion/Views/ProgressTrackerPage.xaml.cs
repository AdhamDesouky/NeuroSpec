using Microsoft.Maui.Controls;
using SkiaSharp;
using System.Collections.Generic;
using Microcharts;

namespace NeuroSpecCompanion.Views
{
    public partial class ProgressTrackerPage : ContentPage
    {
        public ProgressTrackerPage()
        {
            InitializeComponent();

            progress1Chart.Chart = CreatePieChart(70, 30);
            progress2Chart.Chart = CreatePieChart(50, 50);
            progress3Chart.Chart = CreatePieChart(80, 20);
            progress4Chart.Chart = CreatePieChart(90, 10);
        }

        private PieChart CreatePieChart(float completed, float remaining)
        {
            return new PieChart
            {
                Entries = new List<ChartEntry>
                {
                    new ChartEntry(completed)
                    {
                        Color = SKColor.Parse("#00ff00"),
                        Label = "Completed",
                        ValueLabel = $"{completed}%"
                    },
                    new ChartEntry(remaining)
                    {
                        Color = SKColor.Parse("#ff0000"),
                        Label = "Remaining",
                        ValueLabel = $"{remaining}%"
                    }
                },
                BackgroundColor = SKColors.Transparent
            };
        }
    }
}
