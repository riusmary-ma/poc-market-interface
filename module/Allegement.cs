using PocMarketInterface.Enum;

namespace PocMarketInterface.Module
{
    public class Allegement: IData
    {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }

        public string DataType => EDataType.Allegement.ToString();

        public required string MarketCode { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }   
    }
}
