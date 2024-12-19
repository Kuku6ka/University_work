using System.Windows.Documents;
using UnitsMeasure;

namespace Device
{
    public class DeviceController
    {
        private DeviceType measurementType;
        private bool isStopped;
        private DeviceController(DeviceType type)
        {
            measurementType = type;
            isStopped = false;
        }

        public static DeviceController StartDevice(DeviceType type)
        {
            return new DeviceController(type);
        }

        public int TakeMeasurement()
        {
            if (!isStopped)
            {
                Random rnd = new Random();
                return rnd.Next(1, 20);
            }
            else
            {
                throw new Exception("You need to collect data before you get raw data");
            }

        }

        public void StopDevice()
        {
            this.isStopped = true;
        }
    }
}