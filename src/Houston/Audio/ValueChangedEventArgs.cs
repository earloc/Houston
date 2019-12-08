namespace Houston.Audio
{
    public class ValueChangedEventArgs<T> where T : struct
    {
        public T? From { get; }
        public T To { get; }

        public ValueChangedEventArgs(T? from, T to)
        {
            From = from;
            To = to;
        }
    }

}