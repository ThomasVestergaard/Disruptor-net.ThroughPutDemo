using System;
using Disruptor_net.Metrics.QueueItem;

namespace ThroughPutDemo.Console
{
    public class QueueItem : IRingBufferItem
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public DateTimeOffset FirstTouchTime { get; set; }

        public void Update(QueueItem other)
        {
            FirstTouchTime = other.FirstTouchTime;
            StringValue = other.StringValue;
            IntValue = other.IntValue;
        }
    }
}
