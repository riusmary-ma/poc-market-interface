namespace PocMarketInterface.Module
{
    public class Event
    {
        public long EventId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string EventType { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public long AssociatedDataId { get; set; }
    }
}