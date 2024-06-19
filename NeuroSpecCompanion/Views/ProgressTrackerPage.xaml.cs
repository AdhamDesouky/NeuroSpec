using Microsoft.Maui.Controls;
using SkiaSharp;
using System.Collections.Generic;
using Microcharts;
using Microcharts.Maui;

namespace NeuroSpecCompanion.Views
{
    public partial class ProgressTrackerPage : ContentPage
    {
        public ProgressTrackerPage()
        {
            InitializeComponent();

            // Create and set up the charts
            InitializeCharts();
        }

        private void InitializeCharts()
        {
            try
            {
                // Line Chart
                lineChart.Chart = CreateLineChart();

                // Donut Chart
                donutChart.Chart = CreateDonutChart();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Failed to load charts: " + ex.Message, "OK");
            }
        }

        private LineChart CreateLineChart()
        {
            var entries = new List<ChartEntry>
      {
        new ChartEntry(30)
        {
          Color = SKColor.Parse("#FFA07A"),
          Label = "Day 1",
          ValueLabel = "30"
        },
        new ChartEntry(40)
        {
          Color = SKColor.Parse("#ADD8E6"),
          Label = "Day 2",
          ValueLabel = "40"
        },
        new ChartEntry(50)
        {
          Color = SKColor.Parse("#98FB98"),
          Label = "Day 3",
          ValueLabel = "50"
        },
        new ChartEntry(45)
        {
          Color = SKColor.Parse("#FFB6C1"),
          Label = "Day 4",
          ValueLabel = "45"
        }
      };

            return new LineChart
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent
            };
        }
        private DonutChart CreateDonutChart()
        {
            var entries = new List<ChartEntry>
            {
                new ChartEntry(60)
                {
                    Color = SKColor.Parse("#FFA07A"),
                    Label = "Completed",
                    ValueLabel = "60"
                },
                new ChartEntry(40)
                {
                    Color = SKColor.Parse("#ADD8E6"),
                    Label = "Remaining",
                    ValueLabel = "40"
                }
            };

            return new DonutChart
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent
            };
        }
    }
}
