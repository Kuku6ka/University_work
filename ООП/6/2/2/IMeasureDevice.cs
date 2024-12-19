using UnitsEnumeration;
namespace MeasuringDevice
{
    public interface IMeasuringDevice
    {
        decimal MetricValue();

        decimal ImperialValue();

        void StartCollecting();

        void StopCollecting();

        int[] GetRawData();

        string GetLoggingFile();

        Units UnitsToUse { get; }

        int[] DataCaptured { get; }

        int MostRecentMeasure { get; }

         string LoggingFileName{ get; set; }
    }
}
