using System;
using System.Threading.Tasks;
using Disruptor;
using Disruptor.Dsl;
using Disruptor_net.Metrics.EventHandlers;
using Disruptor_net.Metrics.Sinks;

namespace ThroughPutDemo.Console
{
    public class Queue
    {
        private int ringbufferSize = (int)Math.Pow(128, 2);

        private RingBuffer<QueueItem> ringBuffer;
        private Disruptor<QueueItem> disruptor;

        private IMetricsEventHandler<QueueItem> metricsEventHandler;
        private long next;

        public void Start()
        {
            metricsEventHandler = new FixedMessageCountReporter<QueueItem>(new ColoredConsoleSink(), 1000000);
            disruptor = new Disruptor<QueueItem>(() => new QueueItem(), new SingleThreadedClaimStrategy(ringbufferSize), new BusySpinWaitStrategy(), TaskScheduler.Default);
            
            disruptor.HandleEventsWith(metricsEventHandler);

            ringBuffer = disruptor.Start();
            metricsEventHandler.Setup(ringBuffer);

        }

        public void Stop()
        {
            disruptor.Halt();
        }

        public void Enqueue(QueueItem item)
        {
            
            next = ringBuffer.Next();
            var entry = ringBuffer[next];
            item.FirstTouchTime = DateTimeOffset.UtcNow;
            entry.Update(item);
            ringBuffer.Publish(next);
        }
    }
}
