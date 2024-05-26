
using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Drawing;
using static NeuroSpecCompanion.globals;

namespace NeuroSpecCompanion.Views.TapTest;

partial class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool Correct { get; set; }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}
public partial class TapGame : ContentPage
{
    bool started=false;
	int timerDuration = 15;
    private Stopwatch _stopwatch;
    private int _correctClicks,_totalClicks;
    private Random _random = new Random();
    Dictionary<DateTime, Point> _points = new Dictionary<DateTime, Point>();
    private List<double>misses = new List<double>();

    public TapGame()
	{
		InitializeComponent();
        timerLabel.Text = "00:"+timerDuration.ToString();
        TapGestureRecognizer tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += (sender, e) =>
        {
            Microsoft.Maui.Graphics.Point tapPoint = (Microsoft.Maui.Graphics.Point)e.GetPosition(GameArea);
            var xCoordinate = tapPoint.X;
            var yCoordinate = tapPoint.Y;
            _totalClicks++; 
            Console.WriteLine($"Clicked at ({xCoordinate}, {yCoordinate})");
            Point tap = new Point((int)xCoordinate, (int)yCoordinate);
            tap.Correct = false;
            _points.Add(DateTime.Now, tap);
            misses.Add(DistanceOfMisclick(tap, _currentPoint));
            GameArea.Children.Clear();
            ShowPoint();
        };
        GameArea.GestureRecognizers.Add(tapGesture);

    }
    private Point _currentPoint;

    private void ShowPoint()
    {
        if (!started)
        {
            return;
        }
        _currentPoint = new Point(_random.Next(20, 300), _random.Next(20, 500));
        Button pointLabel = new Button
        {
            Text = "•",
            FontSize = 28,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        AbsoluteLayout.SetLayoutBounds(pointLabel, new Rect(_currentPoint.X, _currentPoint.Y, 30, 30));
        GameArea.Children.Add(pointLabel);

        pointLabel.Clicked += (sender, e) =>
        {
            pointLabel.IsVisible = false;
            LogClick(_currentPoint, true, DateTime.Now);
            _totalClicks++;
            _correctClicks++;
            _currentPoint.Correct = true;
            _points.Add(DateTime.Now, _currentPoint);
            ShowPoint();
        };
    }
    private bool TimerTick()
    {
        if (_stopwatch.ElapsedMilliseconds >= timerDuration*1000)
        {
            GameOver();
            return false;
        }
        int remainingTime = timerDuration - (int)_stopwatch.ElapsedMilliseconds / 1000;
        timerLabel.Text = FormatTimeMinSeconds(remainingTime/60,remainingTime%60);
        return true;
    }
    private void startClicked(object sender, EventArgs e)
    {
        started = true;
        _stopwatch = Stopwatch.StartNew();
        _points.Clear();
        _correctClicks = 0;
        _totalClicks = 0;
        timerLabel.Text = FormatTimeMinSeconds(timerDuration/60,timerDuration%60);
        Device.StartTimer(TimeSpan.FromSeconds(1), TimerTick);
        ShowPoint();
    }
      
    private void LogClick(Point point, bool correct, DateTime timeStamp)
    {
        // Log click to backend
        Console.WriteLine($"Click at {point.X}, {point.Y} {(correct ? "Correct" : "Incorrect")}, {timeStamp.Minute}:{timeStamp.Second}.{timeStamp.Millisecond}");
    }
    private void GameOver()
    {
        started = false;
        _stopwatch.Stop();
        GameArea.Children.Clear();
        viewResults();
        DisplayAlert("Game Over", "Game Over", "OK");
    }
    private void viewResults()
    {
        infoLabel.Text = $"Correct Clicks: {_correctClicks}\n";
        infoLabel.Text += $"Total Clicks: {_totalClicks}\n";
        foreach (var point in _points)
        {
            infoLabel.Text += $"Click at {point.Value.X}, {point.Value.Y} {(point.Value.Correct ? "Correct" : "Incorrect")}, {point.Key.Minute}:{point.Key.Second}.{point.Key.Millisecond}\n";
        }
        foreach (var miss in misses)
        {
            infoLabel.Text += $"Missed by {miss}\n";
        }

    }

    private double DistanceOfMisclick(Point tap, Point point)
    {
        if(tap.X>point.X+30)
        {
            tap.X-=30;
        }
        if(tap.Y>point.Y+30)
        {
            tap.Y-=30;
        }
        double distance = Math.Sqrt(Math.Pow(tap.X - point.X, 2) + Math.Pow(tap.Y - point.Y, 2));
        return distance;
    }

}