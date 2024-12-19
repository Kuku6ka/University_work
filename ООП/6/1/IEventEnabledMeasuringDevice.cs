namespace MeasuringDevice
{
    interface IEventEnabledMeasuringDevice : IMeasuringDevice
    {
        event EventHandler NewMeasurementTaken;

        delegate void HeartBeatEventHandler();
        
        event HeartBeatEventHandler HeartBeat;

        int HeartBeatInterval { get; }
    }
}