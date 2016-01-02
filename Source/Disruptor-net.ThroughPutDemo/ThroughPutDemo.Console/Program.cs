namespace ThroughPutDemo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = new Queue();
            queue.Start();

            System.Console.WriteLine("Hit any to start. Ctrl+C to quit when running.");
            System.Console.ReadKey();

            var qItem1 = new QueueItem { IntValue = 10, StringValue = "Blarh" };
            
            while (true)
                queue.Enqueue(qItem1);

            queue.Stop();
        }
    }
}
