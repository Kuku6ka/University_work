using units;
using MeasureLengthDeviceNamespace;
using DeviceControl;

namespace MeasureLengthDeviceNamespace
{
    public class MeasureLengthDevice : IMeasuringDevice
    {
        private Units unitsToUse;
        private int[] dataCaptured;
        private int mostRecentMeasure;
        private DeviceController? controller;
        private const DeviceType measurementType = DeviceType.LENGTH;
        public MeasureLengthDevice(Units units)
        {
            unitsToUse = units;
            dataCaptured = new int[10];
        }
 
        public decimal MetricValue() => (unitsToUse.Equals(Units.Metric)) ? mostRecentMeasure : mostRecentMeasure * 25.4m;
        
        public decimal ImperialValue() => (unitsToUse.Equals(Units.Imperial)) ? mostRecentMeasure : mostRecentMeasure * 0.03937m;

        public void StartCollecting()
        {
            controller = (controller == null) ? DeviceController.StartDevice(measurementType) : controller;
            GetMeasurements();
        }

        public void StopCollecting()
        {
            if (controller != null)
            {
                DeviceController.StopDevice();
                controller = null;
            }
        }

        public int[] GetRawData() => dataCaptured;

        private void GetMeasurements()
        {
            ThreadPool.QueueUserWorkItem((_) =>
            {
                int x = 0;
                Random timer = new Random();
                while (controller != null)
                {
                    Thread.Sleep(timer.Next(1000, 5000));
                    if (controller != null)
                    {
                        dataCaptured[x] = DeviceController.TakeMeasurement();
                        mostRecentMeasure = dataCaptured[x];
                        x = (x + 1) % dataCaptured.Length;
                    }
                }
            });
        }
    }
}
