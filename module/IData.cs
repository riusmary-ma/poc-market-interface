namespace PocMarketInterface.Module
{
    public interface IData
    {
        long Id { get; set; }
        DateTime CreatedTime { get; set; }
        string DataType { get; }
        string MarketCode { get; set; }
        decimal Price { get; set; }
        int Quantity { get; set; }   
        decimal Amount { get; set; }
    }
}