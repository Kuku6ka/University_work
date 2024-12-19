using MeasuringDevice;
using UnitsMeasure;

public class MeasureMassDevice : MeasureDataDevice
{
    private Units unitsToUse;

    public MeasureMassDevice(Units units)
    {
        unitsToUse = units;
        dataCaptured = new int[10];
    }

    public override decimal ImperialValue() => (unitsToUse.Equals(Units.Imperial)) ? mostRecentMeasure : mostRecentMeasure * 2.2046m;

    public override decimal MetricValue() => (unitsToUse.Equals(Units.Metric)) ? mostRecentMeasure : mostRecentMeasure * 0.4536m;
}