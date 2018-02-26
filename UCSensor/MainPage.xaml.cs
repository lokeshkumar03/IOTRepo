using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using Windows.Devices.Gpio;
using System.Diagnostics;
using System.Threading;

namespace UCSensor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;
        UcSensor uc;
        GpioPin TriggerPin;
        GpioPin EchoPin;
        public MainPage()
        {
            this.InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            InitGPIO();
            if (TriggerPin != null && EchoPin != null)
            {
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            GpioStatus.Text = "";
            GpioStatus.Text = "Object is at cms :" + GetDistance().ToString();
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();
            if (gpio == null)
            {
                TriggerPin = null;
                EchoPin = null;
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            this.TriggerPin = gpio.OpenPin(4);
            this.EchoPin = gpio.OpenPin(18);

            this.TriggerPin.SetDriveMode(GpioPinDriveMode.Output);
            this.EchoPin.SetDriveMode(GpioPinDriveMode.Input);

            this.TriggerPin.Write(GpioPinValue.Low);
            //uc = new UcSensor(4, 18);
            //GpioStatus.Text = "Object is at cms :" + uc.GetDistance().ToString();
        }

        

        public double GetDistance()
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            mre.WaitOne(500);
            Stopwatch pulseLength = new Stopwatch();

            //Send pulse
            this.TriggerPin.Write(GpioPinValue.High);
            mre.WaitOne(TimeSpan.FromMilliseconds(0.01));
            this.TriggerPin.Write(GpioPinValue.Low);

            //Recieve pusle
            if (this.EchoPin.Read() == GpioPinValue.Low)
            {
                Debug.WriteLine("Signal received");
            }
            pulseLength.Start();

            if (this.EchoPin.Read() != GpioPinValue.High)
                pulseLength.Stop();

            //Calculating distance
            TimeSpan timeBetween = pulseLength.Elapsed;
            Debug.WriteLine(timeBetween.ToString());
            double cms = timeBetween.TotalSeconds / 0.000058;
            double distance = timeBetween.TotalSeconds * 17000;

            return distance;
        }
    }
}
