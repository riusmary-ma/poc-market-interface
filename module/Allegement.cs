namespace PocMarketInterface.Module
{
    public class Allegement: IData
    {
        public long DataId { get; set; }
        public DateTime CreatedTime { get; set; }

        public string DataType => EDataType.Allegement;

        public string MarketCode { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }   
    }
}
