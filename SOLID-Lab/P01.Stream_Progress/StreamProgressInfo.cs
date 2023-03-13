namespace P01.Stream_Progress
{
    public class StreamProgressInfo
    {
        private IStreamer streamer;

        // If we want to stream a music file, we can't
        public StreamProgressInfo(IStreamer streamer)
        {
            this.streamer = streamer;
        }

        public int CalculateCurrentPercent()
        {
            return (this.streamer.BytesSent * 100) / this.streamer.Length;
        }
    }
}
