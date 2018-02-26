using System;
using Windows.Devices.Gpio;

namespace UCSensor
{
    public class SensorModel
    {
        //activates the sensor
        public GpioPin TriggerPin { get; set; }
        //returns signal
        public GpioPin EchoPin { get; set; }
        public GpioController gpio { get; set; }
    }
}
