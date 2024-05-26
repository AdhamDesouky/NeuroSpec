using System.Numerics;

namespace NeuroSpecCompanion.Views.TremorTest;

public partial class TremorTestMainPage : ContentPage
{
	public TremorTestMainPage()
	{
		InitializeComponent();
	}
    void OnStartTestClicked(object sender, EventArgs e)
    {
        if (Accelerometer.IsMonitoring)
            return;

        Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        Accelerometer.Start(SensorSpeed.UI);
    }

    void OnStopTestClicked(object sender, EventArgs e)
    {
        if (!Accelerometer.IsMonitoring)
            return;

        Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
        Accelerometer.Stop();
    }

    void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
    {
        var data = e.Reading;
        // Display the accelerometer data
        AccelerationLabel.Text = $"Acceleration: X={data.Acceleration.X}, Y={data.Acceleration.Y}, Z={data.Acceleration.Z}";

        // Add logic here to measure shakes and deviations
        MeasureTremor(data.Acceleration);
    }

    private void MeasureTremor(Vector3 acceleration)
    {
        // Implement your logic to measure the tremor
        // For example, check if the acceleration values exceed certain thresholds

        double threshold = 0.1; // Example threshold for shake detection

        if (Math.Abs(acceleration.X) > threshold || Math.Abs(acceleration.Y) > threshold || Math.Abs(acceleration.Z) > threshold)
        {
            // Detected a shake
            // You can implement more sophisticated tremor measurement logic here
        }
    }
}