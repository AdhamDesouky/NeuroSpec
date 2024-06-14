namespace NeuroSpecCompanion.Views.VibrationTest
{
    public partial class VibrationTestGamePage : ContentPage
    {
        int _countdownTimerDuration = 5;
        int _timerDuration = 20;
        bool _started = false;
        Random _rnd = new Random();
        int _currentIntensity = 50;

        public VibrationTestGamePage()
        {
            InitializeComponent();
        }

        void StartStopBtn_Clicked(object sender, EventArgs e)
        {
            if (_started)
            {
                StopGame();
            }
            else
            {
                StartCountdownTimer();
            }
        }
        void StartCountdownTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (_countdownTimerDuration == 0)
                {
                    StartGame();
                    return false;
                }
                startStopBtn.IsEnabled = false;
                _countdownTimerDuration--;
                return true;
            });
        }

        void StartGame()
        {
            _started = true;
            _timerDuration = 20;
            StartVibration();
            StartTimer();
        }

        void StartTimer()
        {
            Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                timerLabel.Text = globals.FormatTimeMinSeconds(_timerDuration / 60, _timerDuration % 60);
                if (_timerDuration % 4 == 0)
                {
                    ChangeVibrationIntensity();
                }
                if (_timerDuration == 0)
                {
                    StopGame();
                    return false;
                }
                _timerDuration--;
                return true;
            });
        }

        void StartVibration()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                if (_started)
                {
                    Vibration.Vibrate(TimeSpan.FromMilliseconds(_currentIntensity));
                    return true; // Continue vibrating
                }
                else
                {
                    Vibration.Cancel();
                    return false; // Stop the timer
                }
            });
        }

        void ChangeVibrationIntensity()
        {
            _currentIntensity = _rnd.Next(0, 100);
        }

        void StopGame()
        {
            _started = false;
            startStopBtn.IsEnabled = true;
            Vibration.Cancel();
            ViewScore();
        }

        void ViewScore()
        {
            // Implement view score logic
        }

        private void OnScreenTapped(object sender, TappedEventArgs e)
        {
            ChangeVibrationIntensity();

        }

        private void startClicked(object sender, EventArgs e)
        {
            StartGame();
        }
    }
}
