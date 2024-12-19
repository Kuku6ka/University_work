namespace MeasuringDevice
{
    public interface IEventEnabledMeasuringDevice : IMeasuringDevice
    {
        int HeartBeatInterval { get; }

        event EventHandler NewMeasurementTaken;

        public delegate void HeartBeatEventHandler(object sender, EventArgs e);

        public event HeartBeatEventHandler HeartBeat;
    }
}