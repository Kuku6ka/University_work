using units;

namespace DeviceControl
{
    public class DeviceController
    {
        private static DeviceController? instance;
        private DeviceType deviceType;
        
        private DeviceController(DeviceType type)
        {
            deviceType = type;
        }

        public static DeviceController StartDevice(DeviceType type)
        {
            instance ??= new DeviceController(type);
            return instance;
        }

        public static void StopDevice()
        {
            instance = null;
        }

        public static int TakeMeasurement()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }
    }
}