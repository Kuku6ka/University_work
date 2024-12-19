
using Device;
using System.ComponentModel;
using System.IO;
using UnitsMeasure;

namespace MeasuringDevice
{
    public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice
    {
        public int[] dataCaptured;
        public int mostRecentMeasure;
        public DeviceController? controller;
        public DeviceType measurementType;

        public int HeartBeatInterval { get; }

        public Units UnitsToUse { get; }

        public int[] DataCaptured { get; }

        public int MostRecentMeasure { get; }

        private StreamWriter? loggingFileWriter;
        public string LoggingFileName { get; set; }

        public event EventHandler NewMeasurementTaken;


        event IEventEnabledMeasuringDevice.HeartBeatEventHandler IEventEnabledMeasuringDevice.HeartBeat
        {
            add { }

            remove { }
        }

        protected virtual void OnNewMeasurementTaken() {
            NewMeasurementTaken?.Invoke(this, EventArgs.Empty);
        }

        public bool disposed = false;
        private void Dispose()
        {
            disposed = true;
            dataCollector?.Dispose();
        }

        private BackgroundWorker dataCollector;
        public abstract decimal MetricValue();

        public abstract decimal ImperialValue();

        public void StartCollecting()
        {
            controller = DeviceController.StartDevice(measurementType);
            loggingFileWriter = new StreamWriter("log.txt");
            GetMeasurements();
        }


        public void StopCollecting()
        {
            if (controller != null)
            {
                controller.StopDevice();
                loggingFileWriter?.Close();
                loggingFileWriter?.Dispose();
                controller = null;
            }
            disposed = true;
            dataCollector?.CancelAsync();
            Dispose();
        }
        private void GetMeasurements()
        {
            dataCollector = new BackgroundWorker();
            dataCollector.WorkerReportsProgress = true;
            dataCollector.WorkerSupportsCancellation = true;

            dataCollector.DoWork += new DoWorkEventHandler(dataCollector_DoWork);
            dataCollector.ProgressChanged += new ProgressChangedEventHandler(dataCollector_ProgressChanged);

            dataCollector.RunWorkerAsync();
        }

        private void dataCollector_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            OnNewMeasurementTaken();
        }

        
        private void dataCollector_DoWork(object? sender, DoWorkEventArgs e)
        {
            dataCaptured = new int[10];
            int i = 0;

            while (!dataCollector.CancellationPending && !disposed)
            {
                if (controller != null)
                {
                    dataCaptured[i] = controller.TakeMeasurement();
                }
                else
                {
                    dataCaptured[i] = dataCaptured[i];
                }
                mostRecentMeasure = dataCaptured[i];
                loggingFileWriter?.WriteLine($"Measurement - {mostRecentMeasure}");
                dataCollector.ReportProgress(0);
                i = (i + 1) % 10;
                Thread.Sleep(500);
            }
        }

        public int[] GetRawData()
        {
            return dataCaptured;
        }


        public string GetLoggingFile()
        {
            throw new NotImplementedException();
        }
    }
}
