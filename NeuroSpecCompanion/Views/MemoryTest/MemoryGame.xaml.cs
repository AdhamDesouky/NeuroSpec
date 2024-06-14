
namespace NeuroSpecCompanion.Views.MemoryTest;

partial class Pair
{
    public int imageIdx { get; set; }
    public Coordinates idx1 = new Coordinates() ;
    public Coordinates idx2 = new Coordinates();
    public bool matched { get; set; }
    public Pair(int img)
    {
        imageIdx = img;
    }
}
partial class Coordinates
{
    public int x { get; set; }
    public int y { get; set; }
    public Coordinates() { }
    public Coordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
partial class ScorePoint
{
    public DateTime timeStamp { get; set; }
    public Coordinates coordinates = new Coordinates();
    public bool correct { get; set; }
    public ScorePoint(DateTime time, Coordinates c, bool correct)
    {
        this.timeStamp = time;
        coordinates = c;
        this.correct = correct;
    
    }
}

public partial class MemoryGame : ContentPage
{
    int[] freq = { 0, 0, 0, 0, 0, 0 };
    bool _started = false;
    int _timerDuration = 7;
    private ImageButton[,] imgBTNs;
    private Frame[,] imgFrames;

    string[] allImages = { "bear.jpg", "fox.jpg", "bunny.jpg", "elephant.jpg", "lion.jpg", "parrot.jpg" };
    List<Pair> allCards = new List<Pair>();
    Pair onHold = null;


    /// <summary>
    /// Scoring system
    /// </summary>
    int _totalClicks = 0;
    int _correctClicks = 0;
    int _missedClicks = 0;
    List<ScorePoint>_score = new List<ScorePoint>();

