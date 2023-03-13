namespace P01.Stream_Progress
{
    public class Program
    {
        static void Main()
        {
            IStreamer fileStreamer = new File("Name", 20, 0);
            IStreamer musicStreamer = new Music("Ivan", "Dark Side of the Moon", 20, 0);
            StreamProgressInfo info = new StreamProgressInfo(musicStreamer);
            
        }
    }
}
