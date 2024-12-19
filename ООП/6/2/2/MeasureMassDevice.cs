using MeasuringDevice;
using UnitsEnumeration;

public class MeasureMassDevice : MeasureDataDevice
{
    private Units unitsToUse;
    new private const DeviceType measurementType = DeviceType.MASS;

    public MeasureMassDevice(Units unitsToUse)
    {
        this.unitsToUse = unitsToUse;
        this.dataCaptured = new int[10];
    }

    public MeasureMassDevice(Units unitsToUse, int heartBeatInterval)
    {
        this.unitsToUse = unitsToUse;
        this.dataCaptured = new int[10];
        this.HeartBeatInterval = heartBeatInterval;
    }

    public override decimal ImperialValue() => (unitsToUse.Equals(Units.Imperial)) ? mostRecentMeasure : mostRecentMeasure * 2.2046m;

    public override decimal MetricValue() => (unitsToUse.Equals(Units.Metric)) ? mostRecentMeasure : mostRecentMeasure * 0.4536m;
}