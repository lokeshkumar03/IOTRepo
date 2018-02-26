﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.Devices.Gpio;


namespace UCSensor
{
    public class UcSensor
    {
        GpioController gpio = GpioController.GetDefault();

        GpioPin TriggerPin; // activates the sensor
        GpioPin EchoPin;// returns signal
        public UcSensor(int TriggerPin, int EchoPin)
        {
            this.TriggerPin = gpio.OpenPin(TriggerPin);
            this.EchoPin = gpio.OpenPin(EchoPin);

            this.TriggerPin.SetDriveMode(GpioPinDriveMode.Output);
            this.EchoPin.SetDriveMode(GpioPinDriveMode.Input);

            this.TriggerPin.Write(GpioPinValue.Low);
        }

        //public double GetDistance()
        //{
        //    ManualResetEvent mre = new ManualResetEvent(false);
        //    mre.WaitOne(500);
        //    Stopwatch pulseLength = new Stopwatch();

        //    //Send pulse
        //    this.TriggerPin.Write(GpioPinValue.High);
        //    mre.WaitOne(TimeSpan.FromMilliseconds(0.01));
        //    this.TriggerPin.Write(GpioPinValue.Low);
            
        //    //Recieve pusle
        //    while (this.EchoPin.Read() == GpioPinValue.Low)
        //    {
        //        Debug.WriteLine("Signal received");
        //        break;
        //    }
        //    pulseLength.Start();


        //    while (this.EchoPin.Read() == GpioPinValue.High)
        //    {
        //    }
        //    pulseLength.Stop();

        //    //Calculating distance
        //    TimeSpan timeBetween = pulseLength.Elapsed;
        //    Debug.WriteLine(timeBetween.ToString());
        //    double cms = timeBetween.TotalSeconds / 0.000058;
        //    double distance = timeBetween.TotalSeconds * 17000;

        //    return distance;
        //}
    }
}
