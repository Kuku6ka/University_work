namespace MeasuringDevice
{
    public class HeartBeatEventArgs : EventArgs
    {
        public DateTime TimeStamp { get; }
        public HeartBeatEventArgs() : base()
        {
            TimeStamp = DateTime.Now;
        }
    }
}