using DeviceControl;
using units;

namespace MeasureLengthDeviceNamespace
{
    public class MeasureLengthDevice : MeasureDataDevice, IMeasuringDevice
    {
        protected override DeviceType measurementType => DeviceType.LENGTH;
        public MeasureLengthDevice(Units units)
        {
            unitsToUse = units;
            dataCaptured = new int[10];
        }
        
        public override decimal MetricValue() => (unitsToUse.Equals(Units.Metric)) ? mostRecentMeasure : mostRecentMeasure * 25.4m;
        
        public override decimal ImperialValue() => (unitsToUse.Equals(Units.Imperial)) ? mostRecentMeasure : mostRecentMeasure * 0.03937m;
    }
}