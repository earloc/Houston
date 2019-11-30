namespace Houston.Audio
{
    public class VolumeChangedEventArgs
    {
        public int From { get; }
        public int To { get; }

        public VolumeChangedEventArgs(int from, int to)
        {
            From = from;
            To = to;
        }
    }

}