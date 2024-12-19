using System.Windows.Documents;
using UnitsEnumeration;

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
            Random random = new Random();
            return random.Next(1, 100);
        }

        public void StopDevice()
        {
            this.isStopped = true;
        }
    }
}