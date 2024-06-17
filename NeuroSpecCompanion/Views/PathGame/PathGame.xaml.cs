using Microsoft.Maui.Controls;
using System;
using System.Timers;
using Microsoft.Maui.Graphics;

namespace NeuroSpecCompanion.Views
{
    public partial class PathGame : ContentPage
    {
        private System.Timers.Timer _timer;
        private int _seconds;
        private int _score;
        private bool _isDragging;
        private double _startX, _startY;
        private bool _isGameRunning;

        public PathGame()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            _score = 0;
            _seconds = 0;
            _isDragging = false;
            _isGameRunning = false;
            TimerLabel.Text = "Time: 00:00";
            ScoreLabel.Text = "Score: 0";

            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _seconds++;
            Device.BeginInvokeOnMainThread(() =>
            {
                TimerLabel.Text = $"Time: {TimeSpan.FromSeconds(_seconds):mm\\:ss}";
            });
        }

        private void StartStopClicked(object sender, EventArgs e)
        {
            if (!_isGameRunning)
            {
                _timer.Start();
                _isGameRunning = true;
                StartStopBtn.Source = "circle_pause"; // Change to pause icon if needed
            }
            else
            {
                _timer.Stop();
                _isGameRunning = false;
                StartStopBtn.Source = "circle_play"; // Change back to play icon if needed
            }
        }
        // Define TouchActionType enum within the class scope
        public enum TouchActionType
        {
            Entered,
            Pressed,
            Moved,
            Released,
            Exited,
            Cancelled
        }

        public class TouchActionEventArgs : EventArgs
        {
            public long Id { private set; get; }
            public TouchActionType Type { private set; get; }
            public Point Location { private set; get; }
            public bool IsInContact { private set; get; }

            public TouchActionEventArgs(long id, TouchActionType type, Point location, bool isInContact)
            {
                Id = id;
                Type = type;
                Location = location;
                IsInContact = isInContact;
            }
        }
        private void OnDraggableObjectTouch(object sender, TouchActionEventArgs e)
        {
            switch (e.Type)
            {
                case TouchActionType.Pressed:
                    _isDragging = true;
                    _startX = e.Location.X;
                    _startY = e.Location.Y;
                    break;

                case TouchActionType.Moved:
                    if (_isDragging)
                    {
                        double translationX = e.Location.X - _startX;
                        double translationY = e.Location.Y - _startY;

                        double newX = DraggableObject.TranslationX + translationX;
                        double newY = DraggableObject.TranslationY + translationY;

                        if (IsWithinPathBounds(newX, newY))
                        {
                            DraggableObject.TranslationX = newX;
                            DraggableObject.TranslationY = newY;
                            _startX = e.Location.X;
                            _startY = e.Location.Y;

                            if (IsAtEndPoint(newX, newY))
                            {
                                _score++;
                                ScoreLabel.Text = $"Score: {_score}";
                                ResetDraggableObject();
                            }
                        }
                        else
                        {
                            _isDragging = false;
                            ResetDraggableObject();
                        }
                    }
                    break;

                case TouchActionType.Released:
                    _isDragging = false;
                    break;
            }
        }

        private bool IsWithinPathBounds(double x, double y)
        {
            double pathLeft = Path.X;
            double pathTop = Path.Y;
            double pathRight = pathLeft + Path.Width;
            double pathBottom = pathTop + Path.Height;

            return x >= pathLeft && x <= pathRight && y >= pathTop && y <= pathBottom;
        }

        private bool IsAtEndPoint(double x, double y)
        {
            double endPointLeft = EndPoint.X;
            double endPointTop = EndPoint.Y;
            double endPointRight = endPointLeft + EndPoint.Width;
            double endPointBottom = endPointTop + EndPoint.Height;

            return x >= endPointLeft && x <= endPointRight && y >= endPointTop && y <= endPointBottom;
        }

        private void ResetDraggableObject()
        {
            DraggableObject.TranslationX = 0;
            DraggableObject.TranslationY = 0;
        }
    }
}
