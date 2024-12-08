namespace MeasureLengthDeviceNamespace
{
    public interface IMeasuringDevice
    {
        public decimal MetricValue();

        public decimal ImperialValue();

        public void StartCollecting();

        public void StopCollecting();

        public int[] GetRawData();
    }
}