    public MemoryGame()
    {
        InitializeComponent();
        timerLabel.Text = globals.FormatTimeMinSeconds(_timerDuration / 60, _timerDuration % 60);
    }
    void StartGame()
    {
        if (_started) { EndGame(); return; }
        _started = true;
        freq = new int[] { 0, 0, 0, 0, 0, 0 };
        CreateUI();
        StartTimer();

    }
    void UpdateScore()
    {
        string score = $"Score: Correct: {_correctClicks}, Missed: {_missedClicks}";
        scoreLabel.Text = score;

        if (_correctClicks==allCards.Count)
        {
            EndGame();
        }

    }
    void ViewScore()
    {
        string score = $"Total Clicks: {_totalClicks}\nCorrect Clicks: {_correctClicks}\nMissed Clicks: {_missedClicks}";
        foreach (ScorePoint p in _score)
        {
            score += $"\n{p.timeStamp} - ({p.coordinates.x}, {p.coordinates.y}) - {(p.correct ? "Correct" : "Incorrect")}";
        }
        DisplayAlert("Score", score, "OK");

    }
    void EndGame()
    {
        _started = false;
        startStopBtn.IsEnabled = false;
        ViewScore();
    }
    string randomizeImages(int r, int c)
    {
        Random rnd = new Random();
        int index = rnd.Next(0, 6);
        if (freq[index] == 2)
        {
            return randomizeImages(r, c);
        }
        freq[index]++;
        if (freq[index] == 1)
        {
            Pair pair1 = new Pair(index);
            pair1.idx1.x = r;
            pair1.idx1.y = c;
            pair1.matched = false;
            allCards.Add(pair1);
        }
        else
        {
            foreach (Pair p in allCards)
            {
                if (p.imageIdx == index)
                {
                    p.idx2.x = r;
                    p.idx2.y = c;
                }
            }
        }
        return allImages[index];
    }
    void StartTimer()
    {
        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            _timerDuration--;
            timerLabel.Text = globals.FormatTimeMinSeconds(_timerDuration / 60, _timerDuration % 60);
            if (_timerDuration == 0)
            {
                FlipCards();
                return false;
            }
            return true;
        });
    }
    void FlipCards()
    {
        foreach (Pair p in allCards)
        {
            ChangeImageButtonVisibility(p.idx1.x, p.idx1.y, false);
            ChangeImageButtonVisibility(p.idx2.x, p.idx2.y, false);
        }
    }
    void CreateUI()
    {
        imgBTNs = new ImageButton[3, 4];
        imgFrames = new Frame[3, 4];
        var screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        screenWidth -= 50;
        var width = screenWidth / 3;
        for (int i = 0; i < 4; i++)
        {

            var horizontalStackLayout = new HorizontalStackLayout
            {
                Margin = new Thickness(0, 10, 0, 0),
                Spacing = 15
            };

            for (int j = 0; j < 3; j++)
            {

                int currI = i;
                int currJ = j;
                var frame = new Frame
                {
                    HeightRequest = width,
                    WidthRequest = width,
                    CornerRadius = 10,
                };
                var imgBtn = new ImageButton
                {
                    Source = randomizeImages(j, i)
                };

                frame.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => ImageButtonClicked(currJ, currI))
                });
                frame.Content = imgBtn;

                horizontalStackLayout.Children.Add(frame);

                imgBTNs[currJ, currI] = imgBtn;
                imgFrames[currJ, currI] = frame;
            }

            verticalStackLayout.Children.Add(horizontalStackLayout);
        }

    }
    void ChangeBackgroundColor(int row, int column, Color color)
    {
        if (row >= 0 && row < imgFrames.GetLength(0) && column >= 0 && column < imgFrames.GetLength(1))
        {
            imgFrames[row, column].BackgroundColor = color;
        }
    }
    void ChangeImageButtonVisibility(int row, int column, bool isVisible)
    {
        if (row >= 0 && row < imgBTNs.GetLength(0) && column >= 0 && column < imgBTNs.GetLength(1))
        {

            imgBTNs[row, column].IsVisible = isVisible;
        }
    }
    void EnableImageButton(int row, int column, bool state)
    {
        if (row >= 0 && row < imgBTNs.GetLength(0) && column >= 0 && column < imgBTNs.GetLength(1))
        {
            imgBTNs[row, column].IsEnabled = state;
        }
    }

    async void ImageButtonClicked(int r, int c)
    {
        Console.WriteLine($"Clicked at ({r}, {c})");

        Pair found = null;
        for (int i = 0; i < allCards.Count; i++)
        {
            if (allCards[i].matched) continue;
            if ((allCards[i].idx1.x == r && allCards[i].idx1.y == c) || (allCards[i].idx2.x == r && allCards[i].idx2.y == c))
            {
                found = allCards[i];
                break;
            }
        }
        //first click
        if (onHold == null)
        {
            onHold = found;
            ChangeImageButtonVisibility(r, c, true);
            EnableImageButton(r, c, false);
        }
        //second click
        else
        {
            //correct match
            if (onHold.imageIdx == found.imageIdx)
            {
                CorrectMatch(r, c);
                onHold = null;

            }
            //incorrect match
            else
            {
                IncorrectMatch(r,c);
            }
        }
    }
    void CorrectMatch(int r, int c)
    {
        onHold.matched = true;
        ChangeImageButtonVisibility(r, c, true);
        EnableImageButton(r, c, false);
        ChangeBackgroundColor(onHold.idx1.x, onHold.idx1.y, Color.FromHex("#84bf68"));
        ChangeBackgroundColor(onHold.idx2.x, onHold.idx2.y, Color.FromHex("#84bf68"));

        //scoring
        _correctClicks++;
        _totalClicks++;
        _score.Add(new ScorePoint(DateTime.Now, new Coordinates(r, c), true));
        UpdateScore();
    }
    async void IncorrectMatch(int r, int c)
    {
        ChangeImageButtonVisibility(r, c, true);
        await Task.Delay(400);
        ChangeImageButtonVisibility(r, c, false);
        ChangeImageButtonVisibility(onHold.idx1.x, onHold.idx1.y, false);
        ChangeImageButtonVisibility(onHold.idx2.x, onHold.idx2.y, false);
        EnableImageButton(r, c, true);
        //scoring
        _missedClicks++;
        _totalClicks++;
        _score.Add(new ScorePoint(DateTime.Now, new Coordinates(r, c), false));
        UpdateScore();
        onHold = null;

    }
    private void startClicked(object sender, EventArgs e)
    {
        StartGame();
    }

}