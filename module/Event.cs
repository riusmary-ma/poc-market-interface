namespace PocMarketInterface.Module
{
    public class Event
    {
        public long EventId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string EventType { get; set; }
        public string Data { get; set; }
        public long AssociatedDataId { get; set; }
    }
